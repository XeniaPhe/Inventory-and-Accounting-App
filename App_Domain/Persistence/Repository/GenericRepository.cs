using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Xenia.IaA.AppDomain.Entity.Model;
using Xenia.IaA.AppDomain.Persistence.Context;

namespace Xenia.IaA.AppDomain.Persistence.Repository;
internal class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : class, IEntity<Entity>
{
    protected readonly ApplicationDbContext context;
    protected readonly DbSet<Entity> entities;

    internal GenericRepository(ApplicationDbContext context)
    {
        this.context = context;
        this.entities = context.Set<Entity>();
    }

    public virtual IEnumerable<Entity> Get(Expression<Func<Entity, bool>>? filter = null,
        Func<IQueryable<Entity>, IOrderedQueryable<Entity>>? orderBy = null)
    {
        IQueryable<Entity> query = entities;

        if (filter is not null)
        {
            query = query.Where(filter);
        }

        if (orderBy is not null)
        {
            query = orderBy(query);
        }

        return query;
    }

    public virtual async Task<Entity?> GetByIDAsync(object ID)
    {
        return await entities.FindAsync(ID);
    }

    public virtual async Task<Entity?> InsertAsync(Entity entity)
    {
        return (await entities.AddAsync(entity))?.Entity;
    }

    public virtual async Task DeleteAsync(object ID)
    {
        Entity? entity = await entities.FindAsync(ID);

        if (entity is null)
            return;

        Delete(entity);
    }

    public virtual void Delete(Entity entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(Entity), "Entity cannot be null.");

        if (context.Entry(entity).State == EntityState.Detached)
        {
            entities.Attach(entity);
        }

        entities.Remove(entity);
    }

    public virtual void Update(Entity entity)
    {
        if (entity is null)
            throw new ArgumentNullException(nameof(Entity), "Entity cannot be null.");

        entities.Attach(entity);
        context.Entry(entity).State = EntityState.Modified;
    }
}
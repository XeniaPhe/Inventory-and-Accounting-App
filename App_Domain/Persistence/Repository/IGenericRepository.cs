using System.Linq.Expressions;
using Xenia.IaA.AppDomain.Entity.Model;

namespace Xenia.IaA.AppDomain.Persistence.Repository;
internal interface IGenericRepository<Entity> where Entity : class, IEntity<Entity>
{
    IEnumerable<Entity> Get(Expression<Func<Entity, bool>> filter, Func<IQueryable<Entity>, IOrderedQueryable<Entity>> orderBy);
    Task<Entity?> GetByIDAsync(object ID);
    Task<Entity?> InsertAsync(Entity entity);
    Task DeleteAsync(object ID);
    void Delete(Entity entity);
    void Update(Entity entity);
}
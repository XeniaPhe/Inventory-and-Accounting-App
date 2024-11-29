﻿using Microsoft.EntityFrameworkCore;
using Xenia.IaA.AppDomain.Controller.Base;
using Xenia.IaA.AppDomain.Controller.Impl;
using Xenia.IaA.AppDomain.Entity.Model;
using Xenia.IaA.AppDomain.Persistence.Context;
using Xenia.IaA.AppDomain.Service;

namespace Xenia.IaA.AppDomain.Controller.Proxy;
public class ProductControllerProxy : ControllerProxyBase<Product, ProductController, ProductService>, IProductController
{
    internal ProductControllerProxy(DbContextOptions<ApplicationDbContext> contextOptions) : base(contextOptions) { }
}
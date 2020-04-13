﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectList.Models
{
    public interface IRepository : IDisposable
    {
        IQueryable<Product> GetProductList();
        IQueryable<Category> GetCategoryList();
        Product GetProduct(int id);
        Category GetCategory(int id);

        void Create(Product product);
        void Update(Product product);
        IQueryable<Product> Filter(string searchString);

        void Delete(int id);

        void Save();
    }
}

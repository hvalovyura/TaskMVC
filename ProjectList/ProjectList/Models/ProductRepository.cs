using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectList.Models
{
    public class ProductRepository : IRepository
    {
        private ProductContext db;
        public ProductRepository(ProductContext context)
        {
            this.db = context;
        }
        public IQueryable<Product> GetProductList()
        {
            return db.Product;
        }

        public IQueryable<Category> GetCategoryList()
        {
            return db.Category;
        }
        public Product GetProduct(int id)
        {
            return db.Product.Find(id);
        }
        public Category GetCategory(int id)
        {
            return db.Category.Find(id);
        }

        //public IQueryable<Product> Filter(string searchString)
        //{
        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        return db.Product.Where(s => s.Name.Contains(searchString));
        //    }
        //    else
        //    {
        //        return db.Product;
        //    }
        //}

        public void Create(Product product)
        {
            db.Product.Add(product);
        }

        public void Update(Product product)
        {
            db.Entry(product).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Product product = db.Product.Find(id);
            if (product != null)
                db.Product.Remove(product);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}

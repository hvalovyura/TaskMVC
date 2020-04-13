using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectList.Models;

namespace ProjectList.Controllers
{
    public class HomeController : Controller
    {

        //ProductContext db;

        //public HomeController(ProductContext context)
        //{
        //    this.db = context;

        //    Category music = new Category { Name = "Music" };
        //    Category video = new Category { Name = "Video" };
        //    Category photo = new Category { Name = "Photo" };
        //    Category category = db.Category.FirstOrDefault(u => u.Name == music.Name);
        //    if(category == null)
        //    {
        //        db.Category.Add(music);
        //    }            
        //    category = db.Category.FirstOrDefault(u => u.Name == video.Name);
        //    if (category == null)
        //    {
        //        db.Category.Add(video);
        //    }
        //    category = db.Category.FirstOrDefault(u => u.Name == photo.Name);
        //    if (category == null)
        //    {
        //        db.Category.Add(photo);
        //    }
        //    db.SaveChanges();           
        //}
        IRepository db;

        public HomeController(IRepository r)
        {
            this.db = r;

            //Category music = new Category { Name = "Music" };
            //Category video = new Category { Name = "Video" };
            //Category photo = new Category { Name = "Photo" };
            //Category category = db.Category.FirstOrDefault(u => u.Name == music.Name);
            //if (category == null)
            //{
            //    db.Category.Add(music);
            //}
            //category = db.Category.FirstOrDefault(u => u.Name == video.Name);
            //if (category == null)
            //{
            //    db.Category.Add(video);
            //}
            //category = db.Category.FirstOrDefault(u => u.Name == photo.Name);
            //if (category == null)
            //{
            //    db.Category.Add(photo);
            //}
            //db.SaveChanges();
        }


        public IActionResult Index(string searchString) //Main page with project list
        {
            IQueryable<Product> products = db.GetProductList().Include(x => x.Category);
            //List<Product> products = db.GetProductList();
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.Name.Contains(searchString));
            }
            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    products = db.Filter(searchString);
            //}
            return View(products);
        }

        //public IActionResult Create()
        //{
        //    IQueryable<Category> categories = db.Category;
        //    ViewBag.Greeting = categories;
        //    return View("Create");
        //}

        //[HttpPost]
        //public async Task<IActionResult> Create(Product project)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        db.Product.Add(project);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        IQueryable<Category> categories = db.Category;
        //        ViewBag.Greeting = categories;
        //        return View();
        //    }            
        //}

        //public async Task<IActionResult> Edit(int? id)
        //{
        //    IQueryable<Category> categories = db.Category;
        //    ViewBag.Greeting = categories;
        //    Product project = await db.Product.FirstOrDefaultAsync(p => p.Id == id);
        //    if (project != null)
        //    {
        //        return View(project);
        //    }
        //    return NotFound();
        //}

        //[HttpPost]
        //public async Task<IActionResult> Edit(Product project)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        db.Product.Update(project);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //}

        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id != null)
        //    {
        //        Product project = new Product { Id = id.Value };
        //        db.Entry(project).State = EntityState.Deleted;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    return NotFound();
        //}

        public IActionResult Category(string searchString)
        {
            IQueryable<Category> categories = db.GetCategoryList();
            if (!String.IsNullOrEmpty(searchString))
            {
                categories = categories.Where(s => s.Name.Contains(searchString));
            }
            return View(categories);
        }

        //public IActionResult CreateCategory()
        //{
        //    return View("CreateCategory");
        //}

        //[HttpPost]
        //public async Task<IActionResult> CreateCategory(Category category)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        db.Category.Add(category);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Category");
        //    }
        //    else
        //    {
        //        return View();
        //    }

        //}

        //public async Task<IActionResult> EditCategory(int? id)
        //{
        //    Category category = await db.Category.FirstOrDefaultAsync(p => p.Id == id);
        //    if (category != null)
        //    {
        //        return View(category);
        //    }
        //    return NotFound();
        //}

        //[HttpPost]
        //public async Task<IActionResult> EditCategory(Category category)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        db.Category.Update(category);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Category");
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //}

        //public async Task<IActionResult> DeleteCategory(int? id)
        //{
        //    bool permission = true;            
        //    if (id != null)
        //    {
        //        Category category = await db.Category.FirstOrDefaultAsync(p => p.Id == id);
        //        if (category != null)
        //        {
        //            IEnumerable<Product> projects = db.Product;
        //            foreach(Product p in projects)
        //            {
        //                if (p.CategoryId == category.Id) permission = false;
        //            }
        //            if (permission == true)
        //            {
        //                db.Category.Remove(category);
        //                await db.SaveChangesAsync();
        //                return RedirectToAction("Category");
        //            }   
        //            else
        //            {
        //                IQueryable<Category> categories = db.Category;
        //                ViewBag.Message = "Can't delete selected category: category does not empty, change project categories in projects list!";
        //                return View("Category", categories);
        //            }                        
        //        }
        //    }
        //    return NotFound();
        //}

    }
}

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
        private List<Category> requiredList;
        IRepository db;
        public void RequiredCategories()
        {
            Category music = new Category { Name = "Music" };       //this categories are required
            Category video = new Category { Name = "Video" };
            Category photo = new Category { Name = "Photo" };
            this.requiredList = new List<Category>();
            requiredList.Add(music);
            requiredList.Add(video);
            requiredList.Add(photo);
        }

        public HomeController(IRepository repository)
        {
            this.db = repository;
            RequiredCategories();
            //Category music = new Category { Name = "Music" };       //this categories are required
            //Category video = new Category { Name = "Video" };
            //Category photo = new Category { Name = "Photo" };
            //Category category = db.GetCategoryList().FirstOrDefault(u => u.Name == music.Name);
            //if (category == null)
            //{
            //    db.CreateCategory(music);
            //    db.Save();
            //}
            //category = db.GetCategoryList().FirstOrDefault(u => u.Name == video.Name);
            //if (category == null)
            //{
            //    db.CreateCategory(video);
            //    db.Save();
            //}
            //category = db.GetCategoryList().FirstOrDefault(u => u.Name == photo.Name);
            //if (category == null)
            //{
            //    db.CreateCategory(photo);
            //    db.Save();
            //}
        }


        public IActionResult Index(string searchString) //Main page with project list
        {
            IQueryable<Product> products = db.GetProductList().Include(x => x.Category);
            if (!String.IsNullOrEmpty(searchString))
            {
                products = products.Where(s => s.Name.Contains(searchString));
            }
            return View(products);
        }

        public IActionResult Create()
        {
            IQueryable<Category> categories = db.GetCategoryList();
            ViewBag.Greeting = categories;
            return View("Create");
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Create(product);
                db.Save();
                return RedirectToAction("Index");
            }
            else
            {
                IQueryable<Category> categories = db.GetCategoryList();
                ViewBag.Greeting = categories;
                return View();
            }
        }

        public IActionResult Edit(int id)
        {
            IQueryable<Category> categories = db.GetCategoryList();
            ViewBag.Greeting = categories;
            Product product = db.GetProduct(id);
            if (product != null)
            {
                return View(product);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Update(product);
                db.Save();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public IActionResult Delete(int id)
        {
            db.Delete(id);
            db.Save();
            return RedirectToAction("Index");
        }

        public IActionResult Category(string searchString)
        {
            Category music = new Category { Name = "Music" };       //this categories are required
            Category video = new Category { Name = "Video" };
            Category photo = new Category { Name = "Photo" };
            Category category = db.GetCategoryList().FirstOrDefault(u => u.Name == music.Name);
            if (category == null)
            {
                db.CreateCategory(music);
                db.Save();
            }
            category = db.GetCategoryList().FirstOrDefault(u => u.Name == video.Name);
            if (category == null)
            {
                db.CreateCategory(video);
                db.Save();
            }
            category = db.GetCategoryList().FirstOrDefault(u => u.Name == photo.Name);
            if (category == null)
            {
                db.CreateCategory(photo);
                db.Save();
            }
            IQueryable<Category> categories = db.GetCategoryList();
            if (!String.IsNullOrEmpty(searchString))
            {
                categories = categories.Where(s => s.Name.Contains(searchString));
            }
            return View(categories);
        }

        public IActionResult CreateCategory()
        {
            return View("CreateCategory");
        }

        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                db.CreateCategory(category);
                db.Save();
                return RedirectToAction("Category");
            }
            else
            {
                return View();
            }
        }

        public IActionResult EditCategory(int id)
        {
            Category category = db.GetCategory(id);
            if (category != null)
            {
                return View(category);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult EditCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                db.UpdateCategory(category);
                db.Save();
                return RedirectToAction("Category");
            }
            else
            {
                return View();
            }
        }

        public IActionResult DeleteCategory(int id)
        {
            bool req = true;
            bool permission = true;
            Category category = db.GetCategory(id);
            foreach (Category cat in requiredList)
            {
                if (cat.Name == category.Name)
                    req = false;
            }
            if (category != null)
            {
                IEnumerable<Product> products = db.GetProductList();
                foreach (Product p in products)
                {
                    if (p.CategoryId == category.Id) permission = false;
                }
                if (permission == true && req == true)
                {
                    db.DeleteCategory(id);
                    db.Save();
                    return RedirectToAction("Category");
                }
                else
                {
                    if (req == false) ViewBag.Message = "Can't delete required category";
                    IQueryable<Category> categories = db.GetCategoryList();
                    if (permission == false && req == true) ViewBag.Message = "Can't delete selected category: category does not empty, change project categories in projects list!";
                    return View("Category", categories);
                }
            }
            else
            {
                return NotFound();
            }
        }

    }
}

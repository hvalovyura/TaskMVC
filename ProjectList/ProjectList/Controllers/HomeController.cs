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

        ProjectsContext db;

        public HomeController(ProjectsContext context)
        {
            this.db = context;
            
        }

        public IActionResult Index(string searchString) //Main page with project list
        {
            IQueryable<Product> users = db.Product.Include(x => x.Category);
            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(s => s.Name.Contains(searchString));
            }            
            return View(users);
        }

        public IActionResult Create()
        {
            IQueryable<Category> categories = db.Category;
            ViewBag.Greeting = categories;
            return View("Create");
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product project)
        {
            if(ModelState.IsValid)
            {
                db.Product.Add(project);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                IQueryable<Category> categories = db.Category;
                ViewBag.Greeting = categories;
                return View();
            }            
        }

        public async Task<IActionResult> Edit(int? id)
        {
            IQueryable<Category> categories = db.Category;
            ViewBag.Greeting = categories;
            Product project = await db.Product.FirstOrDefaultAsync(p => p.Id == id);
            if (project != null)
            {
                return View(project);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product project)
        {
            if(ModelState.IsValid)
            {
                db.Product.Update(project);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Product project = new Product { Id = id.Value };
                db.Entry(project).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        public IActionResult CategoryList(string searchString)
        {
            IQueryable<Category> categories = db.Category;
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
        public async Task<IActionResult> CreateCategory(Category category)
        {
            if(ModelState.IsValid)
            {
                db.Category.Add(category);
                await db.SaveChangesAsync();
                return RedirectToAction("CategoryList");
            }
            else
            {
                return View();
            }
            
        }

        public async Task<IActionResult> EditCategory(int? id)
        {
            Category category = await db.Category.FirstOrDefaultAsync(p => p.Id == id);
            if (category != null)
            {
                return View(category);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditCategory(Category category)
        {
            if(ModelState.IsValid)
            {
                db.Category.Update(category);
                await db.SaveChangesAsync();
                return RedirectToAction("CategoryList");
            }
            else
            {
                return View();
            }
        }

        public async Task<IActionResult> DeleteCategory(int? id)
        {
            bool permission = true;            
            if (id != null)
            {
                Category category = await db.Category.FirstOrDefaultAsync(p => p.Id == id);
                if (category != null)
                {
                    IEnumerable<Product> projects = db.Product;
                    foreach(Product p in projects)
                    {
                        if (p.CategoryId == category.Id) permission = false;
                    }
                    if (permission == true)
                    {
                        db.Category.Remove(category);
                        await db.SaveChangesAsync();
                        return RedirectToAction("CategoryList");
                    }   
                    else
                    {
                        IQueryable<Category> categories = db.Category;
                        ViewBag.Message = "Can't delete selected category: category does not empty, change project categories in projects list!";
                        return View("CategoryList", categories);
                    }                        
                }
            }
            return NotFound();
        }

    }
}

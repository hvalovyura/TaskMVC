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

        public IActionResult Index() //Main page with project list
        {
            IQueryable<Project> users = db.Projects.Include(x => x.Category);
            return View(users);
        }

        public IActionResult Create()
        {
            IQueryable<Category> categories = db.Categories;
            ViewBag.Greeting = categories;
            return View("Create");
        }

        [HttpPost]
        public async Task<IActionResult> Create(Project project)
        {
            if(ModelState.IsValid)
            {
                db.Projects.Add(project);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                IQueryable<Category> categories = db.Categories;
                ViewBag.Greeting = categories;
                return View();
            }            
        }

        public async Task<IActionResult> Edit(int? id)
        {
            IQueryable<Category> categories = db.Categories;
            ViewBag.Greeting = categories;
            Project project = await db.Projects.FirstOrDefaultAsync(p => p.Id == id);
            if (project != null)
            {
                return View(project);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Project project)
        {
            if(ModelState.IsValid)
            {
                db.Projects.Update(project);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }



        public IActionResult CategoryList()
        {
            IQueryable<Category> categories = db.Categories;
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
                db.Categories.Add(category);
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
            Category category = await db.Categories.FirstOrDefaultAsync(p => p.Id == id);
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
                db.Categories.Update(category);
                await db.SaveChangesAsync();
                return RedirectToAction("CategoryList");
            }
            else
            {
                return View();
            }
        }



        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}

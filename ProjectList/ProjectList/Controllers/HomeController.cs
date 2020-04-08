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
            if(db.Categories.Count() == 0)
            {
                Category music = new Category { Name = "Music" };
                Category video = new Category { Name = "Video" };
                Category image = new Category { Name = "Image" };

                Project project1 = new Project { Name = "Album1", Category = music, Price = 100, Description = "New album from new band." };
                Project project2 = new Project { Name = "Album2", Category = music, Price = 500, Description = "New album from old band." };
                Project project3 = new Project { Name = "Video1", Category = video, Price = 200, Description = "New film." };
                Project project4 = new Project { Name = "Video2", Category = video, Price = 250, Description = "Old film." };
                Project project5 = new Project { Name = "Picture", Category = image, Price = 50, Description = "New picture from new artist." };
                Project project6 = new Project { Name = "Picture2", Category = image, Price = 1500, Description = "Ancient picture from dead artist." };

                db.Categories.AddRange(music, video, image);
                db.Projects.AddRange(project1, project2, project3, project4, project5, project6);
                db.SaveChanges();
            }
        }



        private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index()
        {
            IQueryable<Project> users = db.Projects.Include(x => x.Category);
            return View(users);
        }

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}

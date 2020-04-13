using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectList.Controllers;
using ProjectList.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectList.Controllers.Test
{
    [TestClass()]
    public class HomeControllerTests
    {
        ProjectsContext db;
        [TestMethod()]
        public void CreateCategoryTest()
        {
            HomeController controller = new HomeController(db);

            ViewResult result = controller.CreateCategory() as ViewResult;

            Assert.Equals("CreateCategory", result.ViewName);
        }
    }
}
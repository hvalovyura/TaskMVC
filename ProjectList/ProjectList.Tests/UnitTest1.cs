using Microsoft.AspNetCore.Mvc;
using ProjectList.Controllers;
using ProjectList.Models;
using System;
using Xunit;

namespace ProjectList.Tests
{
    public class UnitTest1
    {
        ProjectsContext db;
        [Fact]
        public void Test1()      
        {            
            HomeController controller = new HomeController(db);
            ViewResult result = controller.Create() as ViewResult;
            Assert.Equal("Create", result.ViewName);
        }
    }
}

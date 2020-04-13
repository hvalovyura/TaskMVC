using Microsoft.AspNetCore.Mvc;
using ProjectList.Controllers;
using ProjectList.Models;
using System;
using Xunit;

namespace ProjectList.Test
{
    public class ControllerTests
    {
        ProjectsContext db;
        [Fact]
        public void CreateCategoryViewResultIsEqual_CreateCategory()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.CreateCategory() as ViewResult;

            Assert.Equal("CreateCategory", result.ViewName);
        }

        [Fact]
        public void CreateCategoryViewResultNotNull()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.CreateCategory() as ViewResult;

            Assert.NotNull(result);
        }
    }
}

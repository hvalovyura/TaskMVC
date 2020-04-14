using Microsoft.AspNetCore.Mvc;
using Moq;
using ProjectList.Controllers;
using ProjectList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ProjectList.Test
{
    public class ControllerTests
    {
        [Fact]
        public void IndexViewIsNotEqualNull()
        {
            // Arrange
            var mock = new Mock<IRepository>();
            mock.Setup(a => a.GetProductList());
            HomeController controller = new HomeController(mock.Object);
            // Act
            ViewResult result = controller.Index("") as ViewResult;
            // Assert
            Assert.NotNull(result.Model);
        }

        [Fact]
        public void CategoryViewIsNotEqualNull()
        {
            var mock = new Mock<IRepository>();
            mock.Setup(a => a.GetCategoryList());
            HomeController controller = new HomeController(mock.Object);

            ViewResult result = controller.Category("") as ViewResult;

            Assert.NotNull(result.Model);
        }

        [Fact]
        public void CreateCategoryViewIsEqualCreateCategory()
        {
            var mock = new Mock<IRepository>();
            mock.Setup(a => a.GetCategoryList());
            HomeController controller = new HomeController(mock.Object);

            ViewResult result = controller.CreateCategory() as ViewResult;

            Assert.Equal("CreateCategory", result.ViewName);
        }

        [Fact]
        public void CategorySendCategoryModelToShowInListView()
        {            
            var mockCategory = new Mock<IRepository>();
            mockCategory.Setup(a => a.GetCategoryList());
            HomeController controller = new HomeController(mockCategory.Object);
            
            var result = controller.Category("") as ViewResult;
            
            Assert.IsAssignableFrom<IQueryable<Category>>(result.Model);
        }

        [Fact]
        public void CategoryCountInCategoryViewIsEqual_3()
        {
            var mockCategory = new Mock<IRepository>();
            mockCategory.Setup(a => a.GetCategoryList()).Returns(TestData().AsQueryable());
            HomeController controller = new HomeController(mockCategory.Object);
            
            int count = mockCategory.Object.GetCategoryList().Count();

            
            Assert.Equal(3, count);
        }

        public List<Category> TestData()
        {
            List<Category> testdata = new List<Category>();
            Category music = new Category { Name = "Music" };
            Category video = new Category { Name = "Video" };
            Category photo = new Category { Name = "Photo" };
            testdata.Add(music);
            testdata.Add(video);
            testdata.Add(photo);
            return testdata;
        }

    }
}

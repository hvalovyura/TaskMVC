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

    }
}

using Ecommerce_Web_Application.Controllers;
using Ecommerce_Web_Application.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Ecommerce_Web_Application.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void Test_AddJob_Returns_RedirectToActionResult()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<HomeController>>();
            var mockContext = new Mock<Ecommerce_Web_ApplicationContext>();

            var controller = new HomeController(mockLogger.Object, mockContext.Object);

            // Act
            var result = controller.AddJob("Test Title", "Test Description");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));

            var redirectToActionResult = (RedirectToActionResult)result;
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }

        [TestMethod]
        public void AddJob_ReturnsViewResult_WhenModelStateIsInvalid()
        {
            // Arrange
            var mockContext = new Mock<Ecommerce_Web_ApplicationContext>();
            var controller = new HomeController(null, mockContext.Object);

            // Act
            var result = controller.AddJob("", "") as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(controller.ModelState.IsValid);
            Assert.AreEqual("", result.ViewName); // Ensure default view is returned
        }
    }
}

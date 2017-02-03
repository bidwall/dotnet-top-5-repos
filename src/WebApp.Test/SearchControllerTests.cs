using System.Web.Mvc;
using NUnit.Framework;
using WebApp.Controllers;
using WebApp.Models;

namespace WebApp.Test
{
    [TestFixture]
    public class SearchControllerTests
    {
        private SearchController _searchController;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _searchController = new SearchController();
        }
                
        [Test]
        public void IndexPost_Invalid_ReturnsViewWithModel()
        {
            //Arrange
            _searchController.ModelState.AddModelError("", "");

            //Act
            var result = _searchController.Index(new SearchViewModel()) as ViewResult;

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(string.IsNullOrEmpty(result.ViewName), Is.True);
        }

        [Test]
        public void IndexPost_ValidModel_RedirectsToUsersIndex()
        {
            //Arrange
            _searchController.ModelState.Clear();

            //Act
            var result = _searchController.Index(new SearchViewModel()) as RedirectToRouteResult;

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
            Assert.That(result.RouteValues["controller"], Is.EqualTo("Users"));
        }
    }
}

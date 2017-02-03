using System.Web.Mvc;
using FluentAssertions;
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
            result.Should().NotBeNull();
            result.ViewName.Should().BeNullOrEmpty();
        }

        [Test]
        public void IndexPost_ValidModel_RedirectsToUsersIndex()
        {
            //Arrange
            _searchController.ModelState.Clear();

            //Act
            var result = _searchController.Index(new SearchViewModel()) as RedirectToRouteResult;

            //Assert
            result.Should().NotBeNull();            
            result.RouteValues.Should().Contain("action", "Index");
            result.RouteValues.Should().Contain("controller", "Users");
        }
    }
}

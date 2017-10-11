using System.Collections.Generic;
using System.Web.Mvc;
using FizzWare.NBuilder;
using FluentAssertions;
using Models;
using Moq;
using NUnit.Framework;
using Repositories;
using WebApp.Controllers;
using WebApp.Models;

namespace WebApp.Test
{
    [TestFixture]
    public class UsersControllerTests
    {
        private Mock<IRepository> _mockRepository;
        private UsersController _usersController;
        private const string Username = "User";

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _mockRepository = new Mock<IRepository>();
            _usersController = new UsersController(_mockRepository.Object);
        }

        [Test]
        public void Index_UserNotFound_ReturnsNoResultsFound()
        {
            //Arrange
            _mockRepository.Setup(x => x.GetDetailsForUser(Username)).Returns(default(User));

            //Act
            var result = _usersController.Index(Username) as RedirectToRouteResult;

            //Assert
            result.RouteValues.Should().Contain("action", "NoResultsFound");
            result.RouteValues.Should().Contain("message", $"{Username} does not exists");
        }

        [Test]
        public void Index_UserFound_ReturnsUserDetails()
        {
            //Arrange
            var user = Builder<User>.CreateNew().Build();

            _mockRepository.Setup(x => x.GetDetailsForUser(Username)).Returns(user);
            _mockRepository.Setup(x => x.GetReposForUserFromUrl(user.Repos_Url)).Returns(new List<Repo>());

            //Act
            var result = _usersController.Index(Username) as ViewResult;

            //Assert
            var userViewModel = result.Model as UserViewModel;
            userViewModel.Name.Should().Be(user.Name);
            userViewModel.Location.Should().Be(user.Location);
            userViewModel.AvatarUrl.Should().Be(user.AvatarUrl);
        }

        [Test]
        public void Index_ManyRepos_ReturnsTop5StaredRepos()
        {
            //Arrange
            var repos = Builder<Repo>.CreateListOfSize(10).Build();

            _mockRepository.Setup(x => x.GetDetailsForUser(Username)).Returns(new User());
            _mockRepository.Setup(x => x.GetReposForUserFromUrl(It.IsAny<string>())).Returns(repos);

            //Act
            var result = _usersController.Index(Username) as ViewResult;

            //Assert
            var userViewModel = result.Model as UserViewModel;
            userViewModel.Repos.Should().HaveCount(5);
            userViewModel.Repos.Should().BeInDescendingOrder(x => x.Stars);
        }
    }
}
using System.Collections.Generic;
using FizzWare.NBuilder;
using FluentAssertions;
using HttpClientHelpers;
using Models;
using NUnit.Framework;
using Telerik.JustMock;
using Telerik.JustMock.Helpers;

namespace Repositories.Test
{
    [TestFixture]
    public class GitHubRepositoryTests
    {
        private IHttpClientHelper _mockHttpClientHelper;
        private GitHubRepository _gitHubRepository;

        private const string UserName = "username";
        private const string RepoUrl = "url";
        private readonly string _userUrl = $"{GitHubRepository.GitHubUri}/users/{UserName}";
        
        [SetUp]
        public void SetUp()
        {
            _mockHttpClientHelper = Mock.Create<IHttpClientHelper>(Behavior.Strict);
            _gitHubRepository = new GitHubRepository(_mockHttpClientHelper);
        }

        [Test]
        public void GetDetailsForUser_ConstructsGitHubUrl()
        {
            //Arrange
            _mockHttpClientHelper.Arrange(x => x.GetDataFromUrl<User>(_userUrl)).Returns(new User());

            //Act
            _gitHubRepository.GetDetailsForUser(UserName);
        }

        [Test]
        public void GetDetailsForUser_ReturnsUser()
        {
            //Arrange
            var testUser = Builder<User>.CreateNew().Build();

            _mockHttpClientHelper.Arrange(x => x.GetDataFromUrl<User>(Arg.AnyString)).Returns(testUser);

            //Act
            var user = _gitHubRepository.GetDetailsForUser(UserName);

            //Assert
            user.Name.Should().Be(testUser.Name);
        }

        [Test]
        public void GetReposForUserFromUrl_GetReposForUrl()
        {
            //Arrange
            _mockHttpClientHelper.Arrange(x => x.GetDataFromUrl<IEnumerable<Repo>>(RepoUrl)).Returns(new Repo[] {});

            //Act
            _gitHubRepository.GetReposForUserFromUrl(RepoUrl);
        }

        [Test]
        public void GetReposForUserFromUrl_ReturnsRepos()
        {
            //Arrange
            var testRepos = Builder<Repo>.CreateListOfSize(1).Build();
            _mockHttpClientHelper.Arrange(x => x.GetDataFromUrl<IEnumerable<Repo>>(Arg.AnyString)).Returns(testRepos);

            //Act
            var repos = _gitHubRepository.GetReposForUserFromUrl(RepoUrl);

            //Assert
            repos.Should().BeEquivalentTo(testRepos);
        }
    }
}

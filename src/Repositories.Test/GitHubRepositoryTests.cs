using System.Collections.Generic;
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
        private readonly IHttpClientHelper _mockHttpClientHelper;
        private readonly GitHubRepository _gitHubRepository;

        private const string UserName = "username";
        private const string RepoUrl = "url";
        private readonly string _userUrl = $"{GitHubRepository.GitHubUri}/users/{UserName}";


        public GitHubRepositoryTests()
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
            var testUser = new User{Name = "Test"};

            _mockHttpClientHelper.Arrange(x => x.GetDataFromUrl<User>(Arg.AnyString)).Returns(testUser);

            //Act
            var user = _gitHubRepository.GetDetailsForUser(UserName);

            //Assert
            Assert.That(user.Name, Is.EqualTo(testUser.Name));
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
            IEnumerable<Repo> testRepos = new[] { new Repo{ Name = "Test" } };
            _mockHttpClientHelper.Arrange(x => x.GetDataFromUrl<IEnumerable<Repo>>(Arg.AnyString)).Returns(testRepos);

            //Act
            var repos = _gitHubRepository.GetReposForUserFromUrl(RepoUrl);

            //Assert
            Assert.That(repos, Is.EquivalentTo(testRepos));
        }
    }
}

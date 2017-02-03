using System.Collections.Generic;
using FizzWare.NBuilder;
using FluentAssertions;
using HttpClientHelpers;
using Models;
using Moq;
using NUnit.Framework;

namespace Repositories.Test
{
    [TestFixture]
    public class GitHubRepositoryTests
    {
        private Mock<IHttpClientHelper> _mockHttpClientHelper;
        private GitHubRepository _gitHubRepository;

        private const string UserName = "username";
        private const string RepoUrl = "url";
        private readonly string _userUrl = $"{GitHubRepository.GitHubUri}/users/{UserName}";
        
        [SetUp]
        public void SetUp()
        {
            _mockHttpClientHelper = new Mock<IHttpClientHelper>();
            _gitHubRepository = new GitHubRepository(_mockHttpClientHelper.Object);
        }

        [Test]
        public void GetDetailsForUser_ConstructsGitHubUrl()
        {
            //Act
            _gitHubRepository.GetDetailsForUser(UserName);

            //Assert
            _mockHttpClientHelper.Verify(x => x.GetDataFromUrl<User>(_userUrl), Times.Once);
        }

        [Test]
        public void GetDetailsForUser_ReturnsUser()
        {
            //Arrange
            var testUser = Builder<User>.CreateNew().Build();
            _mockHttpClientHelper.Setup(x => x.GetDataFromUrl<User>(It.IsAny<string>())).Returns(testUser);
            
            //Act
            var user = _gitHubRepository.GetDetailsForUser(UserName);

            //Assert
            user.Name.Should().Be(testUser.Name);
        }

        [Test]
        public void GetReposForUserFromUrl_GetReposForUrl()
        {
            //Act
            _gitHubRepository.GetReposForUserFromUrl(RepoUrl);

            //Assert
            _mockHttpClientHelper.Verify(x => x.GetDataFromUrl<IEnumerable<Repo>>(RepoUrl), Times.Once);
        }

        [Test]
        public void GetReposForUserFromUrl_ReturnsRepos()
        {
            //Arrange
            var testRepos = Builder<Repo>.CreateListOfSize(1).Build();
            _mockHttpClientHelper.Setup(x => x.GetDataFromUrl<IEnumerable<Repo>>(It.IsAny<string>())).Returns(testRepos);

            //Act
            var repos = _gitHubRepository.GetReposForUserFromUrl(RepoUrl);

            //Assert
            repos.Should().BeEquivalentTo(testRepos);
        }
    }
}

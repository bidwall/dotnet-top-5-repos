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
        private const string Url = "url";


        public GitHubRepositoryTests()
        {
            _mockHttpClientHelper = Mock.Create<IHttpClientHelper>(Behavior.Strict);
            _gitHubRepository = new GitHubRepository(_mockHttpClientHelper);
        }

        [Test]
        public void GetDetailsForUser_ConstructsGitHubUrl()
        {
            //Arrange
            var url = $"{GitHubRepository.GitHubUri}/users/{UserName}";

            _mockHttpClientHelper.Arrange(x => x.GetDataFromUrl<User>(url)).Returns(new User());

            //Act
            _gitHubRepository.GetDetailsForUser(UserName);
        }

        [Test]
        public void GetReposForUserFromUrl_GetReposForUrl()
        {
            //Arrange
            _mockHttpClientHelper.Arrange(x => x.GetDataFromUrl<IEnumerable<Repo>>(Url)).Returns(new Repo[] {});

            //Act
            _gitHubRepository.GetReposForUserFromUrl(Url);
        }
    }
}

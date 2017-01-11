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
        [Test]
        public void GetDetailsForUser_ConstructsGitHubUrl()
        {
            //Arrange
            var mockHttpClientHelper = Mock.Create<IHttpClientHelper>(Behavior.Strict);
            var gitHubRepository = new GitHubRepository(mockHttpClientHelper);

            var userName = "user";
            var url = $"https://api.github.com/users/{userName}";

            mockHttpClientHelper.Arrange(x => x.GetDataFromUrl<User>(url)).Returns(new User());

            //Act
            gitHubRepository.GetDetailsForUser(userName);
        }

        [Test]
        public void GetReposForUserFromUrl_GetReposForUrl()
        {
            //Arrange
            var mockHttpClientHelper = Mock.Create<IHttpClientHelper>(Behavior.Strict);
            var gitHubRepository = new GitHubRepository(mockHttpClientHelper);

            var url = "url";

            mockHttpClientHelper.Arrange(x => x.GetDataFromUrl<IEnumerable<Repo>>(url)).Returns(new Repo[] {});

            //Act
            var repos = gitHubRepository.GetReposForUserFromUrl(url);
        }
    }
}

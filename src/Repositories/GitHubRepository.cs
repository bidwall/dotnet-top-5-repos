using System.Collections.Generic;
using HttpClientHelpers;
using Models;

namespace Repositories
{
    public class GitHubRepository : IRepository
    {
        private const string GitHubUri = "https://api.github.com";
        private readonly IHttpClientHelper _gitHubHelper;

        public GitHubRepository(IHttpClientHelper gitHubHelper)
        {
            _gitHubHelper = gitHubHelper;
        }

        public User GetDetailsForUser(string userName)
        {
            return _gitHubHelper.GetDataFromUrl<User>($"{GitHubUri}/users/{userName}");
        }

        public IEnumerable<Repo> GetReposForUserFromUrl(string url)
        {
            return _gitHubHelper.GetDataFromUrl<IEnumerable<Repo>>(url);
        }
    }
}
using System.Collections.Generic;
using Models;

namespace Repositories
{
    public interface IRepository
    {
        User GetDetailsForUser(string userName);
        IEnumerable<Repo> GetReposForUserFromUrl(string url);
    }
}
using System.Linq;
using System.Web.Mvc;
using Repositories;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly IRepository _repository;

        public UsersController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [OutputCache(Duration = 600, VaryByParam = "username")]
        public ActionResult Index(string username)
        {
            var user = _repository.GetDetailsForUser(username);

            if (user == null)
            {
                return RedirectToAction("NoResultsFound", new { message = $"{username} does not exists"});
            }

            var repos = _repository.GetReposForUserFromUrl(user.Repos_Url);

            var model = new UserViewModel
            {
                Name = user.Name,
                Location = user.Location,
                AvatarUrl = user.Avatar_url,
                Repos = repos.OrderByDescending(x => x.StarGazers_Count).Take(5).ToList()
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult NoResultsFound(string message)
        {
            return View(model: message);
        }

    }
}
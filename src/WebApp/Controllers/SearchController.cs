using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class SearchController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View(new SearchViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]        
        public ActionResult Index(SearchViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
           return RedirectToAction("Index", "Users", new { username = model.Username });
        }
    }
}
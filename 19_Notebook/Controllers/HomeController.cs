using _19_Notebook.Models;
using _19_Notebook.Services;
using Microsoft.AspNetCore.Mvc;

namespace _19_Notebook.Controllers
{
    public class HomeController : Controller
    {
        HomeModel HomeModel { get; }
        public HomeController(HomeModel homeModel)
        {
            HomeModel = homeModel;
        }

        public IActionResult Index() => View(HomeModel);
    }
}

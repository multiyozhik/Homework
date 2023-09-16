using _20_NotebookDb.Models;
using Microsoft.AspNetCore.Mvc;

namespace _20_NotebookDb.Controllers
{
    public class HomeController : Controller
    {
        HomeModel HomeModel { get; }
        public HomeController(HomeModel homeModel)
        {
            HomeModel = homeModel;
        }

        public IActionResult Index() => View(HomeModel);

        public IActionResult ContactInfo(Guid id)
            => View(HomeModel.Contacts.First(contact => contact.Id == id));
    }
}

using _20_NotebookDb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _20_NotebookDb.Controllers
{
    //в HomeController в конструктор подкладываем HomeModel и
    //следим за соответствием названий методов контроллера и названий View соотв. папки контроллера
    public class HomeController : Controller 
    {
        HomeModel HomeModel { get; }
        public HomeController(HomeModel homeModel)
        {
            HomeModel = homeModel;            
        }

        [HttpGet] //передача в главную страницу модели для отображения ее с-ва Contacts
        public IActionResult Index() => View(HomeModel);

        [HttpGet] //передача id чз строку запроса Home/GetContactInfo/@person.Id
        public IActionResult GetContactInfo(Guid id) 
            => View(HomeModel.Contacts.First(contact => contact.Id == id));

        [HttpGet] 
        public IActionResult GetCreatingContactView() => View();

        [HttpPost] //передача объекта типа Contact чз форму
        public RedirectToActionResult Create(Contact contact)
        {
            HomeModel.Add(contact);
            return RedirectToAction("index");
        }

        [HttpGet] //передача id чз строку запроса Home/GetChangingContactView/@person.Id
        public IActionResult GetChangingContactView(Guid id) 
            => View(HomeModel.Contacts.First(contact => contact.Id == id));

        //передача id из запроса и передача объекта типа Contact чз форму
        //здесь нужно обязательно вытаскивать id и передавать из запроса вручную в изменяемый контакт,
        //т.к. если параметром прокидывать только контакт, то Guid id нулевой и
        //соответственно дальше в HomeModel.Change не находит контакт с id 
        [HttpPost] 
        public RedirectToActionResult Change([FromQuery]Guid id, [FromForm]Contact newDataofChangingContact)
        {
            HomeModel.Change(newDataofChangingContact with { Id = id }); 
            return RedirectToAction("Index");
        }

        [HttpPost] //передача id чз строку запроса Home/DeleteContact/@person.Id
        public IActionResult DeleteContact(Guid id)
        {
            HomeModel.Delete(id);
            return RedirectToAction("Index");
        }
    }
}

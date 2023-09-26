﻿using _20_NotebookDb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

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
        public IActionResult Create(Contact contact)
        {
            if (IsValid(contact, out string errorMessage))
            {
                HomeModel.Add(contact);
                return RedirectToAction("index");
            }
            else
            {
                return Content(errorMessage);
            }
        }

        [HttpGet] //передача id чз строку запроса Home/GetChangingContactView/@person.Id
        public IActionResult GetChangingContactView(Guid id)
            => View(HomeModel.Contacts.First(contact => contact.Id == id));

        //передача id из запроса и передача объекта типа Contact чз форму
        //здесь нужно обязательно вытаскивать id и передавать из запроса вручную в изменяемый контакт,
        //т.к. если параметром прокидывать только контакт, то Guid id нулевой и
        //соответственно дальше в HomeModel.Change не находит контакт с id 
        [HttpPost]
        public IActionResult Change([FromQuery] Guid id, [FromForm] Contact newDataofChangingContact)
        {
            if (IsValid(newDataofChangingContact, out string errorMessage))
                {
                HomeModel.Change(newDataofChangingContact with { Id = id });
                return RedirectToAction("Index");
            }
            else
            {
                return Content(errorMessage);
            }
        }

        [HttpPost] //передача id чз строку запроса Home/DeleteContact/@person.Id
        public IActionResult DeleteContact(Guid id)
        {
            HomeModel.Delete(id);
            return RedirectToAction("Index");
        }

        [NonAction]
        public bool IsValid(Contact contact, out string errorMessages)
        {
            //здесь некоторые специфические проверки на стороне сервера приходящих данных с формы 
            if (contact.FamilyName?.Length < 3)
                ModelState.AddModelError(
                    "FamilyName",
                    "Фамилия должна содержать не менее 3 символов");
            if (contact.Adress is not null && contact.Adress.Length < 15)
                ModelState.AddModelError(
                    "Adress",
                    "Адрес должен содержать не менее 15 символов");
            string pattern = @"\+7\(\d{3}\)\d{3}-\d{4}";
            if (!Regex.IsMatch(contact.PhoneNumber, pattern))
                ModelState.AddModelError(
                    "PhoneNymber",
                    "Номер телефона должен соответствовать шаблону +7(ххх)ххх-хххх");

            errorMessages = "";
            if (!ModelState.IsValid) //при Invalid словаря ModelState пробегаем по всем его эл-там
            {
                foreach (var property in ModelState)
                {
                    if (property.Value.ValidationState == ModelValidationState.Invalid)
                    {
                        errorMessages = $"Ошибка в свойстве {property.Key}\n";
                        foreach (var error in property.Value.Errors)
                            errorMessages = $"{errorMessages} {error.ErrorMessage}\n";
                    }
                }
                return false;
            }
            else return true;
        }
    }
}

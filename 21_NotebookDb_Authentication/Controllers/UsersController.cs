using _21_NotebookDb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _21_NotebookDb.Controllers
{
    [Authorize(Policy = "OnlyForAdminRole")]
    public class UsersController: Controller
    {
        public UsersModel UserModel { get; }        

        public UsersController(UsersModel model)
        { 
            UserModel = model;
        }
                
        [HttpGet] //вывод списка пользователей (UserName, IsAdmin, возможности DeleteUser)
        public IActionResult Index()
        {
            UserModel.UpdateUsers();
            return View(UserModel);
        }

        [Route("api/users")]    //api users метод
        [HttpGet] 
        public async Task<IEnumerable<UserModel>> GetUsers()
        {
            UserModel.UpdateUsers();
            return UserModel.Users;
        }
                
        [HttpPost] //удаление пользователя администратором
        public async Task<IActionResult> DeleteUser(string id)
        {
            await UserModel.DeleteUser(id);            
            return RedirectToAction("Index");
        }

        [Route("api/delete/{id}")]  //!!! тут очень важно параметр id передать
        [HttpPost]                      //  api delete метод
        public async Task Delete(string id)
        {
            await UserModel.DeleteUser(id);
        }
    }
}

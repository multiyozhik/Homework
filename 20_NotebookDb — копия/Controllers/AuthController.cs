using _21_AuthNotebookDb.Autherization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace _21_AuthNotebookDb.Controllers
{
    [Authorize]
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;

        public AuthController(UserManager<AppUser> userManager,
                                SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(model);
            //}
            //var claims = new List<Claim>()
            //{
            //    new Claim("Demo", "Value")
            //};
            //var claimsIdentity = new ClaimsIdentity(claims);
            //var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            //await HttpContext.SignInAsync("Cookie", claimsPrincipal);
            //return Redirect(model.ReturnUrl);

            if (ModelState.IsValid)
            {
                var loginResult = await signInManager.PasswordSignInAsync(model.LoginProp,
                    model.Password,
                    false,
                    lockoutOnFailure: false);

                if (loginResult.Succeeded)
                {
                    if (Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }

                    return RedirectToAction("Index", "Home");
                }

            }

            ModelState.AddModelError("", "Пользователь не найден");
            return View(model);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser { UserName = model.LoginProp };
                var createResult = await userManager.CreateAsync(user, model.Password);

                if (createResult.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var identityError in createResult.Errors)
                    {
                        ModelState.AddModelError("", identityError.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Cookie");
            return Redirect("Home/Index");
        }
    }
}

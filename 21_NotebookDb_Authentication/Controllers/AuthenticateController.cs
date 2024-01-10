
using _21_NotebookDb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace _21_NotebookDb.Controllers
{
    //класс-контроллер аутентификации с методами Register, Login и Logout
    //(на вход [FromBody] данные формы)
    
    public class AuthenticateController: Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthenticateController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginModel()
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var loginResult = await _signInManager.PasswordSignInAsync(model.Username,
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
                var user = new ApplicationUser { UserName = model.UserName };
                var createResult = await _userManager.CreateAsync(user, model.Password);

                if (createResult.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
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
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }


    //private readonly UserManager<ApplicationUser> userManager;
    //private readonly RoleManager<IdentityRole> roleManager;
    //private readonly IConfiguration _configuration;

    //public AuthenticateController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
    //{
    //    this.userManager = userManager;
    //    this.roleManager = roleManager;
    //    _configuration = configuration;
    //}

    //[HttpGet]

    //public IActionResult Login(string returnUrl)
    //{            
    //    return View(new LoginModel()
    //    { 
    //        ReturnUrl = returnUrl
    //    });
    //}


    //[HttpPost]
    //public async Task<IActionResult> Login([FromBody] LoginModel model)
    //{
    //    var user = await userManager.FindByNameAsync(model.Username);
    //    if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
    //    {
    //        var userRoles = await userManager.GetRolesAsync(user);

    //        var authClaims = new List<Claim>
    //        {
    //            new Claim(ClaimTypes.Name, user.UserName),
    //            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
    //        };

    //        foreach (var userRole in userRoles)
    //        {
    //            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
    //        }

    //        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

    //        var token = new JwtSecurityToken(
    //            issuer: _configuration["JWT:ValidIssuer"],
    //            audience: _configuration["JWT:ValidAudience"],
    //            expires: DateTime.Now.AddHours(3),
    //            claims: authClaims,
    //            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
    //            );

    //        return Ok(new
    //        {
    //            token = new JwtSecurityTokenHandler().WriteToken(token),
    //            expiration = token.ValidTo
    //        });
    //    }
    //    return Unauthorized();
    //}

    //[HttpGet]    
    //public IActionResult Register()
    //{
    //    return View(new RegisterModel());
    //}

    //[HttpPost]        
    //public async Task<IActionResult> Register([FromBody] RegisterModel model)
    //{
    //    var userExists = await userManager.FindByNameAsync(model.UserName);
    //    if (userExists != null)
    //        return StatusCode(
    //            StatusCodes.Status500InternalServerError, 
    //            new ResponseAfterRegistration { 
    //                Status = "Error", 
    //                Message = "User already exists!" });

    //    ApplicationUser user = new ApplicationUser()
    //    {
    //        PasswordHash = model.Password,
    //        SecurityStamp = Guid.NewGuid().ToString(),
    //        UserName = model.UserName
    //    };
    //    var result = await userManager.CreateAsync(user, model.Password);
    //    if (!result.Succeeded)
    //        return StatusCode(
    //            StatusCodes.Status500InternalServerError, 
    //            new ResponseAfterRegistration { 
    //                Status = "Error", 
    //                Message = "User creation failed! Please check user details and try again." });

    //    return Ok(new ResponseAfterRegistration { 
    //        Status = "Success", 
    //        Message = "User created successfully!" });
    //}

    //контроллер для тестирования работы авторизации для роли админа

    //[HttpPost]
    //[Route("register-admin")]
    //public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
    //{
    //    var userExists = await userManager.FindByNameAsync(model.UserName);
    //    if (userExists != null)
    //        return StatusCode(
    //            StatusCodes.Status500InternalServerError, 
    //            new ResponseAfterRegistration { 
    //                Status = "Error", 
    //                Message = "User already exists!" });

    //    ApplicationUser user = new ApplicationUser()
    //    {
    //        Email = model.Email,
    //        SecurityStamp = Guid.NewGuid().ToString(),
    //        UserName = model.UserName
    //    };
    //    var result = await userManager.CreateAsync(user, model.Password);
    //    if (!result.Succeeded)
    //        return StatusCode(
    //            StatusCodes.Status500InternalServerError, 
    //            new ResponseAfterRegistration { 
    //                Status = "Error", 
    //                Message = "User creation failed! Please check user details and try again." });

    //    if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
    //        await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
    //    if (!await roleManager.RoleExistsAsync(UserRoles.Authorized))
    //        await roleManager.CreateAsync(new IdentityRole(UserRoles.Authorized));
    //    if (!await roleManager.RoleExistsAsync(UserRoles.Anonim))
    //        await roleManager.CreateAsync(new IdentityRole(UserRoles.Anonim));

    //    if (await roleManager.RoleExistsAsync(UserRoles.Admin))
    //    {
    //        await userManager.AddToRoleAsync(user, UserRoles.Admin);
    //    }

    //    return Ok(new ResponseAfterRegistration { 
    //        Status = "Success", 
    //        Message = "User created successfully!" });
    //}
}


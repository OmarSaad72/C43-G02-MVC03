using IKEA.DAL.Models.Identity;
using IKEA.PL.View_Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;

namespace IKEA.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            if (ModelState.IsValid)
            {
                var User = new AppUser()
                {
                    UserName = register.Email.Split('@')[0],
                    Email = register.Email,
                    FName = register.FName,
                    LName = register.LName,
                    IsAgree = register.IsAgree
                };
                var result = await _userManager.CreateAsync(User, register.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("LogIn");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(register);
        }
        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(LogInVM logIn)
        {
            if (ModelState.IsValid)
            {
                var User = await _userManager.FindByEmailAsync(logIn.Email); // Check user with same email or not
                if (User != null)
                {
                    var Result = await _userManager.CheckPasswordAsync(User, logIn.Password);
                    if (Result == true) // Email And Password Is Correct
                    {
                        var result = await _signInManager.PasswordSignInAsync(User, logIn.Password, logIn.RememberMe, false);
                        if (result.Succeeded)
                            return RedirectToAction("Index", "Home");
                    }
                    else // Email Is True But Password Is False
                        ModelState.AddModelError(string.Empty, "Password Is Not Found");
                }
                else
                    ModelState.AddModelError(string.Empty, "Email Is Not Found");
            }
            return View(logIn);
        }
        [HttpGet]
        public new/*Masking*/ async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("LogIn");
        }
        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }
        public async Task<IActionResult> SendResetPasswordUrl(ForgetPasswordVM passwordVM)
        {
            if (ModelState.IsValid)
            {
                var User = await _userManager.FindByEmailAsync(passwordVM.Email); // find user exist or not
                if (User != null)
                {
                    // Create Email
                    var token = await _userManager.GeneratePasswordResetTokenAsync(User);
                    var url = Url.Action("ResetPassword", "Account", new { email = passwordVM.Email, token = token }, Request.Scheme);
                    var email = new Email()
                    {
                        To = passwordVM.Email,
                        Subject = "Reset Your Password",
                        Body = url // URL
                    };
                    // Send Email

                }
                ModelState.AddModelError(string.Empty, "Invalid Email");
            }
            return View(passwordVM);
        }
    }
}
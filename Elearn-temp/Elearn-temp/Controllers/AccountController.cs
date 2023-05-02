using Elearn_temp.Models;
using Elearn_temp.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Elearn_temp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager; // user crate etmek ucundur

        private readonly SignInManager<AppUser> _signInManager; //  sayita giris etmek logout olmaq ucun istifade olunur

        public AccountController(UserManager<AppUser> userManager,
                               SignInManager<AppUser> signInManager)
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            AppUser newUser = new()
            {
                UserName = model.Username,
                Email = model.Email,
                FullName = model.FullName,
            };

            IdentityResult result = await _userManager.CreateAsync(newUser, model.Password); // passwordu colden ediriki onu haslaya bilsin yeni kodlari baxmaq olmur 


            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }

                return View(model);
            }




            return RedirectToAction(nameof(Login));

        }



        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            AppUser user = await _userManager.FindByEmailAsync(model.EmailOrUsername);  // burda yoxlayirki email ila daxil olubdu

            if (user is null)  // eger tapa bilmedise nuldusa girir serti yoxlayir
            {
                user = await _userManager.FindByNameAsync(model.EmailOrUsername);  // burda yoxlayirki username ile daxil olubdu

            }

            if (user is null)
            {
                ModelState.AddModelError(string.Empty, "Email or password is wrong");  // eger bele bir email tapilmasa bu eroro cixart
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Email or password is wrong");  // eger bele succesd deyilse bu eroro cixart
                return View(model);

            }

            return RedirectToAction("Index", "Home");
        }




        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();  // bu deyirki gedib logouta clicik edende bu methodu islet yeni cixsin logout etsin
            return RedirectToAction("Index", "Home");
        }




    }
}

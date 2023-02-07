using Cresk.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Cresk.ViewModels.Login;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.RegularExpressions;

namespace Cresk.Controllers
{

    public class AccountController : Controller
    {
        private readonly CreskContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(CreskContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _context = context; 
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            LoginViewModel vm = new LoginViewModel();
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel vm)
        {
            var result = await _signInManager.PasswordSignInAsync(vm.UserName, vm.Password, false, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["LoginFlag"] = "Invalid usermane or Password";
                return View(vm);
            }
        }
        public async Task<ActionResult> Register()
        {
            AccountRegisterViewModel vm = new AccountRegisterViewModel();
            var companies = await _context.Companies.ToListAsync();
            vm.CompanyList = companies.Select(company => new SelectListItem()
            {
                Value = company.Id,
                Text = company.CompanyName
            }).ToList();
            return View(vm);
        }
        [HttpPost]
        public async Task<ActionResult> Register(AccountRegisterViewModel vm)
        {
            var user = new IdentityUser()
            {
                UserName = vm.Email, Email = vm.Email
            };
            var result = await _userManager.CreateAsync(user,vm.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home"); 
            }
            else
            {

                var hasNumber = new Regex(@"[0-9]+");
                var hasUpperChar = new Regex(@"[A-Z]+");
                var hasMinimum8Chars = new Regex(@".{8,}");
                var emailValidation = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

                if (vm.Password != vm.Password2)
                {
                    ViewData["RegisterPasswordFlag"] = "Passwords are not the same";
                }
                else if (!hasNumber.IsMatch(vm.Password) || !hasUpperChar.IsMatch(vm.Password) || !hasMinimum8Chars.IsMatch(vm.Password))
                {
                    ViewData["RegisterPasswordFlag"] = "Password is not valid";
                }
                else if (!emailValidation.IsMatch(vm.Email))
                {
                    ViewData["RegisterPasswordFlag"] = "E-mail is no valid";
                }
                var companies = await _context.Companies.ToListAsync();
                vm.CompanyList = companies.Select(company => new SelectListItem()
                {
                    Value = company.Id,
                    Text = company.CompanyName
                }).ToList();
                return View(vm);
            }
            return RedirectToAction("Login", "Account");
        }
        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}

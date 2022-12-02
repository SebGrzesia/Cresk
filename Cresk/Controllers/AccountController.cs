using Cresk.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Cresk.ViewModels.Login;

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
        public async Task<ActionResult> Login(LoginViewModel vm)
        {
            var result = await _signInManager.PasswordSignInAsync(vm.UserName, vm.Password, false, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(vm);
            }
        }
        public ActionResult Register()
        {
            AccountRegisterViewModel vm = new AccountRegisterViewModel();
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

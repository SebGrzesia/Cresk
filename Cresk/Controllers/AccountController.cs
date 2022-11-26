using Cresk.Data;
using Microsoft.AspNetCore.Mvc;
using Cresk.ViewModels;

namespace Cresk.Controllers
{
    public class AccountController : Controller
    {
        private readonly CreskContext _context;

        public AccountController(CreskContext context)
        {
            _context = context;
        }
        public ActionResult Login()
        {
            LoginViewModel vm = new LoginViewModel();
            return View(vm);
        }
        [HttpPost]
        public ActionResult Login(ViewModels.LoginViewModel vm)
        {
            return RedirectToAction("Index", "Home");
        }
    }
}

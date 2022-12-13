using Microsoft.AspNetCore.Mvc;

namespace Cresk.Controllers
{
    public class CompanyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

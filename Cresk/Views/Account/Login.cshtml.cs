using Microsoft.AspNetCore.Mvc;
using Cresk.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cresk.Views.Account
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public User User { get; set; }
        public void OnGet()
        {
            this.User = new User { UserName = "admin" };
        }
        public void OnPost()
        {

        }
    }
}

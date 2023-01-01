using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Cresk.ViewModels.Login
{
    public class AccountRegisterViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password2 { get; set; }

        public List<SelectListItem> CompanyList { get; set; }
        public string CompanyId { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;

namespace Cresk.ViewModels.Company
{
    public class CreateCompanyViewModel
    {
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
    }
}

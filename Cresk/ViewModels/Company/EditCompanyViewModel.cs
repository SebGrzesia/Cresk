using System.ComponentModel.DataAnnotations;

namespace Cresk.ViewModels.Company
{
    public class EditCompanyViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
    }
}

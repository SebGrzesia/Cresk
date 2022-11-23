using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cresk.ViewModels
{
    public class CreateDbTagViewModel
    {
        [Display(Name="Title")]
        public string Name { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}

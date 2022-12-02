using System.ComponentModel.DataAnnotations;

namespace Cresk.ViewModels.Category
{
    public class EditCategoryViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Title")]
        public string Name { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}

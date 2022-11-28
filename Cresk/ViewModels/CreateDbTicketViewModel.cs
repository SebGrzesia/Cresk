using Cresk.Enums;
using Cresk.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cresk.ViewModels
{
    public class CreateDbTicketViewModel
    {
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Email address")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is invalid")]
        public List<SelectListItem> TagList { get; set; }
        public string TagId { get; set; }
        public string Email { get; set; }
        public TicketPriority Priority { get; set; }
    }
 
}

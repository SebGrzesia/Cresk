using Cresk.Enums;
using Cresk.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Cresk.ViewModels.Tickets
{
    public class EditDbTicketViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Category")]
        public List<SelectListItem> CategoryList { get; set; }
        public string? CategoryId { get; set; }
        [Display(Name = "Email address")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email is invalid")]
        public string Email { get; set; }
        public TicketStatus Status { get; set; }
        public TicketPriority Priority { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy HH:mm}")]
        public DateTime CreateDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy HH:mm}")]
        public DateTime ModifyDate { get; set; }
    }
}

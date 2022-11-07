using Cresk.Enums;
using System.ComponentModel.DataAnnotations;

namespace Cresk.ViewModels
{
    public class EditDbTicketViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "E-mail adress")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid e-mail adress")]
        public string Email { get; set; }
        public TicketStatus Status { get; set; }
        public TicketPriority Priority { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifyDate { get; set; }
    }
}

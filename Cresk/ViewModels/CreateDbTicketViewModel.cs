using Cresk.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cresk.ViewModels
{
    public class CreateDbTicketViewModel
    {
        [Display(Name = "Tytuł zgłoszenia")]
        public string Title { get; set; }
        [Display(Name = "Display")]
        public string Description { get; set; }
        [Display(Name = "Adres email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail jest nieprawidłowy")]
        public string Email { get; set; }
        public TicketPriority Priority { get; set; }
    }
}

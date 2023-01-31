using Cresk.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Cresk.Models
{
    public class DbTicket
    {
        [Display(Name = "Id zgłoszenia")]
        [Key] 
        public string Id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TicketDisplayNumber { get; set; }
        [Display(Name = "Tytuł zgłoszenia")]
        public string Title { get; set; }
        public string? CategoryId { get; set; }
        public TicketCategory? Category { get; set; }
        [Display(Name = "Opis zgłoszenia")]
        public string Description { get; set; }
        [Display(Name = "Adres email")]
        [DataType(DataType.EmailAddress, ErrorMessage ="E-mail jest nieprawidłowy")]
        public string Email { get; set; }
        public TicketStatus Status { get; set; }
        [Required]
        public TicketPriority Priority { get; set; }
        [Display(Name = "Data stworzenia")]
        public DateTime CreatedDate { get; set; }
        [Display(Name = "Data modyfikacji")]
        public DateTime ModifyData { get; set; }

        //public bool ShowResolvedTickets { get; set; }
        public DbTicket()
        {
            //this.ShowResolvedTickets = false;
            this.Id = Guid.NewGuid().ToString();
        }
    }
}

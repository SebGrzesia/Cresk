using Cresk.Enums;
using System.ComponentModel.DataAnnotations;

namespace Cresk.ViewModels
{
    public class IndexDbTicketViewModel
    {

        public string Title { get; set; }
        public string Desctiption { get; set; }
        public string EmailAdress { get; set; }
        public TicketStatus TicketStatus { get; set; }
        public TicketPriority TicketPriority { get; set; }
        public DateTime ModifyDate { get; set; }
    }
}

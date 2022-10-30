using Cresk.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cresk.ViewModels
{
    public class TicketStatusViewModel
    {
        public List<DbTicket>? DbTickets { get; set; }
        public SelectList? Status { get; set; }
        public string? DbTicketStatus { get; set; }
        public string? SearchString { get; set; }
    }
}

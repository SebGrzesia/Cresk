using Cresk.Enums;
using Cresk.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cresk.ViewModels
{
    public class TicketStatusViewModel
    {
        public List<IndexDbTicketViewModel> IndexDbTicketViewModels { get; set; }
        public string SearchString { get; set; } = string.Empty;
        public TicketStatus? TicketStatus { get; set; }
    }
}

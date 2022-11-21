using Cresk.Enums;

namespace Cresk.ViewModels
{
    public class TicketPriorityViewModel
    {
        public List<IndexDbTicketViewModel> indexDbTicketViewModels { get; set; }
        public string SearchString { get; set; } = string.Empty;
        public TicketPriority TicketPriority { get; set; }
    }
}

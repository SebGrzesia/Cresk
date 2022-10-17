using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cresk.Enums
{
    public enum TicketStatus
    {
        New,
        [Display(Name ="In progress")]
        InProgress,
        [Display(Name ="Waiting reply")]
        WaitingReply,
        Replied,
        Resolved
    }
}

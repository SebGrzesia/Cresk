using System.ComponentModel;

namespace Cresk.Models
{
    public enum TicketStatus
    {
        New,
        [Description("In progress")]
        InProgress,
        [Description("Waiting reply")]
        WaitingReply,
        Replied,
        Resolved
    }
}

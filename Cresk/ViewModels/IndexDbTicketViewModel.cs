using Cresk.Enums;
using Cresk.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cresk.ViewModels
{
    public class IndexDbTicketViewModel
    {
        public string Id { get; set; }
        [Display(Name = "ID")]
        public int TicketDisplayNumber { get; set; }
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Tags")]
        public string TagName { get; set; }
        [Display(Name = "Email address")]
        public string EmailAddress { get; set; }
        [Display(Name = "Status")]
        public TicketStatus TicketStatus { get; set; }
        [Display(Name = "Priority")]
        public TicketPriority TicketPriority { get; set; }
        [Display(Name = "Last change")]
        public DateTime ModifyDate { get; set; }
    }

}

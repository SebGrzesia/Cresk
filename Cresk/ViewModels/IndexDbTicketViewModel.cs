﻿using Cresk.Enums;
using System.ComponentModel.DataAnnotations;

namespace Cresk.ViewModels
{
    public class IndexDbTicketViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
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

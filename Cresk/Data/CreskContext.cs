using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Cresk.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Cresk.Data
{
    public class CreskContext : IdentityDbContext
    {
        public CreskContext (DbContextOptions<CreskContext> options)
            : base(options)
        {
        }


        public DbSet<Cresk.Models.DbTicket> DbTicket { get; set; } = default!;

        public DbSet<Cresk.Models.TicketCategory> TicketCategories { get; set; }

        public DbSet<Cresk.Models.Company> Companies { get; set; }
    }
}

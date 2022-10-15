using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Cresk.Models;

namespace Cresk.Data
{
    public class CreskContext : DbContext
    {
        public CreskContext (DbContextOptions<CreskContext> options)
            : base(options)
        {
        }

        public DbSet<Cresk.Models.DbTicket> DbTicket { get; set; } = default!;

        public DbSet<Cresk.Models.DbTag> DbTag { get; set; }

    }
}

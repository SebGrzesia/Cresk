using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Cresk.Data;
using System;
using System.Linq;

namespace Cresk.Models
{
    public static class BasicCategoryData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new CreskContext(serviceProvider.GetRequiredService<DbContextOptions<CreskContext>>()))
            {
                if (context.TicketCategories.Any())
                {
                    return;
                }
                context.TicketCategories.AddRange(
                    new TicketCategory
                    {
                        Name = "Dostępny",
                        Description = "Wszystko związane z dostępami do badań"
                    },
                    new TicketCategory
                    {
                        Name = "Sprzęt",
                        Description = "Wymiana sprzętu komputerowego, wymiana na nowy"
                    },
                    new TicketCategory
                    {
                        Name = "LabOne",
                        Description = "Sprawy związane ze stroną LabOne",
                    },
                    new TicketCategory
                    {
                        Name = "Bledy",
                        Description = "Bledy znalezione w naszym software"
                    }
                    );
                context.SaveChanges();
            }
        }
    }
}

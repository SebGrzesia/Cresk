using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Cresk.Data;
using System;
using System.Linq;

namespace Cresk.Models
{
    public static class BasicTagData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new CreskContext(serviceProvider.GetRequiredService<DbContextOptions<CreskContext>>()))
            {
                if (context.DbTag.Any())
                {
                    return;
                }
                context.DbTag.AddRange(
                    new DbTag
                    {
                        Name = "Dostępny",
                        Description = "Wszystko związane z dostępami do badań"
                    },
                    new DbTag
                    {
                        Name = "Sprzęt",
                        Description = "Wymiana sprzętu komputerowego, wymiana na nowy"
                    },
                    new DbTag
                    {
                        Name = "LabOne",
                        Description = "Sprawy związane ze stroną LabOne",
                    },
                    new DbTag
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

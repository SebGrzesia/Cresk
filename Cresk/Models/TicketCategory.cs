using System.ComponentModel.DataAnnotations;

namespace Cresk.Models
{
    public class TicketCategory
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public TicketCategory()
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}

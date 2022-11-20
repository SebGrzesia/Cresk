using System.ComponentModel.DataAnnotations;

namespace Cresk.Models
{
    public class DbTag
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public DbTag()
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}

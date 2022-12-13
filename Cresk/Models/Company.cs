namespace Cresk.Models
{
    public class Company
    {
        public string Id { get; set; }
        public string CompanyName { get; set; }
        public Company()
        {
            this.Id = Guid.NewGuid().ToString();
        }

    }
}

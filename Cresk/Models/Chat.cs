namespace Cresk.Models
{
    public class Chat
    {
        public string Id { get; set; }
        public DbTicket DbTicket { get; set; }
        public string DbTicketId { get; set; }
        public Chat()
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}

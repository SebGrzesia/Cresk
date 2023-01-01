using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Cresk.Models
{
    public class ChatMessage
    {
        public string Id { get; set; }
        public string Message { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy HH:mm}")]
        public DateTime CreateTime { get; set; }
        public string Username { get; set; }
        public string UserId { get; set; }
        public IdentityUser User {get; set;}
        public string ChatId { get; set; }
        public Chat Chat { get; set; }
        public ChatMessage()
        {
            this.Id = Guid.NewGuid().ToString();
        }
    }
}

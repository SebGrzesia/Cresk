using System.ComponentModel.DataAnnotations;

namespace Cresk.ViewModels.Chat
{
    public class ChatMessageViewModel
    {
        public string Message { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy HH:mm}")]
        public DateTime CreateTime { get; set; }
        public string Username { get; set; }
    }
}

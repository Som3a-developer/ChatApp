using System.Collections.Generic;

namespace ChatApp.Models
{
    public class PrivateChatViewModel
    {
        public string ReceiverId { get; set; }
        public string ReceiverDisplayName { get; set; }
        public List<MessageModel> Messages { get; set; }
    }
}
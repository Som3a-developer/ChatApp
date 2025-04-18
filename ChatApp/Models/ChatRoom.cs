using System.Collections.Generic;

namespace ChatApp.Models
{
    public class ChatRoom
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<MessageModel> Messages { get; set; } = new List<MessageModel>();
    }
}
// Models/ChatRoom.cs
namespace ChatApp.Models
{
    public class ChatRoom
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<MessageModel> Messages { get; set; } = new List<MessageModel>();
    }
}
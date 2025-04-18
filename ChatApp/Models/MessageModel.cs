using System.ComponentModel.DataAnnotations;

namespace ChatApp.Models
{
    public class MessageModel
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime Timestamp { get; set; }

        [Required]
        public string SenderId { get; set; }
        public ApplicationUser Sender { get; set; }

        public int ChatRoomId { get; set; }
        public ChatRoom ChatRoom { get; set; }
    }
}
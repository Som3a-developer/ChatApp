using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.Models
{
    public class MessageModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string SenderId { get; set; }

        public string ReceiverId { get; set; } // Nullable for group messages

        public int? ChatRoomId { get; set; } // Nullable for private messages

        [Required]
        public string Content { get; set; }

        public DateTime SentAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("SenderId")]
        public ApplicationUser Sender { get; set; }

        [ForeignKey("ReceiverId")]
        public ApplicationUser Receiver { get; set; }

        [ForeignKey("ChatRoomId")]
        public ChatRoom ChatRoom { get; set; }
    }
}
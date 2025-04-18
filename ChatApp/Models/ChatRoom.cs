using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChatApp.Models
{
    public class ChatRoom
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<MessageModel> Messages { get; set; } = new List<MessageModel>();
    }
}
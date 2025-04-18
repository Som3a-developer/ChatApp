using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ChatApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public string AvatarUrl { get; set; }
        public ICollection<MessageModel> Messages { get; set; }
    }
}
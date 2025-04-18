using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChatApp.Models
{
    public class ApplicationUser : IdentityUser
    {

        [Required]
        public string DisplayName { get; set; }
        public ICollection<MessageModel> Messages { get; set; }
    }
}
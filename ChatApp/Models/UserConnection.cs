// Models/UserConnection.cs
namespace ChatApp.Models
{
    public class UserConnection
    {
        public string ConnectionId { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
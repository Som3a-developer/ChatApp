using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using ChatApp.Data;
using ChatApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ChatApp.SignalRHub
{
    [Authorize]
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext _context;

        public ChatHub(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SendPrivateMessage(string receiverId, string message)
        {
            var senderId = Context.UserIdentifier;
            var sender = await _context.Users.FirstOrDefaultAsync(u => u.Id == senderId);
            var receiver = await _context.Users.FirstOrDefaultAsync(u => u.Id == receiverId);

            if (receiver == null || sender == null)
            {
                return;
            }

            var messageModel = new MessageModel
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Content = message,
                SentAt = DateTime.UtcNow
            };

            _context.Messages.Add(messageModel);
            try
            {
                await _context.SaveChangesAsync();
                Console.WriteLine("Message saved to database.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save message: {ex.Message}");
                return;
            }

            await Clients.User(receiverId).SendAsync("ReceiveMessage", sender.DisplayName, message, messageModel.SentAt.ToString("o"));
            await Clients.User(senderId).SendAsync("ReceiveMessage", sender.DisplayName, message, messageModel.SentAt.ToString("o"));
        }
    }
}
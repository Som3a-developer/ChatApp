using Microsoft.AspNetCore.SignalR;
using ChatApp.Data;
using ChatApp.Models;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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

            if (receiver == null)
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
            await _context.SaveChangesAsync();

            await Clients.User(receiverId).SendAsync("ReceiveMessage", sender.DisplayName, message, messageModel.SentAt);
            await Clients.User(senderId).SendAsync("ReceiveMessage", sender.DisplayName, message, messageModel.SentAt);
        }
    }
}
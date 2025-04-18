using Microsoft.AspNetCore.SignalR;
using ChatApp.Models;
using ChatApp.Data;

namespace ChatApp.SignalRHub
{
    public class ChatHub : Hub<IChatClient>
    {
        private readonly ApplicationDbContext _context;

        public ChatHub(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SendMessage(string userId, int roomId, string content)
        {
            var message = new MessageModel
            {
                SenderId = userId,
                ChatRoomId = roomId,
                Content = content,
                Timestamp = DateTime.Now
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            await Clients.Group(roomId.ToString()).ReceiveMessage(message);
        }

        public async Task JoinRoom(int roomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId.ToString());
        }
    }
}
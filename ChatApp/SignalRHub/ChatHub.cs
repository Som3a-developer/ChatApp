using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ChatApp.SignalRHub
{
    [Authorize]
    public class ChatHub : Hub<IChatClient>
    {
        public async Task JoinRoom(string roomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
            await Clients.Group(roomId).ReceiveMessage("System", $"User {Context.User.Identity.Name} joined the room.");
        }

        public async Task SendMessage(string roomId, string message)
        {
            await Clients.Group(roomId).ReceiveMessage(Context.User.Identity.Name, message);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            // Ì„ﬂ‰ ≈÷«›… „‰ÿﬁ ·≈“«·… «·„” Œœ„ „‰ «·„Ã„Ê⁄« 
            await base.OnDisconnectedAsync(exception);
        }
    }
}
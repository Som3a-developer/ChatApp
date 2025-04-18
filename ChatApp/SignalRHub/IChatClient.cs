public interface IChatClient
{
    Task NewUserJoined(string userName);
    Task ReceiveMessage(string fromUser, string message);
    Task GetConnectedUsers(List<string> users);
}

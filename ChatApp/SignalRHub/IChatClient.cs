namespace ChatApp.SignalRHub
{
    public interface IChatClient
    {
        Task ReceiveMessage(string user, string message);
    }
}
using Microsoft.AspNetCore.SignalR;

namespace pustokApp.Hubs;

public class ChatPractiseHub:Hub
{
    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}
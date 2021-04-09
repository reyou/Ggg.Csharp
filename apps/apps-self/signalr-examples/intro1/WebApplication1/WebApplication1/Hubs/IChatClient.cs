using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace WebApplication1.Hubs
{
    public interface IChatClient
    {
        Task ReceiveMessage(string user, string message);
        Task ReceiveMessage(string message);
        Task SendAsync(string receiveMessage, string message);
    }

    class ChatClient : IChatClient
    {
        public async Task ReceiveMessage(string user, string message)
        {
            throw new NotImplementedException();
        }

        public async Task ReceiveMessage(string message)
        {
            throw new NotImplementedException();
        }

        public async Task SendAsync(string receiveMessage, string message)
        {
            throw new NotImplementedException();
        }
    }

    public class StronglyTypedChatHub : Hub<IChatClient>
    {
        public override async Task OnConnectedAsync()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnDisconnectedAsync(exception);
        }

        public async Task ReceiveMessage(string user, string message)
        {
            await Clients.User(user).ReceiveMessage("ReceiveMessage", message);
        }

        public async Task ReceiveMessage(string message)
        {
            await Clients.All.ReceiveMessage(  message);
        }

        [HubMethodName("SendMessageToUser")]
        public Task DirectMessage(string user, string message)
        {
            return Clients.User(user).SendAsync("ReceiveMessage", message);

        }
    }
}

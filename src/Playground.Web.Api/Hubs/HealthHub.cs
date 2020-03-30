using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Playground.Web.API.Hubs
{
    public class HealthHub : Hub
    {
        public HealthHub()
        {
        }

        public Task SendMessage(string message)
        {
            return this.Clients.Others.SendAsync("Receive", message);
        }

        public static async Task SendMessage(string message, IHubClients clients)
        {
            await clients.All.SendAsync("Receive", message);
        }

        public async override Task OnConnectedAsync()
        {
            await this.Clients.All.SendAsync("Receive", "A client has connected");

            await base.OnConnectedAsync();
        }

        public async override Task OnDisconnectedAsync(Exception exception)
        {
            await this.Clients.All.SendAsync("Receive", "A client has disconnected");

            await base.OnDisconnectedAsync(exception);
        }
    }
}
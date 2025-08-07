using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
namespace Horas.Api.Hubs
{
    public class ChatHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            Console.WriteLine($"New connection: {Context.ConnectionId}");
            await base.OnConnectedAsync();
        }

        public async Task SendMessage(Guid customerId, Guid supplierId, string message)
        {
            var groupName = GetGroupName(customerId, supplierId);
         
            await Clients.Group(groupName).SendAsync("ReceiveMessage", new
            {
                body = message,
                customerId,
                supplierId
            });
        }


        //public async Task SendMessagetoall(string user, string message)
        //{
        //    await Clients.All.SendAsync("ReceiveMessage", user, message);
        //}
        public async Task JoinGroup(Guid customerId, Guid supplierId)
        {
            var groupName = GetGroupName(customerId, supplierId);
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            // Send welcome message to the joined user
            await Clients.Caller.SendAsync("ReceiveMessage", new
            {
                body = "🎉 Welcome to the chat group!",
                customerId,
                supplierId
            });
        }

        public static string GetGroupName(Guid customerId, Guid supplierId)
        {
            return customerId.CompareTo(supplierId) < 0
                ? $"{customerId}_{supplierId}"
                : $"{supplierId}_{customerId}";
        }

    }
}

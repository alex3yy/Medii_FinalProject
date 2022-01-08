using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Zaharia_Alexandru_Lab2.Hubs {

    [Authorize]
    public class ChatHub : Hub {

        public async Task SendMessage(string user, string firstName, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", Context.User.Identity.Name, firstName, message);
        }
    }
}
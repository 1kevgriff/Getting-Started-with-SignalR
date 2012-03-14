using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SignalR.Hubs;

namespace SEVNUG.Web.Hubs
{
    public class Chat : Hub
    {
        public void BroadcastMessage(string message)
        {
            //Clients.addMessage(message);

            var connectionId = Context.ConnectionId;
            Clients[connectionId].addMessage(message);

            this.AddToGroup("Admins");

            this.RemoveFromGroup("Admins");

            Clients["Admins"].addMessage(message);

        }
    }
}
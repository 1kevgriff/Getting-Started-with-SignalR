using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SignalR.Hubs;

namespace GettingStartedSignalR.TwilioApp.Hubs
{
    public class TwilioHub : Hub
    {
        public void WriteSMS(string from, string message)
        {
            Clients.addMessage(from, message);
        }
    }
}
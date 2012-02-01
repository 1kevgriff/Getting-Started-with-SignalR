using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SignalR.Hubs;

namespace GettingStartedSignalR.TwilioApp.Hubs
{
    public class TwilioHub : Hub
    {
        static TwilioHub()
        {
            Messages = new List<Message>();
        }

        public static List<Message> Messages { get; set; } 
        public void WriteSMS(string from, string message, string fromLocation)
        {
            var item = new Message()
                           {
                               Body = message, From = from, FromLocation = fromLocation
                           };
            Messages.Add(item);

            Clients.addMessage(from, message, item.FromLocation);
        }

        public void GetOld()
        {
            Messages.ForEach(p => Caller.addMessage(p.From, p.Body, p.FromLocation));
        }
    }

    public class Message
    {
        public string From { get; set; }
        public string FromLocation { get; set; }
        public string Body { get; set; }
    }
}
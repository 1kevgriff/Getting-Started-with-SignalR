using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using SignalR;
using SignalR.Hosting.AspNet;
using SignalR.Hubs;
using SignalR.Infrastructure;

namespace GettingStartedSignalR.Web.Hubs
{
    public class Chat : Hub
    {
        private static List<string> ConnectedClients { get; set; }
        static Chat()
        {
            ConnectedClients = new List<string>();

            StartRandomMessenger();
        }

        private static Task StartRandomMessenger()
        {
            return Task.Factory.StartNew(() =>
                                      {
                                          TimeSpan duration = new TimeSpan(0, 0, 0, 5);
                                          DateTime start = DateTime.Now;

                                          var random = new Random();
                                          while (true)
                                          {
                                              if (ConnectedClients.Count == 0) continue;
                                              if (DateTime.Now - start < duration) continue;
                                              start = DateTime.Now;

                                              IConnectionManager connectionManager =
                                                  AspNetHost.DependencyResolver.Resolve<IConnectionManager>();
                                              var clients = connectionManager.GetClients<Chat>();

                                              var randomIndex = random.Next(0, ConnectedClients.Count);
                                              var selected =
                                                  ConnectedClients[randomIndex];

                                              var o = clients[selected];

                                              o.addMessage(string.Format("You were selected at {0}",
                                                                         DateTime.Now.ToLongTimeString()));
                                          }
                                      });
        }

        public void Send(string msg)
        {
            if (!ConnectedClients.Contains(Context.ConnectionId))
                ConnectedClients.Add(Context.ConnectionId);

            // Call the addMessage method on all clients
            Clients.addMessage(msg);
        }
    }
}
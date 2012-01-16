using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GettingStartedSignalR.Web.Hubs;
using SignalR.Hubs;
using Twilio.TwiML;
using Twilio.TwiML.Mvc;

namespace GettingStartedSignalR.Web.Controllers
{
    public class TwilController : TwilioController
    {
        //
        // GET: /Twilio/

        public ActionResult HandleSMS(string From, string To, string Body)
        {
            var response = new TwilioResponse();
            response.Sms("You rock, thanks for playing.");

            var clients = Hub.GetClients<Chat>();
            clients.addMessage(string.Format("Message from {0}: {1}", From, Body));

            return TwiML(response);
        }

    }
}

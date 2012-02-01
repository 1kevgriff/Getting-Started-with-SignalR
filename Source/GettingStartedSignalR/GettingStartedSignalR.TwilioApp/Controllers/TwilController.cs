using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GettingStartedSignalR.TwilioApp.Hubs;
using SignalR.Hubs;
using Twilio;
using Twilio.TwiML.Mvc;

namespace GettingStartedSignalR.TwilioApp.Controllers
{
    [ValidateRequest("4967e4b302d2389c3feec3c19e6b43ba")]
    public class TwilController : Twilio.TwiML.Mvc.TwilioController
    {
        //
        // GET: /Twil/

        public ActionResult GetSMS(string From, string To, string SmsSid, string Body, string FromZip, string FromCity, string FromState, string FromCountry)
        {
            var twilio = new TwilioRestClient("ACa2de2b9a03db42ee981073b917cc8132", "4967e4b302d2389c3feec3c19e6b43ba");
            var message = twilio.GetSmsMessage(SmsSid);

            var clients = Hub.GetClients<TwilioHub>();
            var item = new Message()
                           {
                               Body = message.Body, From = message.From, FromLocation = string.Format("{0}, {1} {2} {3}", FromCity, FromState, FromZip, FromCountry)
                           };
            TwilioHub.Messages.Add(item);
            clients.addMessage(message.From, message.Body, item.FromLocation);

            return new EmptyResult();
        }

    }
}

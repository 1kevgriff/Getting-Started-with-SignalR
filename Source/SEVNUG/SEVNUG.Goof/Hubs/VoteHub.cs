using System.Collections.Generic;
using System.Diagnostics;
using SignalR.Hubs;

namespace SEVNUG.Goof.Hubs
{
    public class VoteHub : Hub
    {
        public static Dictionary<string, int> Votes { get; set; }
        public static Dictionary<string, string> VoteTracker { get; set; } 

        static VoteHub()
        {
            Votes = new Dictionary<string, int>();
            VoteTracker = new Dictionary<string, string>();
        }
        
        public void Vote(string voteFor)
        {
            var connectionId = Context.ConnectionId;

            if (VoteTracker.ContainsKey(connectionId)) // CHANGE VOTE!
                Votes[VoteTracker[connectionId]]--;

            if (Votes.ContainsKey(voteFor))
                Votes[voteFor]++;
            else
                Votes[voteFor] = 1;

            VoteTracker[connectionId] = voteFor;

            List<WijPieChartSeriesItem> seriesList = new List<WijPieChartSeriesItem>();
            
            foreach(string key in Votes.Keys)
            {
                seriesList.Add(new WijPieChartSeriesItem()
                                   {
                                       label = key,
                                       legendEntry = true,
                                       data = Votes[key]
                                   });
                Debug.WriteLine(string.Format("{0} == {1}", key, Votes[key]));
            }

            Clients.updateVotes(seriesList);

            AddToGroup("admin");
            RemoveFromGroup("admin");

            Clients["admin"].updateVotes()
        }
    
        public void Preload()
        {
            List<WijPieChartSeriesItem> seriesList = new List<WijPieChartSeriesItem>();

            foreach (string key in Votes.Keys)
            {
                seriesList.Add(new WijPieChartSeriesItem()
                {
                    label = key,
                    legendEntry = true,
                    data = Votes[key]
                });
                Debug.WriteLine(string.Format("{0} == {1}", key, Votes[key]));
            }

            Caller.updateVotes(seriesList);
        }
    }


    public class WijPieChartSeriesItem
    {
        public string label { get; set; }
        public bool legendEntry { get; set; }
        public int data { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Queue.Models;

namespace Queue
{
    public class signalRhub : Hub
    {
        //public void NextClientCallsToViewer(TicketOnViewer oTicketOnViewer,string number, string desk)
        public void NextClientCallsToViewer(int TicketID, string number, string desktop)
        {
            //IHubContext context = GlobalHost.ConnectionManager.GetHubContext<signalRhub>();
            Clients.All.nextTicket(TicketID, number, desktop);
        }

        public void TicketsToAttend(Boolean ticketsToShow)
        {
            //IHubContext context = GlobalHost.ConnectionManager.GetHubContext<signalRhub>();
            Clients.All.ticketToAttend(ticketsToShow);
        }
    }

}
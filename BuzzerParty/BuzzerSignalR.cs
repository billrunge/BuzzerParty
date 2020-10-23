using System;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.SignalR;

namespace BuzzerParty
{
    public class BuzzerSignalR : Hub
    {
        public Task SendMessage(string user, string message)
        {
            return Clients.All.SendAsync("SendMessge", user, message);
        }
    }
}
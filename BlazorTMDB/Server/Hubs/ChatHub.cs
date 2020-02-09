using BlazorTMDB.Server.Controllers;
using BlazorTMDB.Server.Data;
using BlazorTMDB.Shared.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorTMDB.Server.Hubs
{
    public class ChatHub : Hub
    {
        public ITMDBService _TMDBService { get; set; }

        public ChatHub(ITMDBService service)
        {
            _TMDBService = service;
        }

        public async Task SendMessage()
        {
            await Clients.All.SendAsync("ReceiveMessage", _TMDBService.Test());
        }
    }
}
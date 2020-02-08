using BlazorTMDB.Server.Controllers;
using BlazorTMDB.Shared;
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
        //public TMDBController _TMDBController { get; set; }

        //public ChatHub(TMDBController tMDBController)
        //{
        //    _TMDBController = tMDBController;
        //}

        //public async Task GetData()
        //{
        //    IEnumerable<WeatherForecast> weatherForecasts = _TMDBController.Get();
        //    await Clients.All.SendAsync("ReceiveData", weatherForecasts);
        //}

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
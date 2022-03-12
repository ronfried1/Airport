using FlightControlServer.Models;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightControlServer.Hubs
{
    public class SignalrHubs : Hub, ISignalrHubs
    {

        public void signalrConnection(string data)
        {
            Clients.Caller.SendAsync("send-info", data);
        }

        public void SendAirplanes(List<Airplane> data)
        {
            Clients.Caller.SendAsync("send-airplanes", data);
        }

        public void SendUpdatedAirplanes(Airplane data)
        {
            Clients.Caller.SendAsync("send-updated-airplane", data);
        }

        public void SendStations(Track data)
        {
            Clients.Caller.SendAsync("send-current-stations", data);
        }

        public void SendUpdatedStation(Station to , Station from)
        {
            if (to != null)
            {
                object dataObj = new
                {
                    id = to.Id,
                    plane = to.AirplaneInThisStation?.Id,
                    stationBefore = from?.Id,
                };
                Clients.Caller.SendAsync("send-updated-station", dataObj);
            }
            else
            {
                object dataObj = new
                {
                    id = -1,
                    plane = -1,
                    stationBefore = from.Id,
                };
                Clients.Caller.SendAsync("send-updated-station", dataObj);
            }
        }

    }
}

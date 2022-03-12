using FlightControlServer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightControlServer.Hubs
{
    public interface ISignalrHubs
    {
        void signalrConnection(string data);
        void SendAirplanes(List<Airplane> data);
        void SendStations(Track data);
    }
}
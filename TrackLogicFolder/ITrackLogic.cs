using FlightControlServer.Models;
using System.Threading.Tasks;

namespace FlightControlServer.TrackLogicFolder
{
    public interface ITrackLogic
    {
        Graph DepartureGraph { get; }
        Graph ArrivalGraph { get; }
        bool TrackIsReady { get; }
        Track createTrack();
        Graph createDepartureTrack();
        Graph createArrivalTrack();
    }
}
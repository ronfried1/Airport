using FlightControlServer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightControlServer.PlanesLogicFolder
{
    public interface IAirplanesLogic
    {
        List<Airplane> airplaneList { get; }
        List<Airplane> CreateAirplanes();
        void StartAirplaneTrack(Airplane plane);
        Task GoToNexStation(Airplane plane, Station from);
        void AirplaneCompleteTrack(Airplane plane);
    }
}
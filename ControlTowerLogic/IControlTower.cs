using FlightControlServer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightControlServer.ControlTowerLogic
{
    public interface IControlTower
    {
        bool IsStarted { get;}
        List<Airplane> GetAllAirplens();

        Task AirplaneStartTrackPermission(Airplane plane);

        bool IsPermissionForDeparture(Airplane plane);
        bool IsPermissionForArraival(Airplane plane);
        bool IsStepAvailable(int station);
        bool CheckState();
    }
}
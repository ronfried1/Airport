using FlightControlServer.ControlTowerLogic;
using FlightControlServer.Hubs;
using FlightControlServer.Models;
using FlightControlServer.PlanesLogicFolder;
using FlightControlServer.TrackLogicFolder;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FlightControlServer.AirportManager
{
    //the project

    public class AppStarter : IAppStarter
    {
        // the project real

        private SignalrHubs Signalr { get; }
        private List<Airplane> airplanes { get; set; }
        private IControlTower controlTower { get; }
        private ITrackLogic track;
        private IAirplanesLogic airplanesLogic;

        public AppStarter(IControlTower controlTower, ITrackLogic track, IAirplanesLogic airplanesLogic, SignalrHubs signalr)
        {
            this.controlTower = controlTower;
            this.track = track;
            this.airplanesLogic = airplanesLogic;
            Signalr = signalr;
        }


        public bool StartCreatingAirplanes()
        {
            try
            {
                //build the planes
                airplanes = airplanesLogic.CreateAirplanes();
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public bool StartCreatingTrack()
        {
            try
            {
                //build the track 
                var theMainTrack = track.createTrack();
                track.createDepartureTrack();
                track.createArrivalTrack();
                Signalr.SendStations(theMainTrack);
                return true;

            }
            catch (System.Exception)
            {
                return false;
                throw;
            }
        }

        public bool StartGame()
        {
            try
            {
                foreach (var plane in airplanes)
                {
                    Thread.Sleep(1000);
                    Task.Run(() => GetPermissionFromControlTower(plane));
                }
                return true;
            }
            catch (System.Exception)
            {
                return false;
                throw;
            }
        }

        public async Task GetPermissionFromControlTower(Airplane aitplane)
        {
            if (aitplane.IsDeparture)
            {
                bool Permission = controlTower.IsPermissionForDeparture(aitplane);
                //tell the plane to star the track that he got from this method
                if (Permission)
                {
                  await  controlTower.AirplaneStartTrackPermission(aitplane); //Tell The Plane He Can Start The Track
                } 
            }
            if (aitplane.IsArrival)
            {
                bool Permission = controlTower.IsPermissionForArraival(aitplane);
                if (Permission)
                {
                  await  controlTower.AirplaneStartTrackPermission(aitplane);
                } //tell the plane to star the track that he got from this method
            }
        }

        public void GetPositions()
        {
            Signalr.SendAirplanes(controlTower.GetAllAirplens());
        }
    }
}

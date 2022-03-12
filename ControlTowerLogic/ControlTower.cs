using FlightControlServer.Models;
using FlightControlServer.PlanesLogicFolder;
using FlightControlServer.TrackLogicFolder;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FlightControlServer.ControlTowerLogic
{
    public class ControlTower : IControlTower
    {
        readonly object obj = new object();
        private bool isStarted = false;
        public bool IsStarted => isStarted;
        public List<Airplane> Planes { get; set; }
        public ITrackLogic trackLogic { get; }
        public IAirplanesLogic PlaneLogic { get; }

        public ControlTower(IAirplanesLogic planeLogic, ITrackLogic trackLogic)
        {
            PlaneLogic = planeLogic;
            this.trackLogic = trackLogic;
        }


        public bool IsPermissionForDeparture(Airplane airplane)
        {
            try
            {
                Permission permission = new Permission
                {
                    HavePermission = true,
                    Track = trackLogic.DepartureGraph
                };
                airplane.Permission = permission;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool IsPermissionForArraival(Airplane airplane)
        {
            try
            {
                Permission permission = new Permission
                {
                    HavePermission = true,
                    Track = trackLogic.ArrivalGraph
                };
                airplane.Permission = permission;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task AirplaneStartTrackPermission(Airplane airplane)
        {
            PlaneLogic.StartAirplaneTrack(airplane);
        }



        public async Task StartTheTrack(Airplane airplane)
        {
            if (airplane.IsDeparture)
            {
                StartDeparture(airplane);
            }
            if (airplane.IsArrival)
            {
                StartLanding(airplane);
            }

            // if the plane is complete the track delete him from the list of reset the plane info and start over again
        }

        private void StartLanding(Airplane airplane)
        {
            bool AirplaneComletedTrack = airplane.CompletedTrack;
            while (!AirplaneComletedTrack)
            {

            }
        }

        private void StartDeparture(Airplane airplane)
        {

            bool ThePlaneComleteTheTrack = airplane.CompletedTrack;
            while (!ThePlaneComleteTheTrack)
            {

            }
        }

        public bool IsStepAvailable(int station)
        {
            lock (obj)
            {
                return true;
                //return trackLogic.Track.Station[trackLogic.Track.Station.FindIndex(Station => Station.Id == station)].IsAvailable;
            }
        }

        public bool CheckState()
        {
            return isStarted;
        }

        public List<Airplane> GetAllAirplens()
        {
            return Planes;
        }




    }
}

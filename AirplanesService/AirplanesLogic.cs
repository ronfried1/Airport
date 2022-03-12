using FlightControlServer.ControlTowerLogic;
using FlightControlServer.Hubs;
using FlightControlServer.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FlightControlServer.PlanesLogicFolder
{
    public class AirplanesLogic : IAirplanesLogic
    {
        List<Airplane> planesList;
        public SignalrHubs Signalr { get; }

        List<Airplane> IAirplanesLogic.airplaneList => planesList;

        public AirplanesLogic(SignalrHubs signalr)
        {
            Signalr = signalr;
        }

        private List<Station> findFirstStation(Airplane airplane)
        {
            return airplane.Permission.Track.GetFirstStation();
        }

        public void StartAirplaneTrack(Airplane airplane)
        {
            var firstStations = findFirstStation(airplane);
            if (firstStations.Count == 1)
            {
                Task.Run(() => GoToNexStation(airplane, firstStations[0])); // 1 or 0 (first index)
            }
            else
            {
                Random random = new Random();
                random.Next(firstStations.Count);
                int firstRandomIndex;
                int.TryParse(random.ToString(), out firstRandomIndex);
                Task.Run(() => GoToNexStation(airplane, firstStations[firstRandomIndex]));
            }
        }

        public async Task GoToNexStation(Airplane airplane, Station from)
        {
            List<Station> to = airplane.Permission.Track.GetNextStation(from);
            if (to[0] == null)
            {
                airplane.CurrentStation = null;
                airplane.CompletedTrack = true;
                Signalr.SendUpdatedStation(airplane.CurrentStation, from);
                //Signalr.SendUpdatedPlanesToClient(plane);
                return;
            }
            if (to.Count == 1)
            {
                await to[0].Semaphore.WaitAsync();
                Thread.Sleep(2000);
                
                Station stationToClean = airplane.CurrentStation;
                airplane.CurrentStation = to[0];
                airplane.CurrentStation.AirplaneInThisStation = airplane;
                if (stationToClean != null)
                {
                    stationToClean.AirplaneInThisStation = null;
                    stationToClean.Semaphore.Release();
                }
                else
                {
                    stationToClean = new Station(){ Id = -1};
                }
                Signalr.SendUpdatedStation(airplane.CurrentStation, from);
                //Signalr.SendUpdatedPlanesToClient(plane);
                to[0].Semaphore.Release();
                GoToNexStation(airplane, to[0]);
            }
            else
            {
                Random random = new Random();
                random.Next(to.Count);
                int firstRandomIndex;
                int.TryParse(random.ToString(), out firstRandomIndex);
                to[firstRandomIndex].Semaphore.WaitAsync();
                var stationToClean = airplane.CurrentStation;
                airplane.CurrentStation = to[firstRandomIndex];
                airplane.CurrentStation.AirplaneInThisStation = airplane;
                stationToClean.AirplaneInThisStation = null;
                Signalr.SendUpdatedStation(airplane.CurrentStation, from);
                stationToClean.Semaphore.Release();
                to[firstRandomIndex].Semaphore.Release();
                GoToNexStation(airplane, to[firstRandomIndex]);
            }
        }

        public void AirplaneCompleteTrack(Airplane airplane)
        {
            airplane.CompletedTrack = true;
            //Signalr.SendUpdatedStationToClient(plane.CurrentStation);
            //Signalr.SendUpdatedPlanesToClient(plane);

        }

        public List<Airplane> CreateAirplanes()
        {
            planesList = new List<Airplane>()
            {
                new Airplane() {Id = Guid.NewGuid().ToString() ,IsArrival = true},
                new Airplane() {Id = Guid.NewGuid().ToString() ,IsArrival = true},
                new Airplane() {Id = Guid.NewGuid().ToString() ,IsArrival = true},
                //new Plane() {Id = Guid.NewGuid().ToString() ,IsArrival = true},
                //new Plane() {Id = Guid.NewGuid().ToString() ,IsArrival = true},
                //new Plane() {Id = Guid.NewGuid().ToString() ,IsArrival = true},
                //new Plane() {Id = Guid.NewGuid().ToString() ,IsArrival = true},
                //new Plane() {Id = Guid.NewGuid().ToString() ,IsArrival = true},
                //new Plane() {Id = Guid.NewGuid().ToString() ,IsArrival = true},
                //new Plane() {Id = Guid.NewGuid().ToString() ,IsArrival = true},
            };
            return planesList;
        }


    }
}

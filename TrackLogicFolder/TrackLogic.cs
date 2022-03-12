using FlightControlServer.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FlightControlServer.TrackLogicFolder
{
    public class TrackLogic : ITrackLogic
    {
        Graph departureGraph;
        Graph arrivalGraph ;
        Track track;
        Track departureTrack;
        bool trackIsReady;
        public Graph DepartureGraph => departureGraph;
        public Graph ArrivalGraph => arrivalGraph;
        public Track DepartureTrack => departureTrack;
        public bool TrackIsReady => trackIsReady;

        public Track createTrack()
        {
            track = new Track()
            {
                Station = new List<Station>(){
                   new Station(){Id = 1},
                   new Station(){Id = 2},
                   new Station(){Id = 3},
                   new Station(){Id = 4},
                   new Station(){Id = 5},
                   new Station(){Id = 6},
                   new Station(){Id = 7},
                   new Station(){Id = 8}}
            };
            trackIsReady = true;
            return track;
        }
        public Graph createDepartureTrack()
        {
            departureGraph = new Graph();
            //start point
            departureGraph.AddAdge(null, track.Station[5]);
            departureGraph.AddAdge(null, track.Station[6]);
            departureGraph.AddAdge(track.Station[5], track.Station[7]);
            departureGraph.AddAdge(track.Station[6], track.Station[7]);
            departureGraph.AddAdge(track.Station[7], track.Station[3]);
            //end point
            departureGraph.AddAdge(track.Station[3], null);

            return departureGraph;
        }
        public Graph createArrivalTrack()
        {
            arrivalGraph = new Graph(); 
            //start point
            arrivalGraph.AddAdge(null, track.Station[0]);
            arrivalGraph.AddAdge(track.Station[0], track.Station[1]);
            arrivalGraph.AddAdge(track.Station[1], track.Station[2]);
            arrivalGraph.AddAdge(track.Station[2], track.Station[3]);
            arrivalGraph.AddAdge(track.Station[3], track.Station[4]);
            arrivalGraph.AddAdge(track.Station[4], track.Station[5]);
            arrivalGraph.AddAdge(track.Station[4], track.Station[6]);
            //end point
            arrivalGraph.AddAdge(track.Station[5], null);
            arrivalGraph.AddAdge(track.Station[6], null);

            return arrivalGraph;
        }
    }
}

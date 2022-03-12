using FlightControlServer.Models;
using System.Collections.Generic;
using System.Linq;

namespace FlightControlServer.TrackLogicFolder
{
    public class Graph
    {
        List<Adge> adges = new List<Adge>();

        public void AddAdge(Station from, Station to)
        {
            Adge adge = new Adge() { from = from, to = to };
            adges.Add(adge);
        }

        public List<Station> GetFirstStation()
        {
            var listOfStations = adges.Where(e => e.from == null);
            return listOfStations?.Select(e => e.from).ToList();
        }

        public List<Station> GetNextStation(Station from)
        {
            var listOfNextStations = adges.Where(adge => adge.from == from);
            return listOfNextStations.Select(adge => adge.to).ToList();
        }
    }
    public class Adge
    {
        public Station from { get; set; }
        public Station to { get; set; }
    }
}

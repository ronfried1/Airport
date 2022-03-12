using System;
using System.Threading.Tasks;

namespace FlightControlServer.Models
{
    public class Airplane
    {
        public string Id { get; set; }
        public Station CurrentStation { get; set; }
        public bool IsArrival { get; set; }
        public bool IsDeparture { get; set; }
        public Permission Permission { get; set; }
        public bool CompletedTrack { get; set; }
    }
}

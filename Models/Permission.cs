using FlightControlServer.TrackLogicFolder;

namespace FlightControlServer.Models
{
    public class Permission
    {
        public bool HavePermission { get; set; }
        public Graph Track { get; set; }
    }
}

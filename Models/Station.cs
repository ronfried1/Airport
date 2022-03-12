using System.Threading;
using System.Threading.Tasks;

namespace FlightControlServer.Models
{
    public class Station
    {
        public int Id { get; set; }
        public SemaphoreSlim Semaphore = new SemaphoreSlim(1,1);
        public Airplane AirplaneInThisStation { get; set; }
    }
}
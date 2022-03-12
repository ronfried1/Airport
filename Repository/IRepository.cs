namespace FlightControlServer.Repository
{
    public interface IRepository
    {
        public void GetLastAppState();
        public void SaveLastAppState();
    }
}
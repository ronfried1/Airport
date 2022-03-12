namespace FlightControlServer.AirportManager
{
    public interface IAppStarter
    {
        bool StartCreatingTrack();
        bool StartCreatingAirplanes();
        bool StartGame();
        void GetPositions();
    }
}
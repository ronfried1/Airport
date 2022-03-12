using FlightControlServer.AirportManager;
using FlightControlServer.ControlTowerLogic;
using FlightControlServer.Hubs;
using FlightControlServer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightControlServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AirportController : ControllerBase
    {
        public IAppStarter Starter;
        bool airplanesCreated = false;
        bool trackIsCreated = false;
        bool GameIsStarted = false;

        public AirportController(IAppStarter starter)
        {
            Starter = starter;
        }


        [HttpGet]
        [Route("start")]
        public void Start()
        {
            //create the track
            trackIsCreated = Starter.StartCreatingTrack();
            if (trackIsCreated)
                //create the planes
                airplanesCreated = Starter.StartCreatingAirplanes();
            if (airplanesCreated)
                //start the game
                GameIsStarted = Starter.StartGame();

            //then ask for approve to departure of arrival 
            //when you got the approve from the control tower the plane get object with the approve and with the tack that he is need to do
            //and the can enter to the track and start his track
            //when the plane is start his track send message abut it , and when hes is complete his track

        }


        [HttpGet]
        [Route("getPositions")]
        public void GetPositions()
        {
            if (GameIsStarted)
                Starter.GetPositions();
        } 
        //[HttpGet]
        //[Route("getStations")]
        //public void GetStations()
        //{
        //    if (theGameIsStarted)
        //        Bulder.GetPositions();
        //}




    }
}

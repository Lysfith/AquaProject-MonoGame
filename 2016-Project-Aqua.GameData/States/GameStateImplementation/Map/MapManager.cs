using _2016_Project_Aqua.GameData.Config;
using _2016_Project_Aqua.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace _2016_Project_Aqua.GameData.States.GameStateImplementation.Map
{
    public class MapManager
    {
        private Zone _zone;

        public event NewZoneHandler OnNewZoneCreated;
        public delegate void NewZoneHandler(Zone zone);

        public event SimulationUpdateHandler OnSimulationUpdated;
        public delegate void SimulationUpdateHandler(Tile[,] tiles);

        public int ActualZoneX = 0;
        public int ActualZoneY = 0;

        private DateTime _lastUpdateSimulation;
        private DateTime _lastAddWater;

        public MapManager()
        {
            _lastUpdateSimulation = DateTime.Now;
            _lastAddWater = DateTime.Now.AddMinutes(-1);
        }

        public void GenerateZone()
        {
            DebugGame.Log("MapManager", "GetZone", "Création de la zone...");

            var sw = new Stopwatch();
            sw.Start();

            _zone = new Zone(0, 0, Settings.ZoneWidth, Settings.ZoneHeight);

            _zone.Generate();

            DebugGame.Log("MapManager", "GetZone", "Création de la zone (" + sw.ElapsedMilliseconds + " ms)");

            if (OnNewZoneCreated != null)
            {
                OnNewZoneCreated(_zone);
            }
        }

        public void Update(double time)
        {

        }

        public Zone GetZone()
        {
            return _zone;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2016_Project_Aqua.GameData.States.GameStateImplementation.Map
{
    public class Tile
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Altitude { get; set; }
        public float Water { get; set; }
        public Terrain Terrain { get; set; }

        public Tile()
        {
            Altitude = 0;
            Water = 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2016_Project_Aqua.GameData.States.GameStateImplementation.Map
{
    public enum TypeTerrain
    {
        Grass,
        Dirt,
        Rock
        
    }

    public class Terrain
    {
        public TypeTerrain Type { get; private set; }

        public Terrain(TypeTerrain type)
        {
            Type = type;
        }
    }
}

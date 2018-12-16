using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2016_Project_Aqua.GameData.States.GameStateImplementation.Map
{
    public enum LayerTileType : int
    {
        Floor = 0,
        Interaction = 1,
        Collision = 2,
        Foreground = 3
    }

    public class LayerTile
    {
        public LayerTileType Type { get; private set; }
        public Tile[,] Tiles { get; private set; }

        private int _width;
        private int _height;

        public LayerTile(LayerTileType type, int width, int height)
        {
            _width = width;
            _height = height;
            Type = type;

            Tiles = new Tile[height, width];
        }
    }
}

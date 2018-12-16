using _2016_Project_Aqua.GameData.Config;
using _2016_Project_Aqua.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2016_Project_Aqua.GameData.States.GameStateImplementation.Map
{
    public class Zone
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public LayerTile LayerFloor { get; private set; }
        public LayerTile LayerInteraction { get; private set; }
        public LayerTile LayerCollision { get; private set; }
        public LayerTile LayerForeground { get; private set; }

        public Zone(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;

            CreateLayers();
        }

        public void CreateLayers()
        {
            LayerFloor = new LayerTile(LayerTileType.Floor, Width, Height);
            LayerInteraction = new LayerTile(LayerTileType.Interaction, Width, Height);
            LayerCollision = new LayerTile(LayerTileType.Collision, Width, Height);
            LayerForeground = new LayerTile(LayerTileType.Foreground, Width, Height);
        }

        public void Generate()
        {
            DebugGame.Log("Zone", "Generate", "Génération de la zone");

            //Perlin
            int width = Settings.ZoneWidth;
            int height = Settings.ZoneHeight;
            int octaveCount = 5;

            float[][] perlinNoise = PerlinNoise.GeneratePerlinNoise(width, height, octaveCount);

            //var simplex = new OpenSimplexNoise();

            var terrainGrass = new Terrain(TypeTerrain.Grass);
            var terrainRock = new Terrain(TypeTerrain.Rock);
            var terrainDirt = new Terrain(TypeTerrain.Dirt);

            for (var line = 0; line < Height; line++)
            {
                for (var col = 0; col < Width; col++)
                {
                    //var altitude = (simplex.Evaluate(line, col))* 4f;
                    var altitude = (perlinNoise[line][col] - 0.5f) * 50f;

                    //if (altitude < -6)
                    //{
                    //    layer.Tiles[line, col] = new Tile()
                    //    {
                    //        X  = col,
                    //        Y = line,
                    //        Terrain = terrainDirt,
                    //        Water = 3,
                    //        Altitude = (int)altitude
                    //    };
                    //}
                    //else if (altitude < -4)
                    //{
                    //    layer.Tiles[line, col] = new Tile()
                    //    {
                    //        X = col,
                    //        Y = line,
                    //        Terrain = terrainDirt,
                    //        Water = 2,
                    //        Altitude = (int)altitude
                    //    };
                    //}
                    //else if (altitude < -2)
                    //{
                    //    layer.Tiles[line, col] = new Tile()
                    //    {
                    //        X = col,
                    //        Y = line,
                    //        Terrain = terrainDirt,
                    //        Water = 1,
                    //        Altitude = (int)altitude
                    //    };
                    //}
                    //else 
                    if(altitude > 5)
                    {
                        LayerFloor.Tiles[line, col] = new Tile()
                        {
                            X = col,
                            Y = line,
                            Terrain = terrainRock,
                            Water = 0,
                            Altitude = (int)altitude
                        };
                    }
                    else if (altitude > 3)
                    {
                        LayerFloor.Tiles[line, col] = new Tile()
                        {
                            X = col,
                            Y = line,
                            Terrain = terrainDirt,
                            Water = 0,
                            Altitude = (int)altitude
                        };
                    }
                    else
                    {
                        LayerFloor.Tiles[line, col] = new Tile()
                        {
                            X = col,
                            Y = line,
                            Terrain = terrainGrass,
                            Water = 0,
                            Altitude = (int)altitude
                        };
                    }
                }
            } 
        }

        public List<Zone> Subdivide()
        {
            List<Zone> zones = new List<Zone>();

            int nbSubZoneX = (int)(Math.Floor((float)Width / (float)Settings.SubZoneWidth));
            int nbSubZoneY = (int)(Math.Floor((float)Height / (float)Settings.SubZoneHeight));

            for (var line = 0; line < nbSubZoneY; line++)
            {
                for (var col = 0; col < nbSubZoneX; col++)
                {
                    var zone = new Zone(col, line, Settings.SubZoneWidth, Settings.SubZoneHeight);

                    for (var zoneLine = 0; zoneLine < Settings.SubZoneHeight; zoneLine++)
                    {
                        for (var zoneCol = 0; zoneCol < Settings.SubZoneWidth; zoneCol++)
                        {
                            zone.LayerFloor.Tiles[zoneLine, zoneCol] =
                                LayerFloor.Tiles[zoneLine + line * Settings.SubZoneHeight, zoneCol + col * Settings.SubZoneWidth];
                            //zone.LayerFloor.Tiles[zoneLine, zoneCol].Altitude = (line + col) * 4;
                        }
                    }

                    zones.Add(zone);
                }
            }

            return zones;
        }
    }
}

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2016_Project_Aqua.GameData.Config
{
    public class Settings
    {
        //General
        public static int ScreenWidth = 1366;
        public static int ScreenHeight = 768;

        //Tiles

        public static float Zoom = 1f;
        public static float Tile_Size
        {
            get
            {
                return 32 * Zoom;
            }
        }
        
        public static int Texture_Size = 128;

        //Zones
        public static int ZoneWidth = 1000;
        public static int ZoneHeight = 1000;
        public static int SubZoneWidth = 100;
        public static int SubZoneHeight = 100;

        public static string Texture_Grass = "Textures/grass_1";

        public static string Texture_TileSet1 = "Textures/tileset_pokemon_2_1";
        public static string Texture_Iso_TileSet1 = "Textures/basic_ground_tiles";

        public static Rectangle Texture_Rect_Grass = new Rectangle(128, 0, 128, 128);
        public static Rectangle Texture_Rect_Rock = new Rectangle(256, 0, 128, 128);
        public static Rectangle Texture_Rect_Dirt = new Rectangle(384, 0, 128, 128);
        public static Rectangle Texture_Rect_Water_1 = new Rectangle(256, 0, 128, 128);
        public static Rectangle Texture_Rect_Water_2 = new Rectangle(128, 0, 128, 128);
        public static Rectangle Texture_Rect_Water_3 = new Rectangle(0, 0, 128, 128);
        public static Rectangle Texture_Rect_Water_4 = new Rectangle(384, 0, 128, 128);



    }
}

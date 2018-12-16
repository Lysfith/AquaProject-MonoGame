using _2016_Project_Aqua.GameData.Config;
using _2016_Project_Aqua.GameData.States.GameStateImplementation.Map;
using _2016_Project_Aqua.GameData.Utils;
using _2016_Project_Aqua.Graphic;
using _2016_Project_Aqua.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace _2016_Project_Aqua.GameData.States.GameStateImplementation.View
{
    public class MapView
    {
        private MapManager _mapManager;
        private Dictionary<string, ZoneView> _zoneViews;

        public int OffsetX { get; private set; }
        public int OffsetY { get; private set; }
        public int Zoom { get; set; }

        public MapView(MapManager mapManager)
        {
            _mapManager = mapManager;
            _zoneViews = new Dictionary<string, ZoneView>();

            _mapManager.OnNewZoneCreated += Event_OnNewZoneCreated;


            var tileset = TextureManager.Instance.GetTexture(Settings.Texture_Iso_TileSet1);

            SpriteManager.Instance.AddOrReplaceSprite("Grass", new Sprite(tileset, Settings.Texture_Rect_Grass,
                new Rectangle(0, 0, (int)Settings.Tile_Size, (int)Settings.Tile_Size), Color.White));
            SpriteManager.Instance.AddOrReplaceSprite("Rock", new Sprite(tileset, Settings.Texture_Rect_Rock,
               new Rectangle(0, 0, (int)Settings.Tile_Size, (int)Settings.Tile_Size), Color.White));
            SpriteManager.Instance.AddOrReplaceSprite("Dirt", new Sprite(tileset, Settings.Texture_Rect_Dirt,
               new Rectangle(0, 0, (int)Settings.Tile_Size, (int)Settings.Tile_Size), Color.White));
            SpriteManager.Instance.AddOrReplaceSprite("Water1", new Sprite(tileset, Settings.Texture_Rect_Water_1,
               new Rectangle(0, 0, (int)Settings.Tile_Size, (int)Settings.Tile_Size), Color.White));
            SpriteManager.Instance.AddOrReplaceSprite("Water2", new Sprite(tileset, Settings.Texture_Rect_Water_2,
               new Rectangle(0, 0, (int)Settings.Tile_Size, (int)Settings.Tile_Size), Color.White));
            SpriteManager.Instance.AddOrReplaceSprite("Water3", new Sprite(tileset, Settings.Texture_Rect_Water_3,
               new Rectangle(0, 0, (int)Settings.Tile_Size, (int)Settings.Tile_Size), Color.White));

        }

        public void SetOffset(int x, int y)
        {
            OffsetX += x;
            OffsetY += y;
        }

        public void Resize()
        {
            UpdateSubZonePosition();
            UpdateSubZoneTexture();
        }

        public void Move()
        {
            UpdateSubZonePosition();
        }

        public void Update(double time)
        {

        }

        public void Draw(MySpriteBatch spritebatch)
        {
            //Calcul zones visibles
            int xMin = (int)(-Settings.ScreenWidth * 1/Settings.Zoom);
            int yMin = (int)(-Settings.ScreenHeight * 1/Settings.Zoom);
            int xMax = (int)(Settings.ScreenWidth + Settings.ScreenWidth * 1/Settings.Zoom);
            int yMax = (int)(Settings.ScreenHeight + Settings.ScreenHeight * 1/Settings.Zoom);

            for (var i= _zoneViews.Count-1; i >= 0; i--)
            {
                var zoneView = _zoneViews.ElementAt(i);
                var position = zoneView.Value.GetPosition();

                if (
                    xMin <= position.X && position.X < xMax
                    && yMin <= position.Y && position.Y < yMax
                    )
                {
                    zoneView.Value.Draw(spritebatch);
                }
            }

            var font = FontManager.Instance.GetFont("Arial-16");

            spritebatch.DrawString(font, $"Zones : {xMin},{yMin} {xMax},{yMax}", new Vector2(10, 110), Color.Yellow);
        }

        private void Event_OnNewZoneCreated(Zone zone)
        {
            var subZones = zone.Subdivide();

            foreach (var subZone in subZones)
            {
                var zoneView = new ZoneView(subZone, Vector2.Zero);
                zoneView.UpdateTexture();

                _zoneViews.Add(subZone.X + "_" + subZone.Y, zoneView);
            }

            UpdateSubZonePosition();
            UpdateSubZoneTexture();
        }

        private void UpdateSubZonePosition()
        {
            foreach (var zoneView in _zoneViews)
            {
                var positionZone = Convert2DToIso(zoneView.Value.GetZone().X, zoneView.Value.GetZone().Y, 0);
                float offsetX = positionZone.X * Settings.Tile_Size + OffsetX;
                float offsetY = positionZone.Y * Settings.Tile_Size + OffsetY;

                zoneView.Value.SetPosition(new Vector2(offsetX, offsetY));
            }
        }

        private void UpdateSubZoneTexture()
        {
            //foreach (var zoneView in _zoneViews)
            //{
            //    zoneView.Value.UpdateTexture();
            //}
        }

        private Vector2 Convert2DToIso(int x, int y, int z)
        {
            var tileSizeWidthHalf = 0.5f;
            var tileSizeWidthQuart = 0.25f;

            int nbSubZoneX = (int)(Math.Floor((float)Settings.ZoneWidth / (float)Settings.SubZoneWidth));
            int nbSubZoneY = (int)(Math.Floor((float)Settings.ZoneHeight / (float)Settings.SubZoneHeight));

            var midWidth = Settings.SubZoneHeight * tileSizeWidthHalf;
            var midHeight = Settings.SubZoneHeight * tileSizeWidthQuart;
            var top = midHeight * (nbSubZoneY);

            return new Vector2(
                x * midWidth - y * midWidth,
                (top - (x * midHeight + y * midHeight)) + z * 0.1f
                );
        }
    }
}

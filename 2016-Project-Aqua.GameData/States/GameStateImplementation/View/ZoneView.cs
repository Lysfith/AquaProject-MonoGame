using _2016_Project_Aqua.GameData.Config;
using _2016_Project_Aqua.GameData.States.GameStateImplementation.Map;
using _2016_Project_Aqua.Graphic;
using _2016_Project_Aqua.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2016_Project_Aqua.GameData.States.GameStateImplementation.View
{
    public class ZoneView
    {
        private Zone _zone;
        private Texture2D _texture;
        private bool _textureUpdating;
        private Vector2 _position;

        public int Width { get; private set; }
        public int Height { get; private set; }

        public ZoneView(Zone zone, Vector2 position)
        {
            _zone = zone;
            _position = position;
        }

        public void SetPosition(Vector2 position)
        {
            _position = position;
        }

        public Vector2 GetPosition()
        {
            return _position;
        }

        public Zone GetZone()
        {
            return _zone;
        }

        public void UpdateTexture()
        {
            _textureUpdating = true;

            MySpriteBatch spritebatch = new MySpriteBatch(GraphicManager.Instance.GraphicsDevice);

            Width = Settings.SubZoneWidth * (int)Settings.Tile_Size;
            Height = Settings.SubZoneHeight * (int)Settings.Tile_Size;
            var renderTarget = new RenderTarget2D(GraphicManager.Instance.GraphicsDevice, Width, Height);

            GraphicManager.Instance.GraphicsDevice.SetRenderTarget(renderTarget);

            GraphicManager.Instance.GraphicsDevice.Clear(Color.Transparent);

            Sprite spriteGrass = SpriteManager.Instance.GetSprite("Grass");
            Sprite spriteRock = SpriteManager.Instance.GetSprite("Rock");
            Sprite spriteDirt = SpriteManager.Instance.GetSprite("Dirt");
            Sprite spriteWater1 = SpriteManager.Instance.GetSprite("Water1");
            Sprite spriteWater2 = SpriteManager.Instance.GetSprite("Water2");
            Sprite spriteWater3 = SpriteManager.Instance.GetSprite("Water3");

            spritebatch.Begin();

            for (var line = _zone.Height - 1; line >= 0; line--)
            {
                for (var col = _zone.Width - 1; col >= 0; col--)
                {
                    var tile = _zone.LayerFloor.Tiles[line, col];
                    var position = Convert2DToIso(new Vector3(col, line, tile.Altitude));
                    position.X += renderTarget.Width * 0.5f - Settings.Tile_Size * 0.5f;
                    position.Y += renderTarget.Height * 0.5f - Settings.Tile_Size * 0.5f;

                    if (tile.Terrain.Type == TypeTerrain.Rock)
                    {
                        spritebatch.Draw(spriteRock.Texture,
                            new Rectangle((int)position.X, (int)position.Y, (int)Settings.Tile_Size, (int)Settings.Tile_Size),
                            spriteRock.RectangleSource,
                            spriteRock.Color);
                    }
                    else if (tile.Terrain.Type == TypeTerrain.Grass)
                    {
                        spritebatch.Draw(spriteGrass.Texture,
                            new Rectangle((int)position.X, (int)position.Y, (int)Settings.Tile_Size, (int)Settings.Tile_Size),
                            spriteGrass.RectangleSource,
                            spriteGrass.Color);
                    }
                    else if (tile.Terrain.Type == TypeTerrain.Dirt)
                    {
                        spritebatch.Draw(spriteDirt.Texture,
                             new Rectangle((int)position.X, (int)position.Y, (int)Settings.Tile_Size, (int)Settings.Tile_Size),
                             spriteDirt.RectangleSource,
                             spriteDirt.Color);
                    }
                }
            }

            spritebatch.End();

            GraphicManager.Instance.GraphicsDevice.SetRenderTarget(null);

            _texture = (Texture2D)renderTarget;

            _textureUpdating = false;
        }

        public void Draw(MySpriteBatch spritebatch)
        {
            if(!_textureUpdating)
            {
                var center = _position - new Vector2(_texture.Width * Settings.Zoom * 0.5f, _texture.Height * Settings.Zoom * 0.5f);
                spritebatch.Draw(_texture, 
                    new Rectangle((int)center.X, (int)center.Y, (int)(_texture.Width * Settings.Zoom), (int)(_texture.Height * Settings.Zoom)), 
                    Color.White);
            }
        }

        private Vector2 Convert2DToIso(Vector3 position)
        {
            var tileSizeWidthHalf = Settings.Tile_Size * 0.5f;
            var tileSizeWidthQuart = Settings.Tile_Size * 0.25f;
            var midWidth = Settings.SubZoneHeight * tileSizeWidthHalf;
            var top = Settings.SubZoneHeight * tileSizeWidthQuart;

            return new Vector2(
                position.X * tileSizeWidthHalf - position.Y * tileSizeWidthHalf,
                (top - (position.X * tileSizeWidthQuart + position.Y * tileSizeWidthQuart) - tileSizeWidthQuart) - position.Z
                );
        }
    }
}

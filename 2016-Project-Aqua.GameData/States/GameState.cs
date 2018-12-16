using _2016_Project_Aqua.GameData.Config;
using _2016_Project_Aqua.GameData.States.GameStateImplementation.Map;
using _2016_Project_Aqua.GameData.States.GameStateImplementation.Players;
using _2016_Project_Aqua.GameData.States.GameStateImplementation.View;
using _2016_Project_Aqua.Graphic;
using _2016_Project_Aqua.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace _2016_Project_Aqua.GameData.States
{
    class GameState : IGameState
    {
        private MapManager _mapManager;
        private PlayerManager _playerManager;

        private MapView _mapView;
        private PlayerView _playerView;

        private Vector2 _cameraLooKAt;
        private Vector3 _cameraMove;

        private List<TouchLocation> _touches;

        float _pinchDistance;

        public void Start()
        {
            //Création de la map
            _mapManager = new MapManager();
            _mapView = new MapView(_mapManager);

            _mapView.SetOffset((int)(1366 * 0.5f), (int)(768 * 0.5f));

            _mapManager.GenerateZone();

            _touches = TouchPanel.GetState().ToList();

            
        }

        public void End()
        {

        }

        public void Pause()
        {
            throw new NotImplementedException();
        }

        public void Resume()
        {
            throw new NotImplementedException();
        }

        public void Update(double time)
        {
            _mapManager.Update(time);
            _mapView.Update(time);

            MouseState mouse = Mouse.GetState();
            var oldZoom = Settings.Zoom;
            Settings.Zoom = 1f + (Settings.Zoom * mouse.ScrollWheelValue / 1000); //(mouse.ScrollWheelValue != 0 ? mouse.ScrollWheelValue / 1200f : 0);

           
            var touches = TouchPanel.GetState().ToList();
            //touches.RemoveAll(x => x.Pressure == 0);
            if (touches.Any() && _touches.Any())
            {
                
                if(touches.Count > 1 && _touches.Count > 1)
                {
                    var touch1 = touches.First();
                    var touch2 = touches[1];

                    var oldTouch1 = _touches.First();
                    var oldTouch2 = _touches[1];

                    Vector2 a = touch1.Position;
                    Vector2 b = touch2.Position;
                    float dist = Vector2.Distance(a, b);

                    // prior positions
                    Vector2 aOld = oldTouch1.Position;
                    Vector2 bOld = oldTouch2.Position;
                    float distOld = Vector2.Distance(aOld, bOld);

                    _pinchDistance = 0;
                    if (dist != distOld && Math.Abs(distOld - dist) > 1)
                    {
                        _pinchDistance = distOld - dist;
                        float scale = -(distOld - dist) * 0.1f;
                        Settings.Zoom += scale;

                        _mapView.Resize();
                    }

                    //var center = (touch1.Position - touch2.Position) / 2;
                    //_mapView.SetOffset((int)center.X, (int)center.Y);
                }
                else
                {
                    //Move
                    var touch = touches.First();
                    var oldTouch = _touches.First();
                    var position = oldTouch.Position - touch.Position;
                    _mapView.SetOffset(-(int)position.X, -(int)position.Y);

                    _mapView.Move();

                }
            }
            _touches = touches;

            if (Settings.Zoom > 4)
            {
                Settings.Zoom = 4;
            }
            else if (Settings.Zoom < 0.5f)
            {
                Settings.Zoom = 0.5f;
            }

            if (oldZoom != Settings.Zoom)
            {
                _mapView.Resize();
            }

        }

        public void Draw(MySpriteBatch spritebatch)
        {
            _mapView.Draw(spritebatch);

            SpriteFont font = FontManager.Instance.GetFont("Arial-16");

            spritebatch.DrawString(font, "Pinch : " + _pinchDistance, new Vector2(10, 90), Color.Yellow);
            spritebatch.DrawString(font, "Zoom : " + Settings.Zoom, new Vector2(10, 130), Color.Yellow);
        }
    }
}

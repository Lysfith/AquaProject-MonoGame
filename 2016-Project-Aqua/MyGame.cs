using _2016_Project_Aqua.GameData;
using _2016_Project_Aqua.GameData.Config;
using _2016_Project_Aqua.GameData.Utils;
using _2016_Project_Aqua.Graphic;
using _2016_Project_Aqua.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System.Diagnostics;

namespace _2016_Project_Aqua
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MyGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private MySpriteBatch _spriteBatch;

        private Stopwatch _stopwatch;
        private double _updateTime;
        private double _drawTime;

        //Fps
        private int _frameCounter;
        private int _lastFrameCounter;
        private Stopwatch _stopwatchFps;
        private int _maxFrameCounter;
        private int _minFrameCounter;

        private GameManager _gameManager;

        public MyGame()
        {
            //ScreenWidth = 1920;
            //ScreenHeight = 1080;
            Settings.ScreenWidth = 1366;
            Settings.ScreenHeight = 768;

            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = Settings.ScreenWidth;
            _graphics.PreferredBackBufferHeight = Settings.ScreenHeight;
            _graphics.PreferMultiSampling = true;
            _graphics.GraphicsProfile = GraphicsProfile.HiDef;
            _graphics.PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8;
            _graphics.SynchronizeWithVerticalRetrace = false;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();

            this.IsFixedTimeStep = false;
            this.IsMouseVisible = true;

            TouchPanel.EnableMouseTouchPoint = true;
            TouchPanel.EnableMouseGestures = true;

            TouchPanel.EnabledGestures =
                GestureType.PinchComplete |
                GestureType.Pinch;

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            _frameCounter = 0;
            _lastFrameCounter = 0;
            _minFrameCounter = int.MaxValue;
            _maxFrameCounter = 0;

            _stopwatch = new Stopwatch();
            _stopwatch.Start();

            _stopwatchFps = new Stopwatch();
            _stopwatchFps.Start();

            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new MySpriteBatch(GraphicsDevice);

            GraphicManager.Instance.SetGraphicsDevice(GraphicsDevice);

            FontManager.Instance.AddFont("Arial-10", Content.Load<SpriteFont>("Fonts /Arial-10"));
            FontManager.Instance.AddFont("Arial-16", Content.Load<SpriteFont>("Fonts /Arial-16"));
            FontManager.Instance.AddFont("Arial-24", Content.Load<SpriteFont>("Fonts /Arial-24"));
            FontManager.Instance.AddFont("Arial-36", Content.Load<SpriteFont>("Fonts /Arial-36"));

            TextureManager.Instance.SetContentManager(Content);

            _gameManager = new GameManager();
            _gameManager.Start();

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            _spriteBatch.Dispose();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (_stopwatchFps.ElapsedMilliseconds > 1000)
            {
                if (_frameCounter > _maxFrameCounter)
                {
                    _maxFrameCounter = _frameCounter;
                }

                if (_frameCounter < _minFrameCounter)
                {
                    _minFrameCounter = _frameCounter;
                }

                _lastFrameCounter = _frameCounter;
                _frameCounter = 0;
                _stopwatchFps.Restart();
            }
            else
            {
                _frameCounter++;
            }

            _stopwatch.Restart();

            _gameManager.Update(gameTime.ElapsedGameTime.TotalSeconds);

            _updateTime = _stopwatch.ElapsedMilliseconds;

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            _stopwatch.Restart();

            SpriteFont font = FontManager.Instance.GetFont("Arial-16");

            _gameManager.Draw(_spriteBatch);

            _drawTime = _stopwatch.ElapsedMilliseconds;

            _spriteBatch.DrawString(font, "Update : " + _updateTime.ToString("0.000") + " ms", new Vector2(10, 10), Color.Yellow);
            _spriteBatch.DrawString(font, "Draw : " + _drawTime.ToString("0.000") + " ms", new Vector2(10, 30), Color.Yellow);

            if (_minFrameCounter != int.MaxValue)
            {
                _spriteBatch.DrawString(font,
                    "Fps : " + _lastFrameCounter.ToString("00.00") + ", Min : " + _minFrameCounter.ToString("00.00") + ", Max : " + _maxFrameCounter.ToString("00.00"),
                    new Vector2(10, 50), Color.Yellow);
            }
            else
            {
                _spriteBatch.DrawString(font,
                    "Fps : " + _lastFrameCounter.ToString("00.00") + ", Max : " + _maxFrameCounter.ToString("00.00"),
                    new Vector2(10, 50), Color.Yellow);
            }

            _spriteBatch.DrawString(font, "DrawCallCount : " + _spriteBatch.DrawCallsCount, new Vector2(10, 70), Color.Yellow);


            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

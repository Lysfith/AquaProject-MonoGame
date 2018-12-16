using _2016_Project_Aqua.GameData.Enums;
using _2016_Project_Aqua.Graphic;
using _2016_Project_Aqua.Utils;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;

namespace _2016_Project_Aqua.GameData
{
    public class GameManager
    {
        public SpriteFont Font;

        // Use this for initialization
        public void Start()
        {
            StateManager.Instance.SetGameState(EnumGameState.MainMenu);

            StateManager.Instance.SetGameState(EnumGameState.Game);
#if true
            //StateManager.Instance.SetGameState(EnumGameState.Game);

            DebugGame.ShowFps();

#else

#endif

        }

        // Update is called once per frame
        public void Update(double time)
        {
            StateManager.Instance.Update(time);

#if true
            DebugGame.Update();

            //_fps.text = "FPS : " + DebugGame.GetFps();
#endif

            //if (Input.GetKeyDown(KeyCode.Escape))
            //{
            //    Application.Quit();
            //}
        }

        public void Draw(MySpriteBatch spritebatch)
        {
            StateManager.Instance.Draw(spritebatch);

            //if (Input.GetKeyDown(KeyCode.Escape))
            //{
            //    Application.Quit();
            //}
        }
    }
}
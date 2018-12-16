using _2016_Project_Aqua.Graphic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2016_Project_Aqua.GameData.States
{
    public interface IGameState
    {
        void Start();
        void End();
        void Pause();
        void Resume();
        void Update(double time);
        void Draw(MySpriteBatch spritebatch);
    }
}

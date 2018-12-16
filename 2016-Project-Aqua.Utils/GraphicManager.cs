using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2016_Project_Aqua.Utils
{
    public class GraphicManager
    {
        private static GraphicManager _instance;

        public static GraphicManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GraphicManager();
                }

                return _instance;
            }
        }

        public GraphicsDevice GraphicsDevice { get; private set; }

        public GraphicManager()
        {

        }

        public void SetGraphicsDevice(GraphicsDevice device)
        {
            GraphicsDevice = device;
        }
    }
}

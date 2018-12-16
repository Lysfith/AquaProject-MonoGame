using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2016_Project_Aqua.Utils
{
    public class FontManager
    {
        private static FontManager _instance;

        public static FontManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new FontManager();
                }

                return _instance;
            }
        }

        private Dictionary<string, SpriteFont> _fonts;

        public FontManager()
        {
            _fonts = new Dictionary<string, SpriteFont>();
        }

        public void AddFont(string name, SpriteFont font)
        {
            if (!_fonts.ContainsKey(name))
            {
                _fonts.Add(name, font);
            }
        }

        public SpriteFont GetFont(string name)
        {
            return _fonts[name];
        }
    }
}

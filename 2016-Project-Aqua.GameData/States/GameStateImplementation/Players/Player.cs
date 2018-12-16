
using _2016_Project_Aqua.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2016_Project_Aqua.GameData.States.GameStateImplementation.Players
{
    public class Player
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public string Name { get; private set; }
        public string ZoneKey { get; private set; }

        public event PlayerMoveHandler OnPlayerMoved;
        public delegate void PlayerMoveHandler(Player player, int x, int y);

        public Player(PlayerDefinition definition)
        {
            X = definition.X;
            Y = definition.Y;
            Name = definition.Name;
            ZoneKey = definition.ZoneKey;

            DebugGame.Log("Player", "Player", "Création du player " + Name + " en " + X + ", " + Y + " de la zone " + ZoneKey);
        }

        public void Event_OnPlayerMoved(int x, int y)
        {
            X = x;
            Y = y;

            if(OnPlayerMoved != null)
            {
                OnPlayerMoved(this, x, y);
            }
        }

    }
}

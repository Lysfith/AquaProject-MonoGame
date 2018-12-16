using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _2016_Project_Aqua.GameData.States.GameStateImplementation.Players
{
    public class PlayerManager
    {
        private List<Player> _players;

        public event NewPlayerHandler OnNewPlayerCreated;
        public delegate void NewPlayerHandler(Player player);

        public event PlayerMoveHandler OnPlayerMoved;
        public delegate void PlayerMoveHandler(Player player, int x, int y);

        public PlayerManager()
        {
            _players = new List<Player>();
        }

        public Player CreatePlayer(PlayerDefinition definition)
        {
            var player = new Player(definition);

            player.OnPlayerMoved += Event_OnPlayerMoved;

            if (OnNewPlayerCreated != null)
            {
                OnNewPlayerCreated(player);
            }

            return player;
        }

        public void Event_OnPlayerMoved(Player player, int x, int y)
        {
            if(OnPlayerMoved != null)
            {
                OnPlayerMoved(player, x, y);
            }
        }
    }
}



using _2016_Project_Aqua.GameData.States.GameStateImplementation.Players;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace _2016_Project_Aqua.GameData.States.GameStateImplementation.View
{
    public class PlayerView
    {
        private PlayerManager _playerManager;

        public PlayerView(PlayerManager playerManager)
        {
            _playerManager = playerManager;

            _playerManager.OnNewPlayerCreated += Event_OnNewPlayerCreated;
        }

        private void Event_OnNewPlayerCreated(Player player)
        {
            //Vector3 position = new Vector3(player.X, player.Y, -1);
            //var sprite = (GameObject)MonoBehaviour.Instantiate(Settings.Prefab_Player, position, Quaternion.identity);
            //sprite.name = player.Name;
            //sprite.transform.parent = _player.transform;

            //player.SetBehaviour(sprite.GetComponent<PlayerBehaviour>());
        }
    }
}

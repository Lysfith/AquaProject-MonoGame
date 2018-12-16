
using _2016_Project_Aqua.GameData.Enums;
using _2016_Project_Aqua.GameData.States;
using _2016_Project_Aqua.Graphic;
using _2016_Project_Aqua.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace _2016_Project_Aqua.GameData
{
    public class StateManager
    {
        private static StateManager m_instance;
        public static StateManager Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new StateManager();
                }

                return m_instance;
            }
        }

        private EnumGameState? m_gameState;
        private Dictionary<EnumGameState, IGameState> m_gameStates;

        public StateManager()
        {
            m_gameStates = new Dictionary<EnumGameState, IGameState>();
        }

        public void SetGameState(EnumGameState gameState)
        {
            if (m_gameState.HasValue)
            {
                m_gameStates[m_gameState.Value].End();
            }

            bool stateHasChanged = false;

            switch (gameState)
            {
                case EnumGameState.MainMenu:
                    if (!m_gameState.HasValue
                        || m_gameState.Value == EnumGameState.Pause)
                    {
                        m_gameStates.Clear();
                        m_gameStates[gameState] = new MainMenuState();

                        stateHasChanged = true;
                    }
                    break;
                case EnumGameState.Game:
                    if (m_gameState.HasValue
                        && (m_gameState.Value == EnumGameState.MainMenu
                        || m_gameState.Value == EnumGameState.Pause))
                    {
                        if (m_gameState.Value == EnumGameState.MainMenu)
                        {
                            m_gameStates[gameState] = new GameState();
                            m_gameStates[gameState].Start();
                        }
                        else if (m_gameState.Value == EnumGameState.Pause)
                        {
                            m_gameStates[gameState].Resume();
                        }

                        stateHasChanged = true;
                    }
                    break;
                case EnumGameState.Pause:
                    if (m_gameState.HasValue
                        && m_gameState.Value == EnumGameState.Game)
                    {
                        m_gameStates[m_gameState.Value].Pause();

                        m_gameStates[gameState] = new PauseState();
                        m_gameStates[gameState].Start();

                        stateHasChanged = true;
                    }
                    break;
            }


            if (stateHasChanged)
            {
                DebugGame.Log(
                    "StateManager",
                    "SetGameState",
                    string.Format("L'état du jeu a été modifié ! ({0} => {1})",
                    m_gameState, gameState));

                this.m_gameState = gameState;
            }
            else
            {
                DebugGame.Log(
                    "StateManager",
                    "SetGameState",
                    string.Format("L'état du jeu n'a pas pu être modifié ! ({0} => {1})",
                    m_gameState, gameState));
            }
        }

        public void Update(double time)
        {
            if (m_gameState.HasValue)
            {
                m_gameStates[m_gameState.Value].Update(time);
            }
        }

        public void Draw(MySpriteBatch spritebatch)
        {
            if (m_gameState.HasValue)
            {
                m_gameStates[m_gameState.Value].Draw(spritebatch);
            }
        }
    }
}

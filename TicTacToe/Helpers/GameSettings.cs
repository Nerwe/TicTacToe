using TicTacToe.Model;

namespace TicTacToe.Helpers
{
    public class GameSettings
    {
        private static GameSettings instance;
        public GamePreferences CurrentGame { get; private set; }

        private GameSettings() { }

        public static GameSettings Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameSettings();
                }
                return instance;
            }
        }

        public void SetCurrentGame(GamePreferences game)
        {
            CurrentGame = game;
        }

        public void ClearCurrentGame()
        {
            CurrentGame = null;
        }
    }

}

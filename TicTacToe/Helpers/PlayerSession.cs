using TicTacToe.Model;

namespace TicTacToe.Helpers
{
    public class PlayerSession
    {
        private static PlayerSession instance;
        public PlayerModel CurrentPlayer { get; private set; }

        private PlayerSession() { }

        public static PlayerSession Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PlayerSession();
                }
                return instance;
            }
        }

        public void SetCurrentPlayer(PlayerModel player)
        {
            CurrentPlayer = player;
        }

        public void ClearCurrentPlayer()
        {
            CurrentPlayer = null;
        }
    }

}

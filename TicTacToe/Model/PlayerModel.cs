namespace TicTacToe.Model
{
    public class PlayerModel
    {
        public int PlayerID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Draws { get; set; }
        public int Rank { get; set; }
    }
}

using System;
using TicTacToe.Model;

namespace TicTacToe.Services
{
    public class Game
    {
        public CellType[,] Board { get; private set; }
        public bool IsPlayerTurn { get; private set; }

        public Game()
        {
            Board = new CellType[3, 3];
            IsPlayerTurn = true;
        }

        public void MakeMove(int row, int col)
        {
            if (Board[row, col] == CellType.Empty)
            {
                Board[row, col] = IsPlayerTurn ? CellType.Cross : CellType.Circle;
                IsPlayerTurn = !IsPlayerTurn;
            }
        }

        public (int row, int col) MakeBotMove()
        {
            Random random = new Random();
            int row, col;
            do
            {
                row = random.Next(3);
                col = random.Next(3);
            } while (Board[row, col] != CellType.Empty);

            Board[row, col] = CellType.Circle;
            MakeMove(row, col);

            return (row, col);
        }

        public bool IsGameOver()
        {
            return CheckForWin(CellType.Cross) || CheckForWin(CellType.Circle) || CheckForDraw();
        }

        private bool CheckForWin(CellType player)
        {
            // Horizontal check
            for (int row = 0; row < 3; row++)
            {
                if (Board[row, 0] == player && Board[row, 1] == player && Board[row, 2] == player)
                {
                    return true;
                }
            }

            // Vertical check
            for (int col = 0; col < 3; col++)
            {
                if (Board[0, col] == player && Board[1, col] == player && Board[2, col] == player)
                {
                    return true;
                }
            }

            // Diagonal check
            if ((Board[0, 0] == player && Board[1, 1] == player && Board[2, 2] == player) ||
                (Board[0, 2] == player && Board[1, 1] == player && Board[2, 0] == player))
            {
                return true;
            }

            return false;
        }

        private bool CheckForDraw()
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (Board[row, col] == CellType.Empty)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }

}

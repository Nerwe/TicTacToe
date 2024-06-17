using System;
using System.Collections.ObjectModel;
using System.Windows.Documents;
using TicTacToe.Model;

namespace TicTacToe.Services
{
    public class Game
    {
        public ObservableCollection<ObservableCollection<CellType>> Board { get; private set; }
        public bool IsPlayerTurn { get;  set; }
        public string GameResult { get; set; }

        public Game()
        {
            Board = new ObservableCollection<ObservableCollection<CellType>>();

            FillBoard();
            IsPlayerTurn = true;
        }

        private void FillBoard()
        {
            var newBoard = new ObservableCollection<ObservableCollection<CellType>>();
            for (int i = 0; i < 3; i++)
            {
                var row = new ObservableCollection<CellType>();
                for (int j = 0; j < 3; j++)
                {
                    row.Add(CellType.Empty);
                }
                newBoard.Add(row);
            }
            Board = newBoard;
        }

        public void RestartGame()
        {
            Board.Clear();
            FillBoard();
        }

        public void MakeMove(int row, int col)
        {
            if (Board[row][col] == CellType.Empty)
            {
                Board[row][col] = IsPlayerTurn ? CellType.Cross : CellType.Circle;
                IsPlayerTurn = !IsPlayerTurn;
            }
        }
        public (int row, int col) MakeBotMove(string difficulty)
        {
            switch (difficulty)
            {
                case "Noob":
                    return NoobBot();
                case "Defence":
                    return DefenceBot();
                case "Attack":
                    return AttackBot();
                case "Pro":
                    return ProBot();
                case "AI":
                    return AIBot();
                default:
                    return NoobBot();
            }
        }

        public bool IsGameOver()
        {
            return CheckForWin(CellType.Cross) || CheckForWin(CellType.Circle) || CheckForDraw();
        }
        private bool CheckForWin(CellType player)
        {
            // Проверка по горизонтали
            for (int row = 0; row < 3; row++)
            {
                if (Board[row][0] == player && Board[row][1] == player && Board[row][2] == player)
                {
                    IsPlayerTurn = player == CellType.Cross;
                    GameResult = player.ToString();
                    return true;
                }
            }

            // Проверка по вертикали
            for (int col = 0; col < 3; col++)
            {
                if (Board[0][col] == player && Board[1][col] == player && Board[2][col] == player)
                {
                    IsPlayerTurn = player == CellType.Cross;
                    GameResult = player.ToString();
                    return true;
                }
            }

            // Проверка по диагонали
            if ((Board[0][0] == player && Board[1][1] == player && Board[2][2] == player) ||
                (Board[0][2] == player && Board[1][1] == player && Board[2][0] == player))
            {
                IsPlayerTurn = player == CellType.Cross;
                GameResult = player.ToString();
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
                    if (Board[row][col] == CellType.Empty)
                    {
                        IsPlayerTurn = true;
                        GameResult = "Draw";
                        return false;
                    }
                }
            }
            return true;
        }

        private (int row, int col) NoobBot()
        {
            Random random = new Random();
            int row, col;
            do
            {
                row = random.Next(3);
                col = random.Next(3);
            } while (Board[row][col] != CellType.Empty);

            Board[row][col] = CellType.Circle;
            IsPlayerTurn = !IsPlayerTurn;

            return (row, col);
        }
        private (int row, int col) DefenceBot()
        {
            // Строки
            for (int i = 0; i < 3; i++)
            {
                if (Board[i][0] == CellType.Cross && Board[i][1] == CellType.Cross && Board[i][2] == CellType.Empty)
                {
                    MakeMove(i, 2);
                    return (i, 2);
                }
                if (Board[i][0] == CellType.Cross && Board[i][2] == CellType.Cross && Board[i][1] == CellType.Empty)
                {
                    MakeMove(i, 1);
                    return (i, 1);
                }
                if (Board[i][1] == CellType.Cross && Board[i][2] == CellType.Cross && Board[i][0] == CellType.Empty)
                {
                    MakeMove(i, 0);
                    return (i, 0);
                }
            }

            // Колонки
            for (int j = 0; j < 3; j++)
            {
                if (Board[0][j] == CellType.Cross && Board[1][j] == CellType.Cross && Board[2][j] == CellType.Empty)
                {
                    MakeMove(2, j);
                    return (2, j);
                }
                if (Board[0][j] == CellType.Cross && Board[2][j] == CellType.Cross && Board[1][j] == CellType.Empty)
                {
                    MakeMove(1, j);
                    return (1, j);
                }
                if (Board[1][j] == CellType.Cross && Board[2][j] == CellType.Cross && Board[0][j] == CellType.Empty)
                {
                    MakeMove(0, j);
                    return (0, j);
                }
            }

            // Диагонали
            if (Board[0][0] == CellType.Cross && Board[1][1] == CellType.Cross && Board[2][2] == CellType.Empty)
            {
                MakeMove(2, 2);
                return (2, 2);
            }
            if (Board[0][0] == CellType.Cross && Board[2][2] == CellType.Cross && Board[1][1] == CellType.Empty)
            {
                MakeMove(1, 1);
                return (1, 1);
            }
            if (Board[1][1] == CellType.Cross && Board[2][2] == CellType.Cross && Board[0][0] == CellType.Empty)
            {
                MakeMove(0, 0);
                return (0, 0);
            }
            if (Board[0][2] == CellType.Cross && Board[1][1] == CellType.Cross && Board[2][0] == CellType.Empty)
            {
                MakeMove(2, 0);
                return (2, 0);
            }
            if (Board[0][2] == CellType.Cross && Board[2][0] == CellType.Cross && Board[1][1] == CellType.Empty)
            {
                MakeMove(1, 1);
                return (1, 1);
            }
            if (Board[1][1] == CellType.Cross && Board[2][0] == CellType.Cross && Board[0][2] == CellType.Empty)
            {
                MakeMove(0, 2);
                return (0, 2);
            }

            return NoobBot();
        }
        public (int row, int col) AttackBot()
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (Board[row][col] == CellType.Empty)
                    {
                        Board[row][col] = IsPlayerTurn ? CellType.Cross : CellType.Circle;

                        if (CheckForWin(IsPlayerTurn ? CellType.Cross : CellType.Circle))
                        {
                            IsPlayerTurn = true;
                            return (row, col);
                        }
                        Board[row][col] = CellType.Empty;
                    }
                }
            }

            return NoobBot();
        }
        public (int row, int col) ProBot()
        {
            int bestScore = int.MinValue;
            (int row, int col) bestMove = (-1, -1);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (Board[i][j] == CellType.Empty)
                    {
                        Board[i][j] = CellType.Circle;

                        int score = Minimax(Board, false);

                        Board[i][j] = CellType.Empty;

                        if (score > bestScore)
                        {
                            bestScore = score;
                            bestMove = (i, j);
                        }
                    }
                }
            }

            Board[bestMove.row][bestMove.col] = CellType.Circle;
            IsPlayerTurn = true;

            return bestMove;
        }
        private (int row, int col) AIBot()
        {
            return NoobBot();
        }

        private int Minimax(ObservableCollection<ObservableCollection<CellType>> board, bool isMaximizing)
        {
            if (IsGameOver())
            {
                if (GameResult == "Draw")
                    return 0;
                else if (GameResult == CellType.Circle.ToString())
                    return 10;
                else
                    return -10;
            }

            if (isMaximizing)
            {
                int bestScore = int.MinValue;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (board[i][j] == CellType.Empty)
                        {
                            board[i][j] = CellType.Circle;
                            bestScore = Math.Max(bestScore, Minimax(board, false));
                            board[i][j] = CellType.Empty;
                        }
                    }
                }
                return bestScore;
            }
            else
            {
                int bestScore = int.MaxValue;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (board[i][j] == CellType.Empty)
                        {
                            board[i][j] = CellType.Cross;
                            bestScore = Math.Min(bestScore, Minimax(board, true));
                            board[i][j] = CellType.Empty;
                        }
                    }
                }
                return bestScore;
            }
        }
    }
}
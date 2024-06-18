using System;
using System.Collections.ObjectModel;
using TicTacToe.Model;

namespace TicTacToe.Services
{
    public class Game
    {
        public ObservableCollection<ObservableCollection<CellType>> Board { get; private set; }
        public bool IsPlayerTurn { get; set; }
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
                    return ProBot(CellType.Circle);
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
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (Board[row][col] == CellType.Empty)
                    {
                        Board[row][col] = CellType.Cross;
                        if (CheckForWin(CellType.Cross))
                        {
                            Board[row][col] = CellType.Circle;
                            return (row, col);
                        }
                        Board[row][col] = CellType.Empty;
                    }
                }
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
                        Board[row][col] = CellType.Circle;

                        if (CheckForWin(CellType.Circle))
                        {
                            return (row, col);
                        }
                        Board[row][col] = CellType.Empty;
                    }
                }
            }

            return NoobBot();
        }
        public (int row, int col) ProBot(CellType cell)
        {
            int bestScore = int.MinValue;
            (int row, int col) bestMove = (-1, -1);

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (Board[i][j] == CellType.Empty)
                    {
                        Board[i][j] = cell;

                        int score = Minimax(Board, false, cell);

                        Board[i][j] = CellType.Empty;

                        if (score > bestScore)
                        {
                            bestScore = score;
                            bestMove = (i, j);
                        }
                    }
                }
            }

            if (cell == CellType.Circle)
                Board[bestMove.row][bestMove.col] = cell;
            IsPlayerTurn = true;

            return bestMove;
        }
        private (int row, int col) AIBot()
        {
            return NoobBot();
        }

        private int Minimax(ObservableCollection<ObservableCollection<CellType>> board, bool isMaximizing, CellType cell)
        {
            if (IsGameOver())
            {
                if (GameResult == "Draw")
                    return 0;
                else if (GameResult == cell.ToString())
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
                            board[i][j] = cell;
                            bestScore = Math.Max(bestScore, Minimax(board, false, cell));
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
                            board[i][j] = cell == CellType.Circle ? CellType.Cross : CellType.Circle;
                            bestScore = Math.Min(bestScore, Minimax(board, true, cell));
                            board[i][j] = CellType.Empty;
                        }
                    }
                }
                return bestScore;
            }
        }

        public (int row, int col) HintMove()
        {
            return ProBot(CellType.Cross);
        }
    }
}
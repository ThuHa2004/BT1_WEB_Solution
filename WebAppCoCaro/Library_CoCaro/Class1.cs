using System;

namespace Library_CoCaro
{
    public class CaroGame
    {
        private int[,] board = new int[20, 20]; // Ban co 20x20, 0=rong, 1=X, 2=O
        private int currentPlayer = 1;
        private Random rand = new Random();
        private bool gameEnded = false;

        public int Rows { get { return 20; } }
        public int Cols { get { return 20; } }
        public int CurrentPlayer { get { return currentPlayer; } set { currentPlayer = value; } }
        public int[,] Board { get { return (int[,])board.Clone(); } set { board = value; } }
        public int LastMoveRow { get; private set; }
        public int LastMoveCol { get; private set; }
        public int Mode { get; set; } // 1: Bot vs Nguoi, 2: 2 Nguoi
        public bool GameEnded { get { return gameEnded; } }

        public object WinningCells { get; set; }

        public string GetBoardDisplay()
        {
            string display = "   ";
            for (int j = 0; j < 20; j++)
            {
                display += j.ToString().PadLeft(2) + " ";
            }
            display += "\n";

            for (int i = 0; i < 20; i++)
            {
                display += i.ToString().PadLeft(2) + " ";
                for (int j = 0; j < 20; j++)
                {
                    display += board[i, j] == 0 ? ".  " : (board[i, j] == 1 ? "X  " : "O  ");
                }
                display += "\n";
            }
            return display;
        }

        public string CheckWin()
        {
            if (gameEnded) return "";

            int x = LastMoveRow;
            int y = LastMoveCol;
            if (board[x, y] == currentPlayer)
            {
                if (CheckDirection(x, y, 0, 1, currentPlayer, 5) || // ngang
                    CheckDirection(x, y, 1, 0, currentPlayer, 5) || // doc
                    CheckDirection(x, y, 1, 1, currentPlayer, 5) || // cheo xuong
                    CheckDirection(x, y, 1, -1, currentPlayer, 5))  // cheo len
                {
                    gameEnded = true;
                    return currentPlayer == 1 ? "X thang!" : "O thang!";
                }
            }

            return IsBoardFull() ? "Hoa!" : "Tiep tuc";
        }

        private bool CheckDirection(int x, int y, int dx, int dy, int player, int length)
        {
            int count = 1;
            for (int i = 1; i < length; i++)
            {
                if (x + i * dx >= 0 && x + i * dx < 20 &&
                    y + i * dy >= 0 && y + i * dy < 20 &&
                    board[x + i * dx, y + i * dy] == player) count++;
                else break;
            }
            for (int i = 1; i < length; i++)
            {
                if (x - i * dx >= 0 && x - i * dx < 20 &&
                    y - i * dy >= 0 && y - i * dy < 20 &&
                    board[x - i * dx, y - i * dy] == player) count++;
                else break;
            }
            return count >= 5;
        }

        private bool IsBoardFull()
        {
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 20; j++)
                    if (board[i, j] == 0) return false;
            gameEnded = true;
            return true;
        }

        public bool MakeMove(int row, int col)
        {
            if (gameEnded) return false;
            if (row >= 0 && row < 20 && col >= 0 && col < 20 && board[row, col] == 0)
            {
                board[row, col] = currentPlayer;
                LastMoveRow = row;
                LastMoveCol = col;

                string result = CheckWin();
                if (!string.IsNullOrEmpty(result) && result != "Tiep tuc")
                {
                    gameEnded = true;
                    return true;
                }

                if (Mode == 1 && currentPlayer == 1)
                {
                    currentPlayer = 2;
                    BotMove();
                    result = CheckWin();
                    if (!string.IsNullOrEmpty(result) && result != "Tiep tuc")
                    {
                        gameEnded = true;
                        return true;
                    }
                    currentPlayer = 1;
                }
                else if (Mode == 2)
                {
                    currentPlayer = currentPlayer == 1 ? 2 : 1;
                }
                return true;
            }
            return false;
        }

        private void BotMove()
        {
            if (gameEnded) return;

            if (FindWinningMove(2, out int winRow, out int winCol))
            {
                board[winRow, winCol] = 2;
                LastMoveRow = winRow;
                LastMoveCol = winCol;
                return;
            }

            if (FindBlockingMove(1, out int blockRow, out int blockCol))
            {
                board[blockRow, blockCol] = 2;
                LastMoveRow = blockRow;
                LastMoveCol = blockCol;
                return;
            }

            int attempts = 0;
            while (attempts < 400)
            {
                int row = rand.Next(20);
                int col = rand.Next(20);
                if (board[row, col] == 0)
                {
                    board[row, col] = 2;
                    LastMoveRow = row;
                    LastMoveCol = col;
                    break;
                }
                attempts++;
            }
        }

        private bool FindWinningMove(int player, out int winRow, out int winCol)
        {
            winRow = -1;
            winCol = -1;
            int[][] directions = new int[][] {
                new int[] { 0, 1 }, new int[] { 1, 0 },
                new int[] { 1, 1 }, new int[] { 1, -1 }
            };

            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    if (board[i, j] != 0) continue;

                    foreach (int[] dir in directions)
                    {
                        int dx = dir[0], dy = dir[1];
                        int countForward = CountInDirection(i, j, dx, dy, player);
                        int countBackward = CountInDirection(i, j, -dx, -dy, player);
                        if (countForward + countBackward >= 4)
                        {
                            winRow = i;
                            winCol = j;
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private bool FindBlockingMove(int player, out int blockRow, out int blockCol)
        {
            blockRow = -1;
            blockCol = -1;
            int[][] directions = new int[][] {
                new int[] { 0, 1 }, new int[] { 1, 0 },
                new int[] { 1, 1 }, new int[] { 1, -1 }
            };

            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    if (board[i, j] != 0) continue;

                    foreach (int[] dir in directions)
                    {
                        int dx = dir[0], dy = dir[1];
                        int countForward = CountInDirection(i, j, dx, dy, player);
                        int countBackward = CountInDirection(i, j, -dx, -dy, player);
                        if (countForward + countBackward >= 3)
                        {
                            blockRow = i;
                            blockCol = j;
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private int CountInDirection(int x, int y, int dx, int dy, int player)
        {
            int count = 0;
            x += dx;
            y += dy;
            while (x >= 0 && x < 20 && y >= 0 && y < 20 && board[x, y] == player)
            {
                count++;
                x += dx;
                y += dy;
            }
            return count;
        }
    }
}

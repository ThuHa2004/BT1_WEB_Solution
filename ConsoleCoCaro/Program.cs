using System;
using Library_CoCaro;

namespace ConsoleCoCaro
{
    class Program
    {
        static void Main(string[] args)
        {
            CaroGame game = new CaroGame();
            Console.WriteLine("Chao mung den voi Co Caro 20x20 - Phong cach rieng!");
            Console.WriteLine("Chon che do: 1 (Bot vs Nguoi), 2 (2 Nguoi)");
            int mode = int.Parse(Console.ReadLine());
            game.Mode = mode;
            Console.WriteLine("Nhap hang va cot (0-9), cach nhau bang dau cach. Nhap 'q' de thoat.");

            while (true)
            {
                Console.WriteLine(game.GetBoardDisplay());
                if (game.GameEnded)
                {
                    Console.WriteLine("Game ket thuc! Nhap 'q' de thoat.");
                    if (Console.ReadLine().ToLower() == "q") break;
                    continue;
                }
                Console.Write("Luot cua " + (game.CurrentPlayer == 1 ? "X" : "O") + ": ");
                string input = Console.ReadLine();
                if (input.ToLower() == "q") break;

                string[] parts = input.Split(' ');
                if (parts.Length == 2 && int.TryParse(parts[0], out int row) && int.TryParse(parts[1], out int col))
                {
                    if (game.MakeMove(row, col))
                    {
                        Console.WriteLine($"Da danh tai ({row}, {col})");
                        if (game.GameEnded)
                        {
                            Console.WriteLine("Ket qua: " + game.CheckWin());
                        }
                    }

                    else
                    {
                        Console.WriteLine("O da chiem hoac khong hop le!");
                    }
                }
                else
                {
                    Console.WriteLine("Nhap sai dinh dang!");
                }
            }
            Console.WriteLine("Cam on da choi - Tam biet!");
        }
    }
}
using System;
using System.Web;
using System.Web.UI;
using Library_CoCaro;

namespace WebAppCoCaro
{
    public partial class api : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = Request.QueryString["action"];
            if (string.IsNullOrEmpty(action))
            {
                Response.StatusCode = 400;
                Response.Write("{\"success\": false, \"message\": \"Action is required\"}");
                return;
            }

            Response.ContentType = "application/json";

            int mode;
            if (!int.TryParse(Request.QueryString["mode"], out mode))
            {
                mode = 1;
            }

            // Lấy game từ Session, nếu chưa có hoặc reset thì tạo mới
            CaroGame game = Session["caroGame"] as CaroGame;
            if (game == null || action.ToLower() == "reset")
            {
                game = new CaroGame();
                game.Mode = mode;
                Session["caroGame"] = game;
            }
            else
            {
                game.Mode = mode;
            }

            switch (action.ToLower())
            {
                case "move":
                    int row, col;
                    if (!int.TryParse(Request.QueryString["row"], out row)) row = -1;
                    if (!int.TryParse(Request.QueryString["col"], out col)) col = -1;

                    if (row >= 0 && col >= 0) // Người đi (client gửi tọa độ)
                    {
                        if (game.MakeMove(row, col))
                        {
                            string result = game.CheckWin();
                            Response.Write(GetJsonResponse(true, result, game.Board, game.CurrentPlayer, game.GameEnded));
                        }
                        else
                        {
                            Response.Write(GetJsonResponse(false, "Nuoc di khong hop le!", game.Board, game.CurrentPlayer, game.GameEnded));
                        }
                    }
                    else if (row == -1 && col == -1 && mode == 1) // Bot di, chỉ khi mode = 1
                    {
                        Random rand = new Random();
                        int attempts = 0;
                        while (attempts < 400)
                        {
                            int botRow = rand.Next(20);
                            int botCol = rand.Next(20);
                            if (game.Board[botRow, botCol] == 0)
                            {
                                game.MakeMove(botRow, botCol);
                                break;
                            }
                            attempts++;
                        }
                        string result = game.CheckWin();
                        Response.Write(GetJsonResponse(true, result, game.Board, game.CurrentPlayer, game.GameEnded));
                    }
                    else
                    {
                        Response.Write(GetJsonResponse(false, "Tham so khong hop le!", game.Board, game.CurrentPlayer, game.GameEnded));
                    }
                    break;

                case "reset":
                    game = new CaroGame();
                    game.Mode = mode;
                    Session["caroGame"] = game;
                    Response.Write(GetJsonResponse(true, "Tro choi da reset!", game.Board, game.CurrentPlayer, game.GameEnded));
                    break;

                default:
                    Response.StatusCode = 400;
                    Response.Write("{\"success\": false, \"message\": \"Action khong hop le\"}");
                    break;
            }
        }

        private string GetJsonResponse(bool success, string result, int[,] board, int currentPlayer, bool gameEnded)
        {
            string jsonBoard = SerializeBoard(board);

            // Escape dấu nháy trong result
            if (string.IsNullOrEmpty(result)) result = "";
            result = result.Replace("\\", "\\\\").Replace("\"", "\\\"");

            return "{"
                + "\"success\": " + success.ToString().ToLower()
                + ", \"result\": \"" + result + "\""
                + ", \"currentPlayer\": " + currentPlayer
                + ", \"gameEnded\": " + gameEnded.ToString().ToLower()
                + ", \"board\": " + jsonBoard
                + "}";
        }

        private string SerializeBoard(int[,] board)
        {
            string json = "[";
            for (int i = 0; i < 20; i++)
            {
                json += "[";
                for (int j = 0; j < 20; j++)
                {
                    json += board[i, j];
                    if (j < 19) json += ",";
                }
                json += "]";
                if (i < 19) json += ",";
            }
            json += "]";
            return json;
        }
    }
}
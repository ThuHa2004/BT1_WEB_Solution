using System;
using System.Drawing;
using System.Windows.Forms;
using Library_CoCaro;

namespace WinFormCoCaro
{
    public partial class Form1 : Form
    {
        private CaroGame game = new CaroGame();
        private const int CellSize = 30; // Kích thước mỗi ô
        private bool isPaused = false; // Trạng thái tạm dừng
        private bool isGameStarted = false; // Trạng thái game đã bắt đầu

        public Form1()
        {
            InitializeComponent();

            // Tùy chỉnh giao diện
            this.BackColor = Color.LightSkyBlue; // Nền form màu xanh nhạt
            panel1.BackColor = Color.White; // Nền bàn cờ trắng
            panel1.BorderStyle = BorderStyle.Fixed3D; // Viền 3D cho panel
            comboBox1.BackColor = Color.WhiteSmoke; // Nền ComboBox sáng
            label1.BackColor = Color.White; // Nền Label sáng
            label1.Font = new Font("Arial", 14, FontStyle.Bold); // Font chữ đậm
            button1.BackColor = Color.LightGreen; // Màu nút Bắt đầu
            button2.BackColor = Color.LightCoral; // Màu nút Chơi lại
            button3.BackColor = Color.LightYellow; // Màu nút Tiếp tục/Tạm dừng

            // Đặt tên nút
            button1.Text = "Bắt đầu";
            button2.Text = "Chơi lại";
            button3.Text = "Tạm dừng";

            // Cài đặt ComboBox
            comboBox1.Items.Add("Chơi với BOT");
            comboBox1.Items.Add("Chơi với người");
            comboBox1.SelectedIndex = 0;
            game.Mode = 1;
            comboBox1.SelectedIndexChanged += (s, e) =>
            {
                if (isGameStarted)
                {
                    game = new CaroGame { Mode = comboBox1.SelectedIndex + 1 };
                    isPaused = false;
                    button3.Text = "Tạm dừng";
                    label1.Text = "Trò chơi đã reset theo chế độ mới! Lượt của X.";
                    panel1.Invalidate();
                }
                else
                {
                    label1.Text = "Vui lòng nhấn 'Bắt đầu' để chơi!";
                }
            };

            // Gán sự kiện
            panel1.Paint += Panel1_Paint;
            panel1.MouseClick += Panel1_MouseClick;
            button1.Click += Button1_Click;
            button2.Click += Button2_Click;
            button3.Click += Button3_Click;

            // Cập nhật giao diện ban đầu
            label1.Text = "Vui lòng nhấn 'Bắt đầu' để chơi!";
            panel1.Invalidate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Không cần logic đặc biệt khi load
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.DarkGreen, 2); // Viền ô màu xanh đậm

            // Vẽ lưới 20x20
            for (int i = 0; i <= 20; i++)
            {
                g.DrawLine(pen, 0, i * CellSize, 20 * CellSize, i * CellSize); // Đường ngang
                g.DrawLine(pen, i * CellSize, 0, i * CellSize, 20 * CellSize); // Đường dọc
            }

            // Vẽ ký hiệu X và O
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    if (game.Board[i, j] == 1) // X
                    {
                        g.DrawString("X", new Font("Arial", 14, FontStyle.Bold), Brushes.Red, j * CellSize + 5, i * CellSize + 5);
                    }
                    else if (game.Board[i, j] == 2) // O
                    {
                        g.DrawEllipse(new Pen(Color.Blue, 2), j * CellSize + 5, i * CellSize + 5, 15, 15);
                    }
                }
            }
        }

        private void Panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (!isGameStarted || game.GameEnded || isPaused)
            {
                if (!isGameStarted)
                    label1.Text = "Vui lòng nhấn 'Bắt đầu' để chơi!";
                else if (game.GameEnded)
                    label1.Text = "Game kết thúc!  Nhấn ' Chơi lại' để tiếp tục." ;
                else
                    label1.Text = "Đã tạm dừng! Nhấn 'Tiếp tục' để chơi tiếp.";
                return;
            }

            int row = e.Y / CellSize;
            int col = e.X / CellSize;
            if (row >= 0 && row < 20 && col >= 0 && col < 20)
            {
                if (game.MakeMove(row, col))
                {
                    panel1.Invalidate();
                    if (game.GameEnded)
                    {
                        label1.Text = "Kết quả: " + game.CheckWin();
                    }
                    else
                    {
                        label1.Text = $"Đã đánh tại ({row}, {col}) - Lượt của {(game.CurrentPlayer == 1 ? "X" : "O")}";
                        if (game.Mode == 1 && game.CurrentPlayer == 2) // Bot di
                        {
                            game.MakeMove(-1, -1); // Gọi bot di
                            panel1.Invalidate();
                            if (game.GameEnded)
                            {
                                label1.Text = "Kết quả: " + game.CheckWin();
                            }
                            else
                            {
                                label1.Text = $"Bot đã đánh - Lượt của X";
                            }
                        }
                    }
                }
                else
                {
                    label1.Text = "O đã chiếm hoặc không hợp lệ!";
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            game = new CaroGame { Mode = comboBox1.SelectedIndex + 1 };
            isGameStarted = true;
            isPaused = false;
            button3.Text = "Tạm dừng";
            label1.Text = "Trò chơi bắt đầu! Lượt của X";
            panel1.Invalidate();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (isGameStarted)
            {
                game = new CaroGame { Mode = comboBox1.SelectedIndex + 1 };
                isPaused = false;
                button3.Text = "Tạm dừng";
                label1.Text = "Trò chơi đã reset! Lượt của X.";
                panel1.Invalidate();
            }
            else
            {
                label1.Text = "Vui lòng nhấn 'Bắt đầu' trước!";
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (!isGameStarted || game.GameEnded) return;

            isPaused = !isPaused;
            button3.Text = isPaused ? "Tiếp tục" : "Tạm dừng";
            label1.Text = isPaused ? "Đã tạm dừng! Nhấn 'Tiếp tục' để chơi tiếp." : $"Lượt của {(game.CurrentPlayer == 1 ? "X" : "O")}";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }
    }
}
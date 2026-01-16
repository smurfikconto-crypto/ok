using System;
using System.Drawing;
using System.Windows.Forms;

namespace Projekt_ZTP
{
    public class MainForm : Form
    {
        private Button newGameButton;
        private Panel playerBoardPanel;
        private Panel aiBoardPanel;
        private Label statusLabel;

        public MainForm()
        {
            Text = "Battleship - Prototype";
            ClientSize = new Size(900, 500);
            StartPosition = FormStartPosition.CenterScreen;

            InitControls();
        }

        private void InitControls()
        {
            newGameButton = new Button
            {
                Text = "Nowa gra",
                Location = new Point(20, 20),
                Size = new Size(120, 40)
            };
            newGameButton.Click += NewGameButton_Click;

            statusLabel = new Label
            {
                Text = "Kliknij \"Nowa gra\"",
                AutoSize = true,
                Location = new Point(160, 30)
            };

            playerBoardPanel = new Panel
            {
                Location = new Point(20, 80),
                Size = new Size(350, 350),
                BorderStyle = BorderStyle.FixedSingle
            };

            aiBoardPanel = new Panel
            {
                Location = new Point(430, 80),
                Size = new Size(350, 350),
                BorderStyle = BorderStyle.FixedSingle
            };

            Controls.Add(newGameButton);
            Controls.Add(statusLabel);
            Controls.Add(playerBoardPanel);
            Controls.Add(aiBoardPanel);
        }

        private void NewGameButton_Click(object? sender, EventArgs e)
        {
            // Singleton
            var gm = GameManager.Instance;
            gm.StartGame();

            BuildBoards();
            statusLabel.Text = $"Tura gracza: {gm.CurrentPlayer?.GetName()}";
        }

        private void BuildBoards()
        {
            BuildBoardButtons(playerBoardPanel, isPlayer: true);
            BuildBoardButtons(aiBoardPanel, isPlayer: false);
        }

        private void BuildBoardButtons(Panel host, bool isPlayer)
        {
            host.Controls.Clear();

            int cellSize = 30;
            for (int row = 0; row < 10; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    var btn = new Button
                    {
                        Size = new Size(cellSize, cellSize),
                        Location = new Point(col * cellSize, row * cellSize),
                        Tag = new Position(row, col),
                        Margin = Padding.Empty
                    };

                    if (!isPlayer)
                        btn.Click += AiBoardButton_Click;

                    host.Controls.Add(btn);
                }
            }
        }

        private void AiBoardButton_Click(object? sender, EventArgs e)
        {
            if (sender is not Button btn || btn.Tag is not Position pos)
                return;

            var gm = GameManager.Instance;
            var currentGame = gm.CurrentGame;
            if (currentGame == null) return;

            var result = currentGame.AiBoard.ReceiveShot(pos);
            btn.Enabled = false;

            switch (result)
            {
                case ShotResult.MISS:
                    btn.BackColor = Color.LightGray;
                    break;
                case ShotResult.HIT:
                    btn.BackColor = Color.OrangeRed;
                    break;
                case ShotResult.SUNK:
                    btn.BackColor = Color.DarkRed;
                    break;
            }

            if (currentGame.AiBoard.IsGameOver())
            {
                MessageBox.Show("Wygrałeś!");
            }
        }
    }
}

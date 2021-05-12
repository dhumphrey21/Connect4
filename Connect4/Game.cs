using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Connect4
{
    public class Game
    {
        public string CurrentPlayer { get; set; }
        public PictureBox[,] GameBoard { get; set; }
        public bool Winner { get; set; }
        public Panel GamePanel { get; set; }

        Label turn = new Label();

        int button1Clicks;
        int button2Clicks;
        int button3Clicks;
        int button4Clicks;
        int button5Clicks;
        int button6Clicks;
        int button7Clicks;
        int numMoves;

        public Game(Panel theGamePanel)
        {
            GamePanel = theGamePanel;
            CurrentPlayer = "Red";
            Winner = false;
            GameBoard = new PictureBox[6, 7];
            button1Clicks = 0;
            button2Clicks = 0;
            button3Clicks = 0;
            button4Clicks = 0;
            button5Clicks = 0;
            button6Clicks = 0;
            button7Clicks = 0;
            numMoves = 0;
        }

        // Reset game
        public void StartGame()
        {
            Winner = false;
            GamePanel.Controls.Clear();
            GamePanel.Enabled = true;
            DrawBoard();
            PickStarter();
            button1Clicks = 0;
            button2Clicks = 0;
            button3Clicks = 0;
            button4Clicks = 0;
            button5Clicks = 0;
            button6Clicks = 0;
            button7Clicks = 0;
            numMoves = 0;
        }

        // Randomly determine a player to start the game
        private void PickStarter()
        {
            Random rnd = new Random();
            if (rnd.Next(0, 2) == 0)
            {
                CurrentPlayer = "Red";
            }
            else
            {
                CurrentPlayer = "Yellow";
            }
            turn.Text = CurrentPlayer + "'s turn:";
        }

        private void DrawBoard()
        {
            int left = 0;
            int top = 75;
            int buttonLeft = 0;
            int buttonTop = 50;

            turn.Location = new System.Drawing.Point(0, 10);
            GamePanel.Controls.Add(turn);
            for (int col = 0; col <= GameBoard.GetUpperBound(1); col++)
            {
                Button aButton = new Button();
                aButton.Text = "Drop Tile";
                aButton.Location = new System.Drawing.Point(buttonLeft, buttonTop);
                aButton.Tag = col;
                aButton.Click += ButtonGotClicked;
                GamePanel.Controls.Add(aButton);
                buttonLeft += 75;
            }

            for (int row = 0; row <= GameBoard.GetUpperBound(0); row++)
            {

                left = 0;
                for (int col = 0; col <= GameBoard.GetUpperBound(1); col++)
                {
                    PictureBox aPictureBox = new PictureBox();
                    aPictureBox.Size = new System.Drawing.Size(75, 75);
                    aPictureBox.Location = new System.Drawing.Point(left, top);
                    aPictureBox.Image = Resource1.BlackToken;
                    GamePanel.Controls.Add(aPictureBox);
                    GameBoard[row, col] = aPictureBox;
                    GameBoard[row, col].Tag = "vacant";
                    left += 75;
                }
                top += 75;
            }
        }

        // Determine which button got clicked
        private void ButtonGotClicked(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;


            if ((int)clickedButton.Tag == 0)
            {
                button1Clicks++;
                MakeMove(0, button1Clicks);
            }
            else if ((int)clickedButton.Tag == 1)
            {
                button2Clicks++;
                MakeMove(1, button2Clicks);
            }
            else if ((int)clickedButton.Tag == 2)
            {
                button3Clicks++;
                MakeMove(2, button3Clicks);
            }
            else if ((int)clickedButton.Tag == 3)
            {
                button4Clicks++;
                MakeMove(3, button4Clicks);
            }
            else if ((int)clickedButton.Tag == 4)
            {
                button5Clicks++;
                MakeMove(4, button5Clicks);
            }
            else if ((int)clickedButton.Tag == 5)
            {
                button6Clicks++;
                MakeMove(5, button6Clicks);
            }
            else if ((int)clickedButton.Tag == 6)
            {
                button7Clicks++;
                MakeMove(6, button7Clicks);
            }
        }

        private void MakeMove(int col, int colClicks)
        {
            if (colClicks <= 6)
            {
                for (int row = 5; row <= GameBoard.GetUpperBound(0); row--)
                {
                    if ((string)GameBoard[row, col].Tag == "vacant")
                    {
                        if (CurrentPlayer == "Red")
                        {
                            GameBoard[row, col].Image = Resource1.RedTile;
                            GameBoard[row, col].Tag = "Red";
                            break;
                        }
                        else
                        {
                            GameBoard[row, col].Image = Resource1.YellowTile;
                            GameBoard[row, col].Tag = "Yellow";
                            break;
                        }
                    }
                }

                numMoves++;
                colClicks++;

                // Check for a winning sequence
                CheckForWinner();
                if(Winner)
                {
                    MessageBox.Show(CurrentPlayer + " wins!", "Game Message");
                    GamePanel.Enabled = false;
                }
                // Check to see if board is full
                else if (numMoves == 42)
                {
                    MessageBox.Show("The game is a draw.", "Game Message");
                    GamePanel.Enabled = false;
                }
                // If no winner, move on to next player
                else
                {
                    NextPlayer();
                }
            }
            else
            {
                MessageBox.Show("This column is already full!", "Game Message");
            }
        }

        private void CheckForWinner()
        {
            int count = 0;

            // Check the rows for a winner
            for (int row = 0; row <= GameBoard.GetUpperBound(0); row++)
            {
                count = 0;
                for (int col = 0; col <= GameBoard.GetUpperBound(1); col++)
                {
                    if ((string)GameBoard[row, col].Tag == CurrentPlayer)
                    {
                        count++;
                        if (count >= 4)
                        {
                            Winner = true;
                            row = GameBoard.GetUpperBound(0);

                        }

                    }
                    else
                    {
                        count = 0;
                    }
                }

            }

            count = 0;

            if (!Winner)
            {
                // Check the columns for a winner
                for (int col = 0; col <= GameBoard.GetUpperBound(1); col++)
                {
                    count = 0;
                    for (int row = 0; row <= GameBoard.GetUpperBound(0); row++)
                    {
                        if ((string)GameBoard[row, col].Tag == CurrentPlayer)
                        {
                            count++;

                            if (count >= 4)
                            {
                                Winner = true;
                                col = GameBoard.GetUpperBound(1);
                            }
                        }
                        else
                        {
                            count = 0;
                        }
                    }

                }
            }
        }

        // Alternate between players
        private void NextPlayer()
        {
            if (CurrentPlayer == "Red")
            {
                CurrentPlayer = "Yellow";
            }
            else
            {
                CurrentPlayer = "Red";
            }
            turn.Text = CurrentPlayer + "'s turn:";
        }
    }
}

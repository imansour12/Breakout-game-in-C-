using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Breakout_Game
{
    public partial class Form1 : Form
    {
        //Making the boolean variables for the game
        bool goLeft;
        bool goRight;
        bool isGameOver;

        //Making the integer variables for the game
        int score;
        int ballx;
        int bally;
        int playerSpeed;

        //Generates random number
        Random rnd = new Random();

        public Form1()
        {
            InitializeComponent();
            setupGame();
        }

        private void setupGame()
        {
            //This function sets up default stuff for the game

            score = 0;
            ballx = 5;
            bally = 5;
            playerSpeed = 12;
            txtScore.Text = "Score: " + score;

            //Starts up the event below

            gameTimer.Start();

            //Loops through the controls, Check on google later...

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "blocks")
                {
                    //Makes the colors fun and random
                    x.BackColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256)) ;
                }
            }
        }

        private void gameOver(string message)
        {
            isGameOver = true;
            gameTimer.Stop();
            txtScore.Text = "Score: " + score + " " + message;
        }

        private void mainGameTimerEvent(object sender, EventArgs e)
        {
            //Links the text score to the score integer
            txtScore.Text = "Score: " + score;

            //Sets the limits to how far the player can travel to not break the boundaries

            if (goLeft == true && player.Left>0)
            {
                player.Left -= playerSpeed;
            }

            if (goRight == true && player.Left<694)
            {
                player.Left += playerSpeed;
            }

            ball.Left += ballx;
            ball.Top += bally;

            if (ball.Left<0 || ball.Left>771)
            {
                ballx = -ballx;
            }

            if (ball.Top < 0)
            {
                bally = -bally;
            }

            if (ball.Bounds.IntersectsWith(player.Bounds))
            {
                bally = rnd.Next(5, 12) * -1;

                if (ballx < 0)
                {
                    ballx = rnd.Next(5, 12) * -1;
                }
                else
                {
                    ballx = rnd.Next(5,12);
                }

            }

            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "blocks")
                {
                    if (ball.Bounds.IntersectsWith(x.Bounds))
                    {
                        score += 1;

                        bally = -bally;

                        this.Controls.Remove(x);
                    }
                }
            }

        if (score == 15)
            {
                //When the score hits fifteen, it will show the end game message
                gameOver("YEE HAW YOU WINN!!!");
            }

        if (ball.Top > 657)
            {
                //If the ball touches the top side of the screen, it will display the lose message
                gameOver("hehe, you lost :))))");
            }

        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            //What to do when the left arrow is pressed
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            //What to do when the right arrow is pressed
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            //What to do when the left arrow is not pressed
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            //What to do when the right arrow is not pressed
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
        }
    }
}

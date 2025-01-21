using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrayNotify;
using System.Diagnostics;
namespace PLATFORMER
{
    public partial class Form1 : Form
    {

        private Stopwatch stopwatch = new Stopwatch();
        private int targetFPS = 15;
        private long fractureTime = (1000/15*1000)
            ;
        bool goLeft, goRight, jumping, hasKey;

        int jumpSpeed = 8;

        int force = 8;

        int score = 0;

        int playerSpeed = 8;

        int backgroundSpeed = 8;



        public Form1()
        {
            InitializeComponent();
            
        }

        private void MainTimerEvent(object sender, EventArgs e)
        {
            stopwatch.Restart();

            GameTimer.Interval = 16;
            
            player.Top += jumpSpeed;

            if (goLeft == true && player.Left > 60)
            {
                player.Left -= playerSpeed;
            }
            if (goRight == true && player.Left + (player.Width + 60) < this.ClientSize.Width)
            {
                player.Left += playerSpeed;
            }


            if (goLeft == true && background.Left < 0)
            {
                background.Left += backgroundSpeed;
                MoveGameElements("forward");
            }

            if (goRight == true && background.Left > -1372)
            {
                background.Left -= backgroundSpeed;
                MoveGameElements("back");
            }

            if (jumping == true)
            {
                jumpSpeed = -12;
                force -= 1;
            }
            else
            {
                jumpSpeed = 12;
            }

            if (jumping == true && force < 0)
            {
                jumping = false;
            }

            foreach (Control x in this.Controls)
            {

                if (x is PictureBox && (string)x.Tag == "platform")
                {

                    if (player.Bounds.IntersectsWith(x.Bounds) && jumping == false)
                    {

                        force = 8;

                        player.Top = x.Top - player.Height;

                        jumpSpeed = 0;

                    }

                    

                }
                if (x is PictureBox && (string)x.Tag == "coin")
                {


                    if (player.Bounds.IntersectsWith(x.Bounds) && x.Visible == true)
                    {
                        x.Visible = false;
                        score += 1;
                    }

                }


            }

            if (player.Bounds.IntersectsWith(key.Bounds))

            {

                key.Visible = false;

                hasKey = true;

            }

            if (player.Bounds.IntersectsWith(door.Bounds) && hasKey == true)
            {

                door.Image = Properties.Resources.door_open;
                GameTimer.Stop();
                MessageBox.Show("Well done, your Journey is complete! " + Environment.NewLine + "Click Enter To Play Again!");
                





            }



            if (player.Top + player.Height > this.ClientSize.Height)
            {


                GameTimer.Stop();


                GAME.Visible = true;


            }
            long elapsedTime = stopwatch.ElapsedTicks / (Stopwatch.Frequency/ (1000*1000));


        
            
        
        
        }
        




           

        private void KeyIsDown(object sender, KeyEventArgs e)

        {
            if (e.KeyCode == Keys.Enter)
            {
                RestartGame();
            }

            if (e.KeyCode == Keys.Left)
            {

                goLeft = true;

            }

            if (e.KeyCode == Keys.Right)
            {

                goRight = true;

            }
            if (e.KeyCode == Keys.Space && jumping == false)
            {

                jumping = true;

            }


        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)

            {

                
                goLeft = false;
            }

            if (e.KeyCode == Keys.Right)

            {

                goRight = false;
            }

            if (e.KeyCode == Keys.Space)
            {
                jumping = false;
            }
        }

        private void background_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox18_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox19_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox42_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox41_Click(object sender, EventArgs e)
        {

        }

        private void CloseGame(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void RestartGame()
        {
         
            Form1 newWindow = new Form1();

            newWindow.Show();

            
            this.Hide();

        }

        private void MoveGameElements(string direction)

        {






            foreach (Control x in this.Controls)
            {


                if (x is PictureBox && (string)x.Tag == "platform" || x is PictureBox && (string)x.Tag == "coin" || x is PictureBox && (string)x.Tag == "key" || x is PictureBox && (string)x.Tag == "door")
                {


                    if (direction == "back")
                    {

                        x.Left -= backgroundSpeed;
                    }

                    if (direction == "forward")
                    {


                        x.Left += backgroundSpeed;

                    }




                }
            }



        }

    }
}
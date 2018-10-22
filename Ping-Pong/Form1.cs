using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ping_Pong
{


    public partial class Form1 : Form
    {

        #region settings
        bool goupforplayer1;//boolean to be used to detect player1 up position
        bool godownforplayer1; //boolean to be used to detect player1 down position
        bool goupforplayer2;//boolean to be used to detect player2 up position
        bool godownforplayer2; //boolean to be used to detect player2 down position
        bool? wasGoingUp;
        bool? wasGoingUpLastTickForPlayer1, wasGoingUpLastTickForPlayer2;
        int numberOfTicksGoingInTheSameDirectionForPlayer1, numberOfTicksGoingInTheSameDirectionForPlayer2;
        const int movementSpeed = 5;
        Ball ball;
        PictureBox aPlayer;
        Label scoreLabel;
        public int score;

        #endregion

        Player player1, player2;

        public Form1()
        {
            InitializeComponent();
            player1 = new Player(pad1, Player1Score);
            player2 = new Player(pad2, Player2Score);
            ball = new Ball(aBall);
        }

        private void Keyisdown(object sender, KeyEventArgs e)
        {
            CheckKeys(e, true);
        }

        private void Keyisup(object sender, KeyEventArgs e)
        {
            CheckKeys(e, false);
        }

        private void CheckKeys(KeyEventArgs e, bool isDown)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    goupforplayer2 = isDown;
                    break;
                case Keys.Down:
                    godownforplayer2 = isDown;
                    break;

                case Keys.O:
                case Keys.W:
                    goupforplayer1 = isDown;
                    break;
                case Keys.Oemcomma:
                case Keys.S:
                    godownforplayer1 = isDown;
                    break;
            }
        }



        private void timerTick(object sender, EventArgs e)
        {
            ProcessMove(pad1, goupforplayer1, godownforplayer1, 
                ref wasGoingUpLastTickForPlayer1, ref numberOfTicksGoingInTheSameDirectionForPlayer1);
            ProcessMove(pad2, goupforplayer2, godownforplayer2, 
                ref wasGoingUpLastTickForPlayer2, ref numberOfTicksGoingInTheSameDirectionForPlayer2);
            ball.ProcessMove();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ProcessMove(PictureBox aPlayer, bool goupforplayer, bool godownforplayer, 
            ref bool? wasgoingUp, ref int numberOfTicksGoingInTheSameDirection)
        {
            bool? goingUp = null;

            if (goupforplayer)
            {
                goingUp = true;
            }
            if (godownforplayer)
            {
                if (goingUp.HasValue)
                {
                    goingUp = null;
                }
                else
                {
                    goingUp = false;
                }

            }

            if (wasgoingUp.HasValue)
            {
                if (!goingUp.HasValue)
                {
                    wasGoingUp = null;
                    numberOfTicksGoingInTheSameDirection = 0;
                } else if (wasGoingUp.Value == goingUp.Value)
                {
                    numberOfTicksGoingInTheSameDirection++;
                } else
                {
                    wasGoingUp = goingUp;
                    numberOfTicksGoingInTheSameDirection = 1;
                }
            } else if (goingUp.HasValue)
            {
                wasGoingUp = goingUp;
                numberOfTicksGoingInTheSameDirection = 1;
            }

            DoMove(aPlayer, goingUp, numberOfTicksGoingInTheSameDirection);
        }

        private void DoMove(PictureBox aPlayer, bool? goingUp, int numberOfTicksGoingInTheSameDirection)
        {
            if (goingUp.HasValue)
            {
                var speed = movementSpeed * (numberOfTicksGoingInTheSameDirection * 3);
                if (goingUp.Value)
                {
                    speed *= -1;
                }
                aPlayer.Location = new Point(aPlayer.Location.X,
                                    Math.Max(PongWorldInfo.topofWorld,
                                    Math.Min(PongWorldInfo.bottomofWorld - aPlayer.Height, 
                                    aPlayer.Location.Y + speed)));
            }
        }
    }
}


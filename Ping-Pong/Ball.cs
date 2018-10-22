using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;


namespace Ping_Pong
{
    
    public class Ball
    {
        private PictureBox aBall;
        Player leftsidePlayer, rightsidePlayer, score;
        int xSpeed, ySpeed;


        public Ball(PictureBox aBall, Player leftsidePlayer, Player rightsidePlayer)
        {
            this.aBall = aBall;
            this.leftsidePlayer = leftsidePlayer;
            this.rightsidePlayer = rightsidePlayer;
            xSpeed = 1;
            ySpeed = 2;
        }

        internal void ProcessMove()
        {
            var bottom = PongWorldInfo.bottomofWorld - aBall.Height;
            aBall.Location = new Point(aBall.Location.X + xSpeed, 
                Math.Max(PongWorldInfo.topofWorld, 
                Math.Min(bottom,
                aBall.Location.Y + ySpeed
                )));
            if (aBall.Location.Y == bottom || aBall.Location.Y == PongWorldInfo.topofWorld)
            {
                ySpeed *= -1;
            }
            if (aBall.Location.X == PongWorldInfo.leftOfWorld)
            {
                Score(leftsidePlayer);
            } else if (aBall.Location.X == PongWorldInfo.rightOfWorld - aBall.Width)
            {
                Score(rightsidePlayer);
            }
        }

        private void Score(Player winningPlayer)
        {
            winningPlayer.score++;
            aBall.Location = new Point((PongWorldInfo.leftOfWorld + PongWorldInfo.rightOfWorld) / 2,
            (PongWorldInfo.topofWorld +PongWorldInfo.bottomofWorld) / 2);
        }
    }
}

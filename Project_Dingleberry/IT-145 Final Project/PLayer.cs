using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Project_Dingleberry
{
    public class Player : Entity
    {
        public bool IsMovingUp, IsMovingDown, IsMovingLeft, IsMovingRight;

        private int speed = 8;
        private int lives = 3;
        private DateTime lastHit = DateTime.MinValue;

        // Public property to let GameController check how many lives we have
        public int Lives => lives;

        public Player(string fileName) : base(fileName)
        {
            this.posX = 350;
            this.posY = 450;
        }

        public void ProcessMovement()
        {
            double moveX = 0;
            double moveY = 0;

            if (IsMovingUp) moveY -= 1;
            if (IsMovingDown) moveY += 1;
            if (IsMovingLeft) moveX -= 1;
            if (IsMovingRight) moveX += 1;

            if (moveX != 0 && moveY != 0)
            {
                moveX *= 0.707;
                moveY *= 0.707;
            }

            posX += (int)(moveX * speed);
            posY += (int)(moveY * speed);
        }

        public void clampToScreen(int width, int height)
        {
            if (posX < 0) posX = 0;
            if (posY < 0) posY = 0;
            if (posX > width - 40) posX = width - 40;
            if (posY > height - 60) posY = height - 60;
        }

        public bool playerHit()
        {
            if ((DateTime.Now - lastHit).TotalSeconds >= 1)
            {
                lastHit = DateTime.Now;
                lives -= 1;
                return true;
            }
            return false;
        }
    }
}
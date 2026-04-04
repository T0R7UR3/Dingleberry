using System;
using System.Diagnostics;
using System.Drawing;

namespace Project_Dingleberry
{
    public class Player : Entity
    {
        public bool IsMovingUp, IsMovingDown, IsMovingLeft, IsMovingRight;

        private int speed = 8;
        private int lives = 3;
        private DateTime lastHit = DateTime.MinValue;

        public int Lives => lives;
        public bool IsInvincible => (DateTime.Now - lastHit).TotalSeconds < 1.5;

        public Player(string fileName) : base(fileName, Color.Blue)
        {
            this.posX = 640;
            this.posY = 360;
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

        public void ClampToScreen(int width, int height)
        {
            if (posX < 0) posX = 0;
            if (posY < 40) posY = 40;
            if (posX > width - 32) posX = width - 32;
            if (posY > height - 32) posY = height - 32;
        }

        public bool PlayerHit()
        {
            if (!IsInvincible)
            {
                lastHit = DateTime.Now;
                lives -= 1;
                return true;
            }
            return false;
        }

        public void AddLife()
        {
            lives += 1;
        }

        public override void DrawEntity(Graphics g)
        {
            if (IsInvincible)
            {
                if ((DateTime.Now.Millisecond / 100) % 2 == 0) return;
            }
            base.DrawEntity(g);
        }
    }
}
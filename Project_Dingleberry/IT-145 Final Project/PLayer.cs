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

        // NEW: A public property to easily check if the player is currently invincible
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

        public void clampToScreen(int width, int height)
        {
            if (posX < 0) posX = 0;
            if (posY < 0) posY = 0;
            if (posX > width - 40) posX = width - 40;
            if (posY > height - 60) posY = height - 60;
        }

        public bool playerHit()
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

        // NEW: Override the draw method to add the flicker effect!
        public override void drawEntity(Graphics g)
        {
            if (IsInvincible)
            {
                // Divide the current millisecond by 100 to get a pulsing 0-1-0-1 pattern.
                // If it's an even number, we skip drawing the player this frame!
                if ((DateTime.Now.Millisecond / 100) % 2 == 0)
                {
                    return;
                }
            }

            // If we aren't invincible (or if it's the "visible" part of the blink), draw normally
            base.drawEntity(g);
        }
    }
}
using System;
using System.Drawing;

namespace Project_Dingleberry
{
    public class Player : Entity
    {
        public const int MaxLives = 5;
        public const int HUDWall = 40;
        public const int PlayerSpeed = 8;
        public const double InvincibilitySeconds = 1.5;
        public const double DiagonalMultiplier = 0.707;

        public bool IsMovingUp, IsMovingDown, IsMovingLeft, IsMovingRight;

        private int lives = 3;
        private DateTime lastHit = DateTime.MinValue;

        public int Lives => lives;
        public bool IsInvincible => (DateTime.Now - lastHit).TotalSeconds < InvincibilitySeconds;

        public Player(string fileName) : base(fileName, Color.Blue)
        {
            // NEW: Player is now 60x60
            width = 60;
            height = 60;
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
                moveX *= DiagonalMultiplier;
                moveY *= DiagonalMultiplier;
            }

            posX += (int)(moveX * PlayerSpeed);
            posY += (int)(moveY * PlayerSpeed);
        }

        public void ClampToScreen(int screenWidth, int screenHeight)
        {
            if (posX < 0) posX = 0;
            if (posY < HUDWall) posY = HUDWall;

            // NEW: Uses dynamic width/height so the 60px player doesn't clip off screen
            if (posX > screenWidth - width) posX = screenWidth - width;
            if (posY > screenHeight - height) posY = screenHeight - height;
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
            if (lives < MaxLives)
            {
                lives += 1;
            }
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
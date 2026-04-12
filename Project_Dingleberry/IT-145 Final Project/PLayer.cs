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

        public const int DashSpeedMultiplier = 3;
        public const double DashDurationSeconds = 0.3;
        public const double DashCooldownSeconds = 3.0;

        public bool IsMovingUp, IsMovingDown, IsMovingLeft, IsMovingRight;

        private int lives = 3;
        private DateTime lastHit = DateTime.MinValue;
        private DateTime dashStartTime = DateTime.MinValue;
        private DateTime lastDashTime = DateTime.MinValue;

        public int Lives => lives;

        public bool IsHitInvincible =>
            (DateTime.Now - lastHit).TotalSeconds < InvincibilitySeconds;

        public bool IsDashing =>
            (DateTime.Now - dashStartTime).TotalSeconds < DashDurationSeconds;

        public bool IsInvincible => IsHitInvincible || IsDashing;

        public bool DashReady =>
            (DateTime.Now - lastDashTime).TotalSeconds >= DashCooldownSeconds;

        public double DashCooldownRemaining
        {
            get
            {
                double remaining = DashCooldownSeconds - (DateTime.Now - lastDashTime).TotalSeconds;
                return remaining > 0 ? remaining : 0;
            }
        }

        public double DashChargePercent
        {
            get
            {
                double elapsed = (DateTime.Now - lastDashTime).TotalSeconds;
                double percent = elapsed / DashCooldownSeconds;

                if (percent < 0) return 0;
                if (percent > 1) return 1;

                return percent;
            }
        }

        public Player(string fileName) : base(fileName, Color.Blue)
        {
            width = 60;
            height = 60;
        }

        public override Rectangle Hitbox
        {
            get
            {
                return new Rectangle(
                    posX + 20,
                    posY + 8,
                    20,
                    44
                );
            }
        }

        public void TryDash()
        {
            bool isTryingToMove = IsMovingUp || IsMovingDown || IsMovingLeft || IsMovingRight;

            if (!isTryingToMove)
            {
                return;
            }

            if (!DashReady)
            {
                return;
            }

            dashStartTime = DateTime.Now;
            lastDashTime = dashStartTime;
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

            int currentSpeed = IsDashing
                ? PlayerSpeed * DashSpeedMultiplier
                : PlayerSpeed;

            posX += (int)(moveX * currentSpeed);
            posY += (int)(moveY * currentSpeed);
        }

        public void ClampToScreen(int screenWidth, int screenHeight)
        {
            if (posX < 0) posX = 0;
            if (posY < HUDWall) posY = HUDWall;

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
            if (IsHitInvincible)
            {
                if ((DateTime.Now.Millisecond / 100) % 2 == 0)
                {
                    return;
                }
            }

            base.DrawEntity(g);
        }
    }
}
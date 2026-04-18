// Project: John Stick
// Course: IT145 Foundations of Application Development
// Authors: Murdock MacAskill, Beth, and Landen
// File: Player.cs
// Purpose: Defines the player character, movement, dash, lives, and invincibility behavior.
// Date: 04/17/2026

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

        // FIX: Now we store the game time internally, updated every frame by the GameController
        private double currentGameTime = 0;
        private double lastHitTime = -999;
        private double dashStartTime = -999;
        private double lastDashTime = -999;

        public int Lives => lives;

        public bool IsHitInvincible =>
            (currentGameTime - lastHitTime) < InvincibilitySeconds;

        public bool IsDashing =>
            (currentGameTime - dashStartTime) < DashDurationSeconds;

        public bool IsInvincible => IsHitInvincible || IsDashing;

        public bool DashReady =>
            (currentGameTime - lastDashTime) >= DashCooldownSeconds;

        public double DashCooldownRemaining
        {
            get
            {
                double remaining = DashCooldownSeconds - (currentGameTime - lastDashTime);
                return remaining > 0 ? remaining : 0;
            }
        }

        public double DashChargePercent
        {
            get
            {
                double elapsed = currentGameTime - lastDashTime;
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

        // FIX: Called by GameController every frame so the player knows what time it is
        public void UpdateTime(double gameTime)
        {
            currentGameTime = gameTime;
        }

        public void TryDash()
        {
            bool isTryingToMove = IsMovingUp || IsMovingDown || IsMovingLeft || IsMovingRight;

            if (!isTryingToMove) return;
            if (!DashReady) return;

            dashStartTime = currentGameTime;
            lastDashTime = currentGameTime;
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
                lastHitTime = currentGameTime;
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
                // We can keep DateTime.Now here because this is strictly for the visual 
                // blinking effect, it doesn't affect gameplay logic!
                if ((DateTime.Now.Millisecond / 100) % 2 == 0)
                {
                    return;
                }
            }

            base.DrawEntity(g);
        }
    }
}
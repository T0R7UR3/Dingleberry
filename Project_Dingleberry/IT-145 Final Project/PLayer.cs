using System;
using System.Diagnostics;
using System.Drawing;

namespace Project_Dingleberry
{ 
    // Player class inherits from Entity.cs to use posX, posY, and setImage()
    public class Player : Entity
    {
        // Input tracking booleans. These are toggled by OnKeyDown/OnKeyUp in GameStage.cs
        public bool IsMovingUp, IsMovingDown, IsMovingLeft, IsMovingRight;

        // Determines how many pixels the player moves per frame (16ms)
        private int speed = 8;

        // The number of times the player can be hit before dying
        private int lives = 3;

        //
        private DateTime lastHit = DateTime.Now;

        public Player(string fileName) : base(fileName)
        {
            // Sets the initial spawning coordinates in the Battle Zone
            this.posX = 350;
            this.posY = 450;
        }
        
        public void ProcessMovement()
        {
            double moveX = 0;
            double moveY = 0;

            // 1. Check which directions are currently active
            if (IsMovingUp) moveY -= 1;
            if (IsMovingDown) moveY += 1;
            if (IsMovingLeft) moveX -= 1;
            if (IsMovingRight) moveX += 1;

            // 2. Diagonal Normalization
            // If moving in two directions (e.g., Up and Right), we multiply by 0.707
            // This prevents the player from moving faster diagonally (Pythagorean Theorem fix)
            if (moveX != 0 && moveY != 0)
            {
                moveX *= 0.707;
                moveY *= 0.707;
            }

            // 3. Apply the calculated movement to the Entity's base positions
            posX += (int)(moveX * speed);
            posY += (int)(moveY * speed);
        }


        public void clampToScreen(int width, int height)
        {
            // Prevent moving off the left or top edges
            if (posX < 0) posX = 0;
            if (posY < 0) posY = 0;

            // Prevent moving off the right or bottom edges (accounting for approx. sprite size)
            if (posX > width - 40) posX = width - 40;
            if (posY > height - 60) posY = height - 60;
        }

        public void playerHit()
        {
            if ((DateTime.Now - lastHit).TotalSeconds >= 2)
            {
                lastHit = DateTime.Now;
                lives -= 1;

                Debug.WriteLine($"Player was hit. {lives} lives remaining.");

                if (lives <= 0)
                {
                    // Game over.
                    Debug.WriteLine("Player died.");
                }
            }
        }
    }
}
// Program: Project Dingleberry Player Class
// Author: Murdock MacAskill
// Date: 03/24/2026
//
// Purpose
// Represents the player
// Inherits from Entity and adds movement

using System.Windows.Forms;
using System.Drawing;

namespace RenderingTest
{
    internal class Player : Entity
    {
        private int speed = 5;

        public Player(Form1 screen) : base(screen)
        {
            // Override the default image for player
            entityImage.Image = Image.FromFile(@"Images\\Player.png");

            // Optional: make player bigger
            entityImage.Size = new Size(30, 30);
        }

        // Movement methods
        public void moveUp()
        {
            posY -= speed;
        }

        public void moveDown()
        {
            posY += speed;
        }

        public void moveLeft()
        {
            posX -= speed;
        }

        public void moveRight()
        {
            posX += speed;
        }

        // Optional: clamp to screen (fixes your left side issue 👀)
        public void clampToScreen(int width, int height)
        {
            if (posX < 0) posX = 0;
            if (posY < 0) posY = 0;

            if (posX > width - entityImage.Width)
                posX = width - entityImage.Width;

            if (posY > height - entityImage.Height)
                posY = height - entityImage.Height;
        }
    }
}
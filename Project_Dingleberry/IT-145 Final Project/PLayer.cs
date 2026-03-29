using System.Drawing;

namespace Project_Dingleberry
{
    internal class Player : Entity
    {
        private int speed = 5;

        public Player(string fileName) : base(fileName) { }

        public void moveUp() { posY -= speed; }
        public void moveDown() { posY += speed; }
        public void moveLeft() { posX -= speed; }
        public void moveRight() { posX += speed; }

        public void clampToScreen(int width, int height)
        {
            if (entityImage == null) return;
            if (posX < 0) posX = 0;
            if (posY < 0) posY = 0;
            if (posX > width - entityImage.Width) posX = width - entityImage.Width;
            if (posY > height - entityImage.Height) posY = height - entityImage.Height;
        }
    }
}
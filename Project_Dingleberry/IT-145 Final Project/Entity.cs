using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace Project_Dingleberry
{
    internal class Entity
    {
        protected int posX;
        protected int posY;
        protected Image? entityImage;

        public Entity(string fileName)
        {
            setImage(fileName);
            posX = 100;
            posY = 100;
        }

        public virtual void drawEntity(Graphics g)
        {
            if (entityImage != null) g.DrawImage(entityImage, posX, posY);
        }

        // Use this for collision: if (player.Hitbox.IntersectsWith(enemy.Hitbox))
        public Rectangle Hitbox => new Rectangle(posX, posY, entityImage?.Width ?? 0, entityImage?.Height ?? 0);

        public void setPos(int x, int y)
        {
            posX = x;
            posY = y;
        }

        public void setImage(string fileName)
        {
            try
            {
                string path = Path.Combine(Application.StartupPath, "Images", fileName);
                entityImage = Image.FromFile(path);
            }
            catch
            {
                // Creates a fallback placeholder if the image is missing
                entityImage = new Bitmap(30, 30);
            }
        }
    }
}
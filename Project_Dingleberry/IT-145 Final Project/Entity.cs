using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace Project_Dingleberry
{
    public class Entity
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

        // Trying this for collision: if (player.Hitbox.IntersectsWith(enemy.Hitbox))
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
                // Create a bright red 30x30 square if the image is missing
                Bitmap tempImg = new Bitmap(30, 30);
                using (Graphics g = Graphics.FromImage(tempImg))
                {
                    g.Clear(Color.Red);
                }
                entityImage = tempImg;
            }
        }
    }
}
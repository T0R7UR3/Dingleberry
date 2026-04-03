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
        protected Color fallbackColor; // NEW: Stores the placeholder color

        // NEW: Constructor now asks for a color
        public Entity(string fileName, Color fallback)
        {
            fallbackColor = fallback;
            setImage(fileName);
            posX = 100;
            posY = 100;
        }

        public virtual void drawEntity(Graphics g)
        {
            if (entityImage != null) g.DrawImage(entityImage, posX, posY);
        }

        public Rectangle Hitbox => new Rectangle(posX, posY, entityImage?.Width ?? 30, entityImage?.Height ?? 30);

        public void setPos(int x, int y)
        {
            posX = x;
            posY = y;
        }

        public int GetX() => posX;
        public int GetY() => posY;

        public void setImage(string fileName)
        {
            try
            {
                string path = Path.Combine(Application.StartupPath, "Images", fileName);
                entityImage = Image.FromFile(path);
            }
            catch
            {
                // Draws the custom color instead of always being red!
                Bitmap tempImg = new Bitmap(30, 30);
                using (Graphics g = Graphics.FromImage(tempImg))
                {
                    g.Clear(fallbackColor);
                }
                entityImage = tempImg;
            }
        }
    }
}
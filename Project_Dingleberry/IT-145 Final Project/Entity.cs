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
        protected Color fallbackColor;

        protected int width = 32;
        protected int height = 32;

        public Entity(string fileName, Color fallback)
        {
            fallbackColor = fallback;
            SetImage(fileName);
            posX = 100;
            posY = 100;
        }

        public virtual void DrawEntity(Graphics g)
        {
            if (entityImage != null)
            {
                g.DrawImage(entityImage, posX, posY, width, height);
            }
        }

        public virtual Rectangle Hitbox
        {
            get { return new Rectangle(posX, posY, width, height); }
        }

        public void SetPos(int x, int y)
        {
            posX = x;
            posY = y;
        }

        public int GetX() => posX;
        public int GetY() => posY;

        public void SetImage(string fileName)
        {
            try
            {
                string path = Path.Combine(Application.StartupPath, "Assets", "Images", fileName);
                entityImage = Image.FromFile(path);
            }
            catch
            {
                Bitmap tempImg = new Bitmap(32, 32);
                using (Graphics g = Graphics.FromImage(tempImg))
                {
                    g.Clear(fallbackColor);
                }

                entityImage = tempImg;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace RenderingTest
{
    internal class Entity
    {
        protected int posX;
        protected int posY;
        protected int velocityX;
        protected int velocityY;
        protected Image entityImage;
        public Entity(Image img) {
            entityImage = img;
            posX = 100;
            posY = 100;
        }

        public void drawEntity(Graphics g)
        {
            g.DrawImage(entityImage, posX, posY);
        }

        public void moveEntity(Form1 screen)
        {
            posX = Math.Max(0, Math.Min(posX + velocityX, screen.Width - entityImage.Width));
            posY = Math.Max(0, Math.Min(posY + velocityY, screen.Height - entityImage.Height));
        }

        public void setPos(int x, int y)
        {
            if (x >= 0 && y >= 0)
            {
                posX = x;
                posY = y;
            }
            else
            {
                MessageBox.Show("Error: Position can't be negative.");
            }
        }

        public void setImage(string dir)
        {
            try
            {
                entityImage = Image.FromFile($"Images\\{dir}");
            }
            catch
            {
                MessageBox.Show("Image file does not exist.");
            }
        }

        public void addVelocity(int velX, int velY)
        {
            velocityX += velX;
            velocityY += velY;
        }
        public int getDistance(Entity otherEntity)
        {
            int x = posX - otherEntity.posX;
            int y = posY - otherEntity.posY;
            return (int) Math.Abs(Math.Sqrt(x * x + y * y));
        }
    }
}

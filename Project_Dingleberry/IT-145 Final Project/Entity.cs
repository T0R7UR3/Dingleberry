// Project: John Stick
// Course: IT145 Foundations of Application Development
// Authors: Murdock MacAskill, Beth, and Landen
// File: Entity.cs
// Purpose: Provides shared position, image, drawing, and hitbox behavior for game objects.
// Date: 04/17/2026

using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;

namespace Project_Dingleberry
{
    public class Entity
    {
        // NEW: This dictionary caches the images so we don't load from the hard drive 100 times!
        private static Dictionary<string, Image> ImageCache = new Dictionary<string, Image>();

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
            // NEW: Check if we already loaded this image!
            if (ImageCache.ContainsKey(fileName))
            {
                entityImage = ImageCache[fileName];
                return;
            }

            try
            {
                string path = Path.Combine(Application.StartupPath, "Assets", "Images", fileName);

                // Load it, but save a copy in the cache for next time
                Image loadedImg = Image.FromFile(path);
                ImageCache[fileName] = loadedImg;
                entityImage = loadedImg;
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
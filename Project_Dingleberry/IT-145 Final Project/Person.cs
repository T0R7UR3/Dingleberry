// Program: Project Dingleberry Player Class
// Author: Murdock MacAskill
// Date: 03/20/2026
//
// Purpose
// This class represents the player
// Right now it only stores position and draws a simple circle

using System.Drawing;

namespace ProjectDingleberry
{
    public class Player
    {
        // Player x and y position
        public int X;
        public int Y;

        // Size of the player circle
        public int Size;

        public Player(int startX, int startY)
        {
            X = startX;
            Y = startY;
            Size = 30;
        }

        public void Draw(Graphics g)
        {
            // Draw a circle at the player position
            // We subtract half the size so X and Y feel like the center
            int left = X - (Size / 2);
            int top = Y - (Size / 2);

            g.FillEllipse(Brushes.Black, left, top, Size, Size);
            g.DrawEllipse(Pens.White, left, top, Size, Size);
        }
    }
}
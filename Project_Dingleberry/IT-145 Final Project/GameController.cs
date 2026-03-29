using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Project_Dingleberry
{
    internal class GameController
    {
        private John_Stick gameForm;
        private Player player;
        private List<Entity> enemies;

        public GameController(John_Stick form)
        {
            gameForm = form;
            enemies = new List<Entity>();

            // Initialize Player with the image filename
            // Make sure "Player.png" is in your bin/Debug/Images folder!
            player = new Player("Player.png");
            player.setPos(200, 200);

            // Add a test enemy
            Entity testEnemy = new Entity("Enemy.png");
            testEnemy.setPos(400, 100);
            enemies.Add(testEnemy);
        }

        // FIX: This is the method John_Stick.cs was looking for!
        public void HandleKeyPress(Keys key)
        {
            // Supports both WASD and Arrow Keys
            if (key == Keys.W || key == Keys.Up) player.moveUp();
            if (key == Keys.S || key == Keys.Down) player.moveDown();
            if (key == Keys.A || key == Keys.Left) player.moveLeft();
            if (key == Keys.D || key == Keys.Right) player.moveRight();
        }

        public void Update()
        {
            // 1. Handle Movement Logic & Screen Boundaries
            player.clampToScreen(gameForm.ClientSize.Width, gameForm.ClientSize.Height);

            // 2. Handle Collisions (Example)
            foreach (var enemy in enemies)
            {
                if (player.Hitbox.IntersectsWith(enemy.Hitbox))
                {
                    // Logic for when player touches enemy (e.g., take damage)
                }
            }

            // 3. Refresh the screen (calls OnPaint in John_Stick.cs)
            gameForm.Invalidate();
        }

        public void Draw(Graphics g)
        {
            // Draw Player
            player.drawEntity(g);

            // Draw all Enemies
            foreach (var enemy in enemies)
            {
                enemy.drawEntity(g);
            }
        }
    }
}
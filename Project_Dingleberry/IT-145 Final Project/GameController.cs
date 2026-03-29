using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Project_Dingleberry
{
    internal class GameController
    {
        private Form gameForm;
        private Player player;
        private List<Entity> enemies;

        public GameController(Form form)
        {
            gameForm = form;
            enemies = new List<Entity>();

            player = new Player("Player.png");
            player.setPos(350, 450);

            Entity testEnemy = new Entity("Enemy.png");
            testEnemy.setPos(350, 100);
            enemies.Add(testEnemy);
        }

        // Helper to let GameStage talk to the player's bools
        public Player GetPlayer() => player;

        public void Update()
        {
            // Move based on held keys
            player.ProcessMovement();

            // Keep on screen
            player.clampToScreen(gameForm.ClientSize.Width, gameForm.ClientSize.Height);

            // Redraw
            gameForm.Invalidate();
        }

        public void Draw(Graphics g)
        {
            player.drawEntity(g);
            foreach (var enemy in enemies)
            {
                enemy.drawEntity(g);
            }
        }
    }
}
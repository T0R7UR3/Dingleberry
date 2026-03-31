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
        private List<Enemy> enemies;

                public GameController(Form form)
        {
            gameForm = form;
            enemies = new List<Enemy>();

            player = new Player("Player.png");
            player.setPos(350, 450);

            // Spawn a Chaser
            Enemy chaser = new Enemy("Enemy.png", EnemyType.Chaser);
            chaser.setPos(100, 100);
            enemies.Add(chaser);

            // Spawn a Bouncer
            Enemy bouncer = new Enemy("Enemy.png", EnemyType.Bouncer);
            bouncer.setPos(600, 100);
            enemies.Add(bouncer);

            // Spawn a Drifter
            Enemy drifter = new Enemy("Enemy.png", EnemyType.Drifter);
            drifter.setPos(350, 200);
            enemies.Add(drifter);
        }
  
        // Helper to let GameStage talk to the player's bools
        public Player GetPlayer() => player;

        public void Update()
        {
            // Move based on held keys
            player.ProcessMovement();

            // Keep on screen
            player.clampToScreen(gameForm.ClientSize.Width, gameForm.ClientSize.Height);

            // NEW: Update all enemies
            foreach (var enemy in enemies)
            {
                enemy.Update(player, gameForm.ClientSize.Width, gameForm.ClientSize.Height);
            }

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
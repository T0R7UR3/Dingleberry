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

        // added code here - BDD

        private int maxEnemies = 10;        //can update with more or less enemies -BDD
        private int spawnTimer = 0;         
        private int spawnInterval = 100;    
        private Random rand = new Random(); 

        //code added end here - BDD

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
        //code added here -BDD

        private void SpawnRandomEnemy()
        {
            Array enemyTypes = Enum.GetValues(typeof(EnemyType));
            EnemyType randomType = (EnemyType)enemyTypes.GetValue(rand.Next(enemyTypes.Length));

            Enemy newEnemy = new Enemy("Enemy.png", randomType);

            int spawnX = rand.Next(0, gameForm.ClientSize.Width - 50);
            int spawnY = rand.Next(0, gameForm.ClientSize.Height - 50);

            newEnemy.setPos(spawnX, spawnY);

            enemies.Add(newEnemy);
        }

        //code added end here -BDD

        // Helper to let GameStage talk to the player's bools
        public Player GetPlayer() => player;

        public void Update()
        {
            // Move based on held keys
            player.ProcessMovement();

            // Keep on screen
            player.clampToScreen(gameForm.ClientSize.Width, gameForm.ClientSize.Height);

            //code added here -BDD
            spawnTimer++; // Increase the timer every frame

            // If enough time has passed AND we are under the max cap
            if (spawnTimer >= spawnInterval && enemies.Count < maxEnemies)
            {
                SpawnRandomEnemy();
                spawnTimer = 0; // Reset the timer after spawning
            }

            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                enemies[i].Update(player, gameForm.ClientSize.Width, gameForm.ClientSize.Height);

                // Collision check (Player touches enemy)
                if (player.Hitbox.IntersectsWith(enemies[i].Hitbox))
                {
                    enemies.RemoveAt(i);
                }
            }
            //code added end here -BDD


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
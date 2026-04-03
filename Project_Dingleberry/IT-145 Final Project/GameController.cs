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

        private int maxEnemies = 10;
        private int spawnTimer = 0;
        private int spawnInterval = 100;
        private Random rand = new Random();

        // The emergency stop switch to prevent ghost timer pop-ups
        private bool isGameOver = false;

        public GameController(Form form)
        {
            gameForm = form;
            enemies = new List<Enemy>();

            player = new Player("Player.png");
            player.setPos(350, 450);

            Enemy chaser = new Enemy("Enemy.png", EnemyType.Chaser);
            chaser.setPos(100, 100);
            enemies.Add(chaser);

            Enemy bouncer = new Enemy("Enemy.png", EnemyType.Bouncer);
            bouncer.setPos(600, 100);
            enemies.Add(bouncer);

            Enemy drifter = new Enemy("Enemy.png", EnemyType.Drifter);
            drifter.setPos(350, 200);
            enemies.Add(drifter);
        }

        private void SpawnRandomEnemy()
        {
            // Check if a Chaser already exists in the list
            bool hasChaser = false;
            foreach (var enemy in enemies)
            {
                if (enemy.Type == EnemyType.Chaser)
                {
                    hasChaser = true;
                    break;
                }
            }

            // Build a list of allowed types for this specific spawn
            List<EnemyType> allowedTypes = new List<EnemyType> { EnemyType.Bouncer, EnemyType.Drifter };

            // Only add Chaser to the pool if one doesn't exist yet
            if (!hasChaser)
            {
                allowedTypes.Add(EnemyType.Chaser);
            }

            // Pick a random type from our safe list
            EnemyType randomType = allowedTypes[rand.Next(allowedTypes.Count)];
            Enemy newEnemy = new Enemy("Enemy.png", randomType);

            int spawnX = 0;
            int spawnY = 0;
            int safeDistance = 200;
            bool safeSpotFound = false;

            while (!safeSpotFound)
            {
                spawnX = rand.Next(0, gameForm.ClientSize.Width - 50);
                spawnY = rand.Next(0, gameForm.ClientSize.Height - 50);

                double diffX = spawnX - player.GetX();
                double diffY = spawnY - player.GetY();
                double distance = Math.Sqrt(Math.Pow(diffX, 2) + Math.Pow(diffY, 2));

                if (distance >= safeDistance)
                {
                    safeSpotFound = true;
                }
            }

            newEnemy.setPos(spawnX, spawnY);
            enemies.Add(newEnemy);
        }

        public Player GetPlayer() => player;

        public void Update()
        {
            // If the game is over, ignore all leftover timer ticks
            if (isGameOver) return;

            player.ProcessMovement();
            player.clampToScreen(gameForm.ClientSize.Width, gameForm.ClientSize.Height);

            spawnTimer++;
            if (spawnTimer >= spawnInterval && enemies.Count < maxEnemies)
            {
                SpawnRandomEnemy();
                spawnTimer = 0;
            }

            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                enemies[i].Update(player, gameForm.ClientSize.Width, gameForm.ClientSize.Height);

                if (player.Hitbox.IntersectsWith(enemies[i].Hitbox))
                {
                    bool tookDamage = player.playerHit();

                    if (tookDamage)
                    {
                        enemies.RemoveAt(i);

                        // Check for Game Over
                        if (player.Lives <= 0)
                        {
                            // Flip the switch so no more updates happen
                            isGameOver = true;

                            MessageBox.Show("You ran out of lives!", "Game Over");

                            // Close this specific game window to return to Main Menu
                            gameForm.Close();
                            return;
                        }
                    }
                }
            }

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
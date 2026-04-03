using System;
using System.Collections.Generic;
using System.Diagnostics; // NEW: Required for the Stopwatch!
using System.Drawing;
using System.Windows.Forms;

namespace Project_Dingleberry
{
    internal class GameController
    {
        private Form gameForm;
        private Player player;
        private List<Enemy> enemies;

        private int maxEnemies = 20;
        private int spawnTimer = 0;
        private int spawnInterval = 100;
        private Random rand = new Random();

        private bool isGameOver = false;

        // NEW: The survival timer to track the player's score
        private Stopwatch survivalTimer;

        public GameController(Form form)
        {
            gameForm = form;
            enemies = new List<Enemy>();

            player = new Player("Player.png");
            player.setPos(640, 360);

            // Start the clock as soon as the game begins!
            survivalTimer = new Stopwatch();
            survivalTimer.Start();

            Enemy chaser = new Enemy("Enemy.png", EnemyType.Chaser);
            chaser.setPos(100, 100);
            enemies.Add(chaser);

            Enemy bouncer = new Enemy("Enemy.png", EnemyType.Bouncer);
            bouncer.setPos(1000, 100);
            enemies.Add(bouncer);

            Enemy drifter = new Enemy("Enemy.png", EnemyType.Drifter);
            drifter.setPos(640, 100);
            enemies.Add(drifter);
        }

        private void SpawnRandomEnemy()
        {
            bool hasChaser = false;
            foreach (var enemy in enemies)
            {
                if (enemy.Type == EnemyType.Chaser)
                {
                    hasChaser = true;
                    break;
                }
            }

            List<EnemyType> allowedTypes = new List<EnemyType> { EnemyType.Bouncer, EnemyType.Drifter };
            if (!hasChaser) allowedTypes.Add(EnemyType.Chaser);

            EnemyType randomType = allowedTypes[rand.Next(allowedTypes.Count)];
            Enemy newEnemy = new Enemy("Enemy.png", randomType);

            int spawnX = 0;
            int spawnY = 0;
            int safeDistance = 300;
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

                        if (player.Lives <= 0)
                        {
                            isGameOver = true;

                            // NEW: Stop the clock!
                            survivalTimer.Stop();

                            // Format the final time to look like "01:23"
                            string finalTime = survivalTimer.Elapsed.ToString(@"mm\:ss");

                            // Show the time in the Game Over box
                            MessageBox.Show($"You ran out of lives!\n\nSurvival Time: {finalTime}", "Game Over");

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
            // 1. Draw the player
            player.drawEntity(g);

            // 2. Draw the enemy swarm
            foreach (var enemy in enemies)
            {
                enemy.drawEntity(g);
            }

            // 3. Draw the HUD
            using (Font hudFont = new Font("Arial", 16, FontStyle.Bold))
            {
                // Draw Lives in the top-left
                string livesText = $"Lives: {player.Lives}";
                g.DrawString(livesText, hudFont, Brushes.Black, new PointF(10, 10));

                // NEW: Draw Timer in the top-right
                // The formatting @"mm\:ss" turns raw milliseconds into clean Minutes:Seconds
                string timeText = $"Time: {survivalTimer.Elapsed.ToString(@"mm\:ss")}";

                // Measure how wide the text is so we can perfectly right-align it
                SizeF textSize = g.MeasureString(timeText, hudFont);
                g.DrawString(timeText, hudFont, Brushes.Black, new PointF(gameForm.ClientSize.Width - textSize.Width - 10, 10));
            }
        }
    }
}
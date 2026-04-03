using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private Stopwatch survivalTimer;
        private int difficultyLevel = 1;

        // NEW: Item management
        private List<Item> activeItems;
        private int itemSpawnTimer = 0;
        private int itemSpawnInterval = 300; // Spawns items roughly every 5 seconds

        public GameController(Form form)
        {
            gameForm = form;
            enemies = new List<Enemy>();
            activeItems = new List<Item>(); // Initialize item list

            player = new Player("Player.png");
            player.setPos(640, 360);

            survivalTimer = new Stopwatch();
            survivalTimer.Start();

            // Initial enemies
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

            // Difficulty escalation
            int currentSeconds = (int)survivalTimer.Elapsed.TotalSeconds;
            if (currentSeconds / 15 >= difficultyLevel)
            {
                difficultyLevel++;
                maxEnemies += 5;
                if (spawnInterval > 20) spawnInterval -= 10;
            }

            player.ProcessMovement();
            player.clampToScreen(gameForm.ClientSize.Width, gameForm.ClientSize.Height);

            // Enemy Spawning Logic
            spawnTimer++;
            if (spawnTimer >= spawnInterval && enemies.Count < maxEnemies)
            {
                SpawnRandomEnemy();
                spawnTimer = 0;
            }

            // ITEM SPAWNING LOGIC (Max 5 items on screen at once)
            itemSpawnTimer++;
            if (itemSpawnTimer >= itemSpawnInterval && activeItems.Count < 5)
            {
                Array itemTypes = Enum.GetValues(typeof(ItemType));
                ItemType randomItem = (ItemType)itemTypes.GetValue(rand.Next(itemTypes.Length));

                // Assign the correct placeholder colors and fake image names
                Color itemColor = Color.Green; // Default for Bomb
                string imgName = "Bomb.png";

                if (randomItem == ItemType.Life)
                {
                    itemColor = Color.Black;
                    imgName = "Life.png";
                }
                else if (randomItem == ItemType.Mine)
                {
                    itemColor = Color.Yellow;
                    imgName = "Mine.png";
                }

                Item newItem = new Item(randomItem, imgName, itemColor);
                newItem.setPos(rand.Next(50, gameForm.ClientSize.Width - 50), rand.Next(50, gameForm.ClientSize.Height - 50));
                activeItems.Add(newItem);

                itemSpawnTimer = 0;
            }

            // ITEM COLLISION LOGIC
            for (int i = activeItems.Count - 1; i >= 0; i--)
            {
                if (player.Hitbox.IntersectsWith(activeItems[i].Hitbox))
                {
                    ItemType type = activeItems[i].Type;
                    activeItems.RemoveAt(i); // Consume the item

                    if (type == ItemType.Bomb)
                    {
                        // Destroy half enemies
                        int enemiesToDestroy = enemies.Count / 2;
                        for (int j = 0; j < enemiesToDestroy; j++)
                        {
                            if (enemies.Count > 0) enemies.RemoveAt(0);
                        }
                    }
                    else if (type == ItemType.Life)
                    {
                        player.AddLife(); // Gain 1 HP
                    }
                    else if (type == ItemType.Mine)
                    {
                        // Take damage! If you are invincible, you just blow it up safely.
                        bool tookDamage = player.playerHit();
                        if (tookDamage && player.Lives <= 0)
                        {
                            TriggerGameOver("You stepped on a mine!");
                            return;
                        }
                    }
                }
            }

            // ENEMY COLLISION LOGIC
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
                            TriggerGameOver("You got swarmed!");
                            return;
                        }
                    }
                }
            }

            gameForm.Invalidate();
        }

        // Helper method to keep Game Over logic clean
        private void TriggerGameOver(string causeOfDeath)
        {
            isGameOver = true;
            survivalTimer.Stop();

            string finalTime = survivalTimer.Elapsed.ToString(@"mm\:ss");
            MessageBox.Show($"{causeOfDeath}\n\nLevel Reached: {difficultyLevel}\nSurvival Time: {finalTime}", "Game Over");

            gameForm.Close();
        }

        public void Draw(Graphics g)
        {
            // Draw items (so they render Underneath the player and enemies)
            foreach (var item in activeItems)
            {
                item.drawEntity(g);
            }

            player.drawEntity(g);

            foreach (var enemy in enemies)
            {
                enemy.drawEntity(g);
            }

            using (Font hudFont = new Font("Arial", 16, FontStyle.Bold))
            {
                string livesText = $"Lives: {player.Lives}";
                g.DrawString(livesText, hudFont, Brushes.Black, new PointF(10, 10));

                string timeText = $"Time: {survivalTimer.Elapsed.ToString(@"mm\:ss")}";
                SizeF textSize = g.MeasureString(timeText, hudFont);
                g.DrawString(timeText, hudFont, Brushes.Black, new PointF(gameForm.ClientSize.Width - textSize.Width - 10, 10));

                string diffText = $"Level: {difficultyLevel}";
                SizeF diffSize = g.MeasureString(diffText, hudFont);
                g.DrawString(diffText, hudFont, Brushes.DarkRed, new PointF((gameForm.ClientSize.Width / 2) - (diffSize.Width / 2), 10));
            }
        }
    }
}
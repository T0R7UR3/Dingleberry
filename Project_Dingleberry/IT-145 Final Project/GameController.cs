using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Project_Dingleberry
{
    internal class GameController
    {
        private const int EntitySize = 32;
        private const int HUDWall = 40;
        private const int SafeSpawnDistance = 300;
        private const int MaxSpawnAttempts = 50;
        private const int EscalateEveryXSeconds = 15;
        private const int MaxItemsOnScreen = 5;

        private GameStage gameForm;
        private Player player;
        private List<Enemy> enemies;
        private List<Item> activeItems;

        private int maxEnemies = 20;
        private int spawnTimer = 0;
        private int spawnInterval = 100;

        // The Master Random Generator
        private Random rand = new Random();

        private bool isGameOver = false;
        private bool isPaused = false;
        private Stopwatch survivalTimer;
        private int difficultyLevel = 1;

        private int itemSpawnTimer = 0;
        private int itemSpawnInterval = 300;

        public GameController(GameStage form)
        {
            gameForm = form;
            enemies = new List<Enemy>();
            activeItems = new List<Item>();

            player = new Player("john_stick.png");
            player.SetPos(640, 360); // Set once, right here!

            survivalTimer = new Stopwatch();
            survivalTimer.Start();

            // FIX: Passing the master 'rand' to the enemies
            Enemy chaser = new Enemy("enemy_chaser.png", EnemyType.Chaser, rand);
            chaser.SetPos(100, 100);
            enemies.Add(chaser);

            Enemy bouncer = new Enemy("enemy_chaser.png", EnemyType.Bouncer, rand);
            bouncer.SetPos(1000, 100);
            enemies.Add(bouncer);

            Enemy drifter = new Enemy("enemy_chaser.png", EnemyType.Drifter, rand);
            drifter.SetPos(640, 100);
            enemies.Add(drifter);
        }

        public void TogglePause()
        {
            if (isGameOver) return;

            isPaused = !isPaused;

            if (isPaused) survivalTimer.Stop();
            else survivalTimer.Start();
        }

        private void SpawnRandomEnemy()
        {
            bool hasChaser = false;
            foreach (var enemy in enemies)
            {
                if (enemy.Type == EnemyType.Chaser) { hasChaser = true; break; }
            }

            List<EnemyType> allowedTypes = new List<EnemyType> { EnemyType.Bouncer, EnemyType.Drifter };
            if (!hasChaser) allowedTypes.Add(EnemyType.Chaser);

            EnemyType randomType = allowedTypes[rand.Next(allowedTypes.Count)];
            Enemy newEnemy = new Enemy("Enemy.png", randomType, rand);

            int spawnX = 0;
            int spawnY = 0;
            int safeDistance = 300;
            bool safeSpotFound = false;
            int attempts = 0;

            // FIX: Capped at 50 attempts to prevent infinite loop crashes
            while (!safeSpotFound && attempts < 50)
            {
                spawnX = rand.Next(0, gameForm.ClientSize.Width - 32);
                spawnY = rand.Next(40, gameForm.ClientSize.Height - 32);

                double diffX = spawnX - player.GetX();
                double diffY = spawnY - player.GetY();
                double distance = Math.Sqrt(Math.Pow(diffX, 2) + Math.Pow(diffY, 2));

                if (distance >= safeDistance)
                {
                    safeSpotFound = true;
                }
                attempts++;
            }

            // If we couldn't find a safe spot in 50 tries, skip spawning this time
            if (safeSpotFound)
            {
                newEnemy.SetPos(spawnX, spawnY);
                enemies.Add(newEnemy);
            }
        }

        public Player GetPlayer() => player;

        public void Update()
        {
            if (isGameOver || isPaused) return;

            int currentSeconds = (int)survivalTimer.Elapsed.TotalSeconds;
            if (currentSeconds / 15 >= difficultyLevel)
            {
                difficultyLevel++;
                maxEnemies += 5;
                if (spawnInterval > 20) spawnInterval -= 10;
            }

            player.ProcessMovement();
            player.ClampToScreen(gameForm.ClientSize.Width, gameForm.ClientSize.Height);

            spawnTimer++;
            if (spawnTimer >= spawnInterval && enemies.Count < maxEnemies)
            {
                SpawnRandomEnemy();
                spawnTimer = 0;
            }

            itemSpawnTimer++;
            if (itemSpawnTimer >= itemSpawnInterval && activeItems.Count < 5)
            {
                ItemType[] itemTypes = (ItemType[])Enum.GetValues(typeof(ItemType));
                ItemType randomItem = itemTypes[rand.Next(itemTypes.Length)];

                Color itemColor = Color.Green;
                string imgName = "halve_enemies.png";

                if (randomItem == ItemType.Life) { itemColor = Color.Black; imgName = "extra_life.png"; }
                else if (randomItem == ItemType.Mine) { itemColor = Color.Yellow; imgName = "mine_bad.png"; }

                Item newItem = new Item(randomItem, imgName, itemColor);

                bool safeSpawnFound = false;
                int spawnX = 0;
                int spawnY = 0;
                int attempts = 0;

                // FIX: Capped attempts AND checking against everything on screen
                while (!safeSpawnFound && attempts < 50)
                {
                    spawnX = rand.Next(0, gameForm.ClientSize.Width - 32);
                    spawnY = rand.Next(40, gameForm.ClientSize.Height - 32);

                    Rectangle testBox = new Rectangle(spawnX, spawnY, 32, 32);
                    safeSpawnFound = true;

                    // Avoid other items
                    foreach (var existingItem in activeItems)
                    {
                        if (testBox.IntersectsWith(existingItem.Hitbox)) { safeSpawnFound = false; break; }
                    }

                    // Avoid Player
                    if (safeSpawnFound && testBox.IntersectsWith(player.Hitbox)) safeSpawnFound = false;

                    // Avoid Enemies
                    if (safeSpawnFound)
                    {
                        foreach (var enemy in enemies)
                        {
                            if (testBox.IntersectsWith(enemy.Hitbox)) { safeSpawnFound = false; break; }
                        }
                    }

                    attempts++;
                }

                if (safeSpawnFound)
                {
                    newItem.SetPos(spawnX, spawnY);
                    activeItems.Add(newItem);
                }
                itemSpawnTimer = 0;
            }

            for (int i = activeItems.Count - 1; i >= 0; i--)
            {
                if (player.Hitbox.IntersectsWith(activeItems[i].Hitbox))
                {
                    ItemType type = activeItems[i].Type;
                    activeItems.RemoveAt(i);

                    if (type == ItemType.Bomb)
                    {
                        int enemiesToDestroy = enemies.Count / 2;
                        for (int j = 0; j < enemiesToDestroy; j++)
                        {
                            if (enemies.Count > 0) enemies.RemoveAt(0);
                        }
                    }
                    else if (type == ItemType.Life)
                    {
                        player.AddLife();
                    }
                    else if (type == ItemType.Mine)
                    {
                        bool tookDamage = player.PlayerHit();
                        if (tookDamage && player.Lives <= 0)
                        {
                            TriggerGameOver("You stepped on a mine!");
                            return;
                        }
                    }
                }
            }

            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                enemies[i].Update(player, gameForm.ClientSize.Width, gameForm.ClientSize.Height);

                if (player.Hitbox.IntersectsWith(enemies[i].Hitbox))
                {
                    bool tookDamage = player.PlayerHit();

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

        private void TriggerGameOver(string causeOfDeath)
        {
            isGameOver = true;
            survivalTimer.Stop();

            string finalTime = survivalTimer.Elapsed.ToString(@"mm\:ss");

            DialogResult result = MessageBox.Show(
                $"{causeOfDeath}\n\nLevel Reached: {difficultyLevel}\nSurvival Time: {finalTime}",
                "Game Over",
                MessageBoxButtons.RetryCancel,
                MessageBoxIcon.Information);

            if (result == DialogResult.Retry)
            {
                gameForm.RestartGame();
            }
            else
            {
                gameForm.Close();
            }
        }

        public void Draw(Graphics g)
        {
            if (!isGameOver) player.DrawEntity(g);

            foreach (var item in activeItems) { item.DrawEntity(g); }
            foreach (var enemy in enemies) { enemy.DrawEntity(g); }

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

            if (isPaused && !isGameOver)
            {
                using (Font pauseFont = new Font("Arial", 48, FontStyle.Bold))
                {
                    string pauseText = "PAUSED";
                    SizeF pSize = g.MeasureString(pauseText, pauseFont);
                    float pX = (gameForm.ClientSize.Width - pSize.Width) / 2;
                    float pY = (gameForm.ClientSize.Height - pSize.Height) / 2;
                    g.DrawString(pauseText, pauseFont, Brushes.Gray, new PointF(pX, pY));
                }
            }
        }
    }
}
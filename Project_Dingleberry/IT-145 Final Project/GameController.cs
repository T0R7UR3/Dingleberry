#pragma warning disable IDE0130

using System.Diagnostics;

namespace Project_Dingleberry;

internal class GameController
{
    private const int HUDWall = 40;
    private const int SafeSpawnDistance = 300;
    private const int MaxSpawnAttempts = 50;
    private const int EscalateEveryXSeconds = 15;
    private const int MaxItemsOnScreen = 5;

    private const int EnemySize = 51;
    private const int ItemSize = 32;

    private const int ItemMargin = 60;
    private const int MinItemDistanceFromEnemy = 120;
    private const int MinItemDistanceFromItem = 90;
    private const int MinMineDistanceFromPlayer = 220;
    private const int MinHelpfulDistanceFromPlayer = 80;
    private const int MaxHelpfulDistanceFromPlayer = 550;

    private readonly GameStage gameForm;
    private readonly Player player;
    private readonly List<Enemy> enemies = [];
    private readonly List<Item> activeItems = [];
    private readonly Random rand = new();
    private readonly Stopwatch survivalTimer = new();

    private int maxEnemies = 20;
    private int spawnTimer;
    private int spawnInterval = 100;

    private bool isGameOver;
    private bool isPaused;
    private int difficultyLevel = 1;

    private int itemSpawnTimer;
    private readonly int itemSpawnInterval = 300;
    public GameController(GameStage form)
    {
        gameForm = form;

        player = new("john_stick.png");
        player.SetPos(960, 540);

        survivalTimer.Start();

        Enemy chaser = new("enemy_chaser.png", EnemyType.Chaser, rand);
        chaser.SetPos(100, 100);
        enemies.Add(chaser);

        Enemy bouncer = new("enemy_bouncer.png", EnemyType.Bouncer, rand);
        bouncer.SetPos(1000, 100);
        enemies.Add(bouncer);

        Enemy drifter = new("enemy_drifter.png", EnemyType.Drifter, rand);
        drifter.SetPos(640, 100);
        enemies.Add(drifter);
    }

    public void TogglePause()
    {
        if (isGameOver)
        {
            return;
        }

        isPaused = !isPaused;

        if (isPaused)
        {
            survivalTimer.Stop();
        }
        else
        {
            survivalTimer.Start();
        }
    }

    private void SpawnRandomEnemy()
    {
        bool hasChaser = false;

        foreach (Enemy enemy in enemies)
        {
            if (enemy.Type == EnemyType.Chaser)
            {
                hasChaser = true;
                break;
            }
        }

        List<EnemyType> allowedTypes =
        [
            EnemyType.Bouncer,
            EnemyType.Drifter
        ];

        if (!hasChaser)
        {
            allowedTypes.Add(EnemyType.Chaser);
        }

        EnemyType randomType = allowedTypes[rand.Next(allowedTypes.Count)];

        string enemySprite = randomType == EnemyType.Chaser
            ? "enemy_chaser.png"
            : randomType == EnemyType.Bouncer
                ? "enemy_bouncer.png"
                : "enemy_drifter.png";

        Enemy newEnemy = new(enemySprite, randomType, rand);

        int spawnX = 0;
        int spawnY = 0;
        bool safeSpotFound = false;
        int attempts = 0;

        while (!safeSpotFound && attempts < MaxSpawnAttempts)
        {
            spawnX = rand.Next(0, gameForm.ClientSize.Width - EnemySize);
            spawnY = rand.Next(HUDWall, gameForm.ClientSize.Height - EnemySize);

            double diffX = spawnX - player.GetX();
            double diffY = spawnY - player.GetY();
            double distance = Math.Sqrt((diffX * diffX) + (diffY * diffY));

            if (distance >= SafeSpawnDistance)
            {
                safeSpotFound = true;
            }

            attempts++;
        }

        if (safeSpotFound)
        {
            newEnemy.SetPos(spawnX, spawnY);
            enemies.Add(newEnemy);
        }
    }

    private ItemType GetRandomItemType()
    {
        int roll = rand.Next(100);

        if (player.Lives <= 1)
        {
            if (roll < 50)
            {
                return ItemType.Life;
            }

            if (roll < 85)
            {
                return ItemType.Bomb;
            }

            return ItemType.Mine;
        }

        if (player.Lives == 2)
        {
            if (roll < 45)
            {
                return ItemType.Life;
            }

            if (roll < 80)
            {
                return ItemType.Bomb;
            }

            return ItemType.Mine;
        }

        if (roll < 40)
        {
            return ItemType.Bomb;
        }

        if (roll < 75)
        {
            return ItemType.Life;
        }

        return ItemType.Mine;
    }

    private static Point GetCenter(Rectangle rect)
    {
        return new(rect.X + (rect.Width / 2), rect.Y + (rect.Height / 2));
    }

    private static double GetDistance(Point first, Point second)
    {
        int diffX = first.X - second.X;
        int diffY = first.Y - second.Y;

        return Math.Sqrt((diffX * diffX) + (diffY * diffY));
    }

    private bool IsFairDistanceFromPlayer(Rectangle testBox, ItemType itemType)
    {
        Point itemCenter = GetCenter(testBox);
        Point playerCenter = GetCenter(player.Hitbox);
        double distance = GetDistance(itemCenter, playerCenter);

        if (itemType == ItemType.Mine)
        {
            return distance >= MinMineDistanceFromPlayer;
        }

        return distance >= MinHelpfulDistanceFromPlayer &&
               distance <= MaxHelpfulDistanceFromPlayer;
    }

    private bool IsFarEnoughFromEnemies(Rectangle testBox)
    {
        Point itemCenter = GetCenter(testBox);

        foreach (Enemy enemy in enemies)
        {
            if (testBox.IntersectsWith(enemy.Hitbox))
            {
                return false;
            }

            Point enemyCenter = GetCenter(enemy.Hitbox);
            double distance = GetDistance(itemCenter, enemyCenter);

            if (distance < MinItemDistanceFromEnemy)
            {
                return false;
            }
        }

        return true;
    }

    private bool IsFarEnoughFromOtherItems(Rectangle testBox)
    {
        Point itemCenter = GetCenter(testBox);

        foreach (Item existingItem in activeItems)
        {
            if (testBox.IntersectsWith(existingItem.Hitbox))
            {
                return false;
            }

            Point existingItemCenter = GetCenter(existingItem.Hitbox);
            double distance = GetDistance(itemCenter, existingItemCenter);

            if (distance < MinItemDistanceFromItem)
            {
                return false;
            }
        }

        return true;
    }

    public Player GetPlayer()
    {
        return player;
    }

    public void Update()
    {
        if (isGameOver || isPaused)
        {
            return;
        }

        int currentSeconds = (int)survivalTimer.Elapsed.TotalSeconds;

        if (currentSeconds / EscalateEveryXSeconds >= difficultyLevel)
        {
            difficultyLevel++;
            maxEnemies += 5;

            if (spawnInterval > 20)
            {
                spawnInterval -= 10;
            }
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

        if (itemSpawnTimer >= itemSpawnInterval && activeItems.Count < MaxItemsOnScreen)
        {
            ItemType randomItem = GetRandomItemType();

            Color itemColor = Color.Green;
            string imgName = "half_enemy.png";

            if (randomItem == ItemType.Life)
            {
                itemColor = Color.Black;
                imgName = "extra_life.png";
            }
            else if (randomItem == ItemType.Mine)
            {
                itemColor = Color.Yellow;
                imgName = "bomb_mine.png";
            }

            Item newItem = new(randomItem, imgName, itemColor);

            bool safeSpawnFound = false;
            int spawnX = 0;
            int spawnY = 0;
            int attempts = 0;

            while (!safeSpawnFound && attempts < MaxSpawnAttempts)
            {
                spawnX = rand.Next(ItemMargin, gameForm.ClientSize.Width - ItemSize - ItemMargin);
                spawnY = rand.Next(HUDWall + ItemMargin, gameForm.ClientSize.Height - ItemSize - ItemMargin);

                newItem.SetPos(spawnX, spawnY);
                Rectangle testBox = newItem.Hitbox;

                safeSpawnFound = true;

                if (testBox.IntersectsWith(player.Hitbox))
                {
                    safeSpawnFound = false;
                }

                if (safeSpawnFound && !IsFarEnoughFromOtherItems(testBox))
                {
                    safeSpawnFound = false;
                }

                if (safeSpawnFound && !IsFarEnoughFromEnemies(testBox))
                {
                    safeSpawnFound = false;
                }

                if (safeSpawnFound && !IsFairDistanceFromPlayer(testBox, randomItem))
                {
                    safeSpawnFound = false;
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
                    SoundManager.PlayItemBomb();

                    int enemiesToDestroy = enemies.Count / 2;

                    // TWEAK: Remove enemies from the back of the list to prevent rendering glitches
                    for (int j = 0; j < enemiesToDestroy; j++)
                    {
                        if (enemies.Count > 0)
                        {
                            enemies.RemoveAt(enemies.Count - 1);
                        }
                    }
                }
                else if (type == ItemType.Life)
                {
                    SoundManager.PlayItemLife();
                    player.AddLife();
                }
                else if (type == ItemType.Mine)
                {
                    SoundManager.PlayItemMine();

                    for (int j = activeItems.Count - 1; j >= 0; j--)
                    {
                        if (activeItems[j].Type == ItemType.Mine)
                        {
                            activeItems.RemoveAt(j);
                        }
                    }

                    bool tookDamage = player.PlayerHit();

                    if (tookDamage && player.Lives <= 0)
                    {
                        TriggerGameOver("You stepped on a mine!");
                        return;
                    }
                }

                break;
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
                    SoundManager.PlayPlayerHit();
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

        SoundManager.PlayGameOver();

        string finalTime = survivalTimer.Elapsed.ToString(@"mm\:ss");

        HighScoreManager.SaveScore(difficultyLevel, finalTime);

        DialogResult result = MessageBox.Show(
            "Level Reached: " + difficultyLevel + "\nSurvival Time: " + finalTime + "\n\n" + causeOfDeath,
            "Game Over",
            MessageBoxButtons.RetryCancel,
            MessageBoxIcon.Information
        );

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
        if (!isGameOver)
        {
            player.DrawEntity(g);
        }

        foreach (Item item in activeItems)
        {
            item.DrawEntity(g);
        }

        foreach (Enemy enemy in enemies)
        {
            enemy.DrawEntity(g);
        }

        using Font hudFont = new("Arial", 16, FontStyle.Bold);

        string livesText = "Lives: " + player.Lives;
        g.DrawString(livesText, hudFont, Brushes.Black, new PointF(10, 10));

        SizeF livesSize = g.MeasureString(livesText, hudFont);

        string dashText = player.DashReady
            ? "Dash READY"
            : "Dash " + player.DashCooldownRemaining.ToString("0.0") + "s";

        float dashTextX = 10 + livesSize.Width + 20;
        g.DrawString(dashText, hudFont, Brushes.DarkBlue, new PointF(dashTextX, 10));

        SizeF dashTextSize = g.MeasureString(dashText, hudFont);

        string timeText = "Time " + survivalTimer.Elapsed.ToString(@"mm\:ss");
        SizeF timeSize = g.MeasureString(timeText, hudFont);
        float timeTextX = gameForm.ClientSize.Width - timeSize.Width - 10;

        int dashBarX = (int)(dashTextX + dashTextSize.Width + 15);
        int dashBarY = 12;
        int dashBarHeight = 16;

        int maxDashBarWidth = (int)(timeTextX - dashBarX - 20);
        int dashBarWidth = Math.Max(80, Math.Min(140, maxDashBarWidth));

        Rectangle dashBarOutline = new Rectangle(dashBarX, dashBarY, dashBarWidth, dashBarHeight);
        g.DrawRectangle(Pens.Black, dashBarOutline);

        int dashFillWidth = (int)((dashBarWidth - 4) * player.DashChargePercent);
        if (dashFillWidth > 0)
        {
            g.FillRectangle(Brushes.DeepSkyBlue, dashBarX + 2, dashBarY + 2, dashFillWidth, dashBarHeight - 4);
        }

        g.DrawString(
            timeText,
            hudFont,
            Brushes.Black,
            new PointF(timeTextX, 10)
        );

        string diffText = "Level " + difficultyLevel;
        SizeF diffSize = g.MeasureString(diffText, hudFont);
        g.DrawString(
            diffText,
            hudFont,
            Brushes.DarkRed,
            new PointF((gameForm.ClientSize.Width / 2) - (diffSize.Width / 2), 10)
        );

        if (isPaused && !isGameOver)
        {
            using Font pauseFont = new("Arial", 48, FontStyle.Bold);

            string pauseText = "PAUSED";
            SizeF pauseSize = g.MeasureString(pauseText, pauseFont);
            float pauseX = (gameForm.ClientSize.Width - pauseSize.Width) / 2;
            float pauseY = (gameForm.ClientSize.Height - pauseSize.Height) / 2;

            g.DrawString(pauseText, pauseFont, Brushes.Gray, new PointF(pauseX, pauseY));
        }
    }
}

#pragma warning restore IDE0130
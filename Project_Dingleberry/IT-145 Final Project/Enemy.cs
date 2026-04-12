using System;
using System.Drawing;

namespace Project_Dingleberry
{
    public enum EnemyType
    {
        Chaser,
        Bouncer,
        Drifter
    }

    public class Enemy : Entity
    {
        private const int ChaserSpeed = 3;
        private const int BaseSpeed = 4;
        private const int HUDWall = 40;

        private EnemyType type;
        public EnemyType Type
        {
            get { return type; }
            set { type = value; }
        }

        private int speedX = BaseSpeed;
        private int speedY = BaseSpeed;
        private Random rand;

        private string actualSprite;
        public bool IsSpawning { get; private set; }

        // FIX: Uses double for in-game time tracking
        private double spawnStartTime;
        private const double TelegraphDurationSeconds = 1.0;

        // FIX: Constructor now takes the current game time when spawned
        public Enemy(string fileName, EnemyType enemyType, Random sharedRand, double gameTime) : base("warning.png", Color.Red)
        {
            Type = enemyType;
            rand = sharedRand;

            width = 51;
            height = 51;

            actualSprite = fileName;
            IsSpawning = true;
            spawnStartTime = gameTime;
        }

        public override Rectangle Hitbox
        {
            get
            {
                return new Rectangle(
                    posX + 17,
                    posY + 6,
                    16,
                    38
                );
            }
        }

        // FIX: Now accepts gameTime to check spawn telegraphs safely
        public void Update(Player target, int screenWidth, int screenHeight, double gameTime)
        {
            if (IsSpawning)
            {
                if ((gameTime - spawnStartTime) >= TelegraphDurationSeconds)
                {
                    IsSpawning = false;
                    SetImage(actualSprite);
                }
                else
                {
                    return;
                }
            }

            switch (Type)
            {
                case EnemyType.Chaser:
                    if (posX < target.GetX()) posX += ChaserSpeed;
                    if (posX > target.GetX()) posX -= ChaserSpeed;
                    if (posY < target.GetY()) posY += ChaserSpeed;
                    if (posY > target.GetY()) posY -= ChaserSpeed;
                    break;

                case EnemyType.Bouncer:
                    posX += speedX;
                    posY += speedY;

                    if (posX <= 0 || posX >= screenWidth - width) speedX *= -1;
                    if (posY <= HUDWall || posY >= screenHeight - height) speedY *= -1;
                    break;

                case EnemyType.Drifter:
                    posX += speedX;
                    posY += speedY;

                    if (posX <= 0 || posX >= screenWidth - width) speedX *= -1;
                    if (posY <= HUDWall || posY >= screenHeight - height) speedY *= -1;

                    if (rand.Next(0, 100) < 2)
                    {
                        speedX = rand.Next(-BaseSpeed, BaseSpeed + 1);
                        speedY = rand.Next(-BaseSpeed, BaseSpeed + 1);
                    }
                    break;
            }
        }
    }
}
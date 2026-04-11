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

        public Enemy(string fileName, EnemyType enemyType, Random sharedRand) : base(fileName, Color.Red)
        {
            Type = enemyType;
            rand = sharedRand;

            // NEW: Enemies are now 51x51
            width = 51;
            height = 51;
        }

        public void Update(Player target, int screenWidth, int screenHeight)
        {
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

                    // NEW: Uses custom width/height for perfect bouncing
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
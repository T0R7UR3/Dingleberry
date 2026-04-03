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
        private EnemyType type;

        public EnemyType Type
        {
            get { return type; }
            set { type = value; }
        }

        private int speed = 3;
        private int speedX = 4;
        private int speedY = 4;
        private Random rand = new Random();

        // NEW: Passed Color.Red to the base Entity
        public Enemy(string fileName, EnemyType enemyType) : base(fileName, Color.Red)
        {
            Type = enemyType;
        }

        public void Update(Player target, int screenWidth, int screenHeight)
        {
            switch (Type)
            {
                case EnemyType.Chaser:
                    if (posX < target.GetX()) posX += speed;
                    if (posX > target.GetX()) posX -= speed;
                    if (posY < target.GetY()) posY += speed;
                    if (posY > target.GetY()) posY -= speed;
                    break;

                case EnemyType.Bouncer:
                    posX += speedX;
                    posY += speedY;

                    if (posX <= 0 || posX >= screenWidth - 30) speedX *= -1;
                    if (posY <= 0 || posY >= screenHeight - 30) speedY *= -1;
                    break;

                case EnemyType.Drifter:
                    posX += speedX;
                    posY += speedY;

                    if (posX <= 0 || posX >= screenWidth - 30) speedX *= -1;
                    if (posY <= 0 || posY >= screenHeight - 30) speedY *= -1;

                    if (rand.Next(0, 100) < 2)
                    {
                        speedX = rand.Next(-4, 5);
                        speedY = rand.Next(-4, 5);
                    }
                    break;
            }
        }
    }
}
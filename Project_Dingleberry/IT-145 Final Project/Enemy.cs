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
        //T17 Code added here -BDD

        public bool IsSpawning { get; private set; } = true;
        private int spawnTimer = 50; 
        private string finalSprite;

        //Code ended here -BDD

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
            //T17 code added here -BDD

            finalSprite = fileName;
            SetImage("warning.png"); // Placeholder for warning sprite

            //Code ended here -BDD

            Type = enemyType;
            rand = sharedRand;

            width = 51;
            height = 51;
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

        public void Update(Player target, int screenWidth, int screenHeight)
        {
            //T17 code added here -BDD
            
            if(IsSpawning)
            {
                spawnTimer--;
                if (spawnTimer <= 0)
                {
                    IsSpawning = false;
                    SetImage(finalSprite); // Switch to final sprite after spawning
                }
                return; // Skip movement while spawning
            }

            //Code ended here -BDD

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
using System;
using System.Drawing;

namespace Project_Dingleberry
{
    public enum ItemType
    {
        Bomb,
        Life,
        Mine
    }

    public class Item : Entity
    {
        public ItemType Type { get; private set; }

        public Item(ItemType type, string fileName, Color fallback) : base(fileName, fallback)
        {
            Type = type;
        }

        public override Rectangle Hitbox
        {
            get
            {
                return new Rectangle(
                    posX - 6,
                    posY - 6,
                    width + 12,
                    height + 12
                );
            }
        }
    }
}
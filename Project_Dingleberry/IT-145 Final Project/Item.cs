using System;
using System.Drawing;

namespace Project_Dingleberry
{
    public enum ItemType
    {
        Bomb,   // Clears the screen (Green)
        Life,   // Gives a health point (Black)
        Mine    // Hurts the player (Yellow)
    }

    public class Item : Entity
    {
        public ItemType Type { get; private set; }

        public Item(ItemType type, string fileName, Color fallback) : base(fileName, fallback)
        {
            Type = type;
        }
    }
}
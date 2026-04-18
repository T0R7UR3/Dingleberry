// Project: John Stick
// Course: IT145 Foundations of Application Development
// Authors: Murdock MacAskill, Beth, and Landen
// File: Item.cs
// Purpose: Defines collectible and hazardous items used during gameplay.
// Date: 04/17/2026

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
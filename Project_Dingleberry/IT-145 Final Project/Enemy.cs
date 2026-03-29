using System;
using System.Drawing;

namespace Project_Dingleberry
{
    // The ":" symbol makes Enemy inherit from your existing Entity class
    public class Enemy : Entity
    {
        // Constructor passes the fileName to the base Entity class to handle the image
        public Enemy(string fileName) : base(fileName)
        {
            // The starting coordinates are set in the GameController, 
            // Initialize enemy-specific stats (like health or speed) here later.
        }

        // Added an Update method so we have a place to put AI/movement logic later
        public void Update()
        {
            // TODO: Add movement or attack logic in future tasks
        }
    }
}
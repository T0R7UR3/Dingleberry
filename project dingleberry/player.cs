// Program: Project Dingleberry Player Class
// Author: Adorable Llama
// Date: 03/10/2026
//
// Purpose
// Player handles movement, health, hitbox, and placeholder drawing for a top down bullet hell game

using System;
using System.Drawing;

public class Player
{
    // Position of the player in game world coordinates (pixels)
    public PointF Position { get; private set; }

    // How fast the player moves in pixels per second
    public float Speed { get; set; } = 260f;

    // Player health as a simple integer value
    public int Health { get; private set; } = 5;

    // Radius used for a simple circular hitbox
    // We use a circle hitbox because it is fast and easy for a bullet hell game
    public float Radius { get; set; } = 14f;

    // Convenience property so the game loop can check if the player is alive
    public bool IsAlive
    {
        get { return Health > 0; }
    }

    // Constructor
    // startPosition is where the player begins on the screen
    // startingHealth sets the starting health
    public Player(PointF startPosition, int startingHealth)
    {
        Position = startPosition;
        Health = startingHealth;
    }

    // Update
    // Reads input booleans from the form and updates player position
    // deltaSeconds is the time since last frame so movement stays consistent
    // bounds is the playable area so the player cannot leave the screen
    public void Update(bool up, bool down, bool left, bool right, float deltaSeconds, RectangleF bounds)
    {
        // Convert button presses into a direction vector
        float dx = 0f;
        float dy = 0f;

        if (up) dy -= 1f;
        if (down) dy += 1f;
        if (left) dx -= 1f;
        if (right) dx += 1f;

        // Normalize the direction so diagonal movement is not faster
        // Example
        // Without normalization, moving up and right would move farther than just up
        float length = (float)Math.Sqrt((dx * dx) + (dy * dy));
        if (length > 0f)
        {
            dx /= length;
            dy /= length;
        }

        // Move the player using speed and delta time
        float newX = Position.X + (dx * Speed * deltaSeconds);
        float newY = Position.Y + (dy * Speed * deltaSeconds);

        // Clamp position to the bounds so the player stays on screen
        // We include Radius so the circle hitbox stays inside the bounds
        newX = Math.Max(bounds.Left + Radius, Math.Min(bounds.Right - Radius, newX));
        newY = Math.Max(bounds.Top + Radius, Math.Min(bounds.Bottom - Radius, newY));

        Position = new PointF(newX, newY);
    }

    // TakeDamage
    // Reduces health but never below 0
    public void TakeDamage(int amount)
    {
        Health = Math.Max(0, Health - amount);
    }

    // GetHitbox
    // Returns a rectangle that represents the player collision area
    // Even though we think of it as a circle, a rectangle works fine for simple collisions
    public RectangleF GetHitbox()
    {
        return new RectangleF(Position.X - Radius, Position.Y - Radius, Radius * 2f, Radius * 2f);
    }

    // Draw
    // Placeholder drawing for now
    // Later you can replace this with sprite drawing using Image and DrawImage
    public void Draw(Graphics g)
    {
        RectangleF r = GetHitbox();

        // Fill is the body
        g.FillEllipse(Brushes.Black, r);

        // Outline helps you see the hitbox while debugging
        g.DrawEllipse(Pens.White, r);
    }
}
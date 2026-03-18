// Program: Project Dingleberry Bullet Class
// Author: Adorable Llamas
// Date: 03/10/2026
//
// Purpose
// Bullet handles movement, hitbox, off screen despawn, collision checks, and placeholder drawing

using System.Drawing;

public class Bullet
{
    // Current bullet position
    public PointF Position { get; private set; }

    // Bullet velocity in pixels per second
    // Example
    // (300, 0) means it moves right at 300 pixels per second
    public PointF Velocity { get; private set; }

    // Radius for hitbox size
    public float Radius { get; set; } = 5f;

    // If false, the bullet should be removed or ignored by the game loop
    public bool IsActive { get; private set; } = true;

    // Constructor
    // startPosition is where the bullet spawns
    // velocity defines direction and speed
    public Bullet(PointF startPosition, PointF velocity)
    {
        Position = startPosition;
        Velocity = velocity;
    }

    // Update
    // Moves the bullet each frame
    // Deactivates it when it goes off screen so lists do not grow forever
    public void Update(float deltaSeconds, RectangleF bounds)
    {
        if (!IsActive) return;

        // Move based on velocity and delta time
        Position = new PointF(
            Position.X + (Velocity.X * deltaSeconds),
            Position.Y + (Velocity.Y * deltaSeconds)
        );

        // Despawn check
        // The 50 pixel padding prevents bullets from disappearing right on the edge
        if (Position.X < bounds.Left - 50f || Position.X > bounds.Right + 50f ||
            Position.Y < bounds.Top - 50f || Position.Y > bounds.Bottom + 50f)
        {
            IsActive = false;
        }
    }

    // GetHitbox
    // Returns a rectangle used for simple collision detection
    public RectangleF GetHitbox()
    {
        return new RectangleF(Position.X - Radius, Position.Y - Radius, Radius * 2f, Radius * 2f);
    }

    // CollidesWith
    // Basic rectangle intersection collision check
    public bool CollidesWith(RectangleF other)
    {
        return IsActive && GetHitbox().IntersectsWith(other);
    }

    // Deactivate
    // Lets the game loop turn off a bullet after a hit
    public void Deactivate()
    {
        IsActive = false;
    }

    // Draw
    // Placeholder drawing for now
    // Later replace with sprite drawing
    public void Draw(Graphics g)
    {
        if (!IsActive) return;

        RectangleF r = GetHitbox();
        g.FillEllipse(Brushes.Red, r);
    }
}
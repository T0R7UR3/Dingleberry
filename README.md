John Stick
Developed by Dingleberry Entertainment (DBE) Project Lead: Murdock John MacAskill III
"Play hard. Respawn harder."

Team: The Dingleberry Three (Murdock, Beth, Landon)

John Stick is a high-intensity, top-down Arena Avoider survival game. Built on the .NET 10 (LTS) framework, it challenges players to step into the shoes of the legendary John Stick to survive a claustrophobic "Battle Zone" against an escalating swarm of intelligent AI entities.

🕹️ Gameplay & Controls
You control John Stick (Our Hero). In a world of evil stickmen, movement and timing are your only weapons. Weave through the swarm, trigger screen-clearing bombs, and manage your tactical dash to survive the night.

Movement: W, A, S, D or Arrow Keys.

Tactical Dash: Spacebar. Provides a high-speed, invincible burst of movement on a 3-second cooldown.

Health: Start with 3 Lives (Max 5). Regenerate via items.

Invincibility: 1.5 seconds of "Flicker State" after taking damage.

Pause: P or Escape.

🚀 Key Features
1. "Boiling Frog" Difficulty System
The game utilizes a precision survival clock to manage tension. Every 15 seconds, the game level increases, bumping the enemy cap by +5 and aggressively decreasing the spawn interval. By the 2-minute mark, the arena becomes a test of near-perfect micro-movement.

2. Risk/Reward Item System
Dynamic items spawn every 5 seconds, forcing you to abandon safe corners:

half_enemy.png (Smart Bomb): A screen-wipe mechanic that obliterates 50% of the current swarm.

extra_life.png (Extra Life): High-value health regeneration that adds +1 to your life count.

bomb_mine.png (Landmine): Stationary hazards that cost a life if stepped on, but clear all other mines from the screen.

3. Kinetic AI Swarm
Chaser (enemy_chaser.png): Relentless pathfinding AI that tracks player coordinates.

Bouncer (enemy_bouncer.png): High-speed physics-based entities that ricochet off screen boundaries.

Drifter (enemy_drifter.png): Unpredictable, random-walk AI designed to break player patterns.

🛠️ Technical Specifications
Framework: .NET 10.0 (LTS).

Language: C#.

Engine: Custom GDI+ Rendering Engine.

Resolution: 1920x1080 (Full HD).

Audio Engine: Powered by NAudio. Features a dedicated mixer for overlapping sound effects and preloaded cached audio.

Physics: 0.707 Diagonal Normalization. No diagonal speed advantage; John Stick moves at a consistent speed in all directions.

Memory Optimization: Image Caching system prevents repeated disk I/O, ensuring smooth frame rates even with dozens of active entities.

👥 The Dingleberry Three (Credits)
This project was developed through a collaborative 1/3 split:

Murdock: Lead Architecture & Game Controller.

Beth: Asset Management & UI Design.

Landon: AI Behavior & Physics.

📦 Installation
Download the latest Release zip.

Ensure .NET 10 Runtime is installed.

Important: The Assets/ folder must remain in the same directory as Project_Dingleberry.exe to load sprites and sounds.

Run and survive.

© 2026 Dingleberry Entertainment. All Rights Reserved.

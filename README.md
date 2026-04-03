# Project Dingleberry
### Developed by Dingleberry Entertainment (DBE)

**Project Dingleberry** is a high-intensity, top-down **Arena Avoider** survival game. Built on the cutting-edge **.NET 10 (LTS)** framework, it challenges players to navigate a claustrophobic "Battle Zone" against an escalating swarm of intelligent AI entities.

> *“Play hard. Respawn harder”*

---

## 🕹️ Gameplay & Controls

You control the **Blue Hero**. In a world of red squares, movement is your only weapon. Weave through the swarm, trigger screen-clearing bombs, and manage your invincibility windows to survive.

* **Movement:** `W, A, S, D` or `Arrow Keys`
* **Health:** 3 Lives (Regenerate via Black Life items).
* **Invincibility:** 1.5 seconds of "Flicker State" after taking damage.

---

## 🚀 Key Features

### 1. "Boiling Frog" Difficulty System
The game utilizes a precision survival clock to manage tension. Every **15 seconds**, the game level increases, bumping the enemy cap by **+5** and aggressively decreasing the spawn interval. By the 2-minute mark, the arena becomes a test of near-perfect micro-movement.

### 2. Risk/Reward Item System
Dynamic items spawn every 5 seconds, forcing the player to abandon safe corners:
* **🟢 Green Bomb:** Screen-wipe mechanic that obliterates 50% of the current swarm.
* **⚫ Black Life:** High-value health regeneration.
* **🟡 Landmine:** Stationary hazards that punish careless movement.

### 3. Kinetic AI Swarm
* **Chaser:** Relentless pathfinding AI that tracks player coordinates.
* **Bouncer:** High-speed physics-based entities that ricochet off screen boundaries.
* **Drifter:** Unpredictable, random-walk AI designed to break player patterns.

---

## 🛠️ Technical Specifications

* **Framework:** .NET 10.0 (LTS)
* **Language:** C#
* **Engine:** Custom GDI+ Rendering Engine
* **Resolution:** 1280x720 (Widescreen)
* **Physics:** $0.707$ Diagonal Normalization (No diagonal speed advantage).
* **Precision:** High-fidelity `Stopwatch` class for frame-independent scoring.

---

## 👥 The Dingleberry Three (Credits)

This project was developed through a collaborative 1/3 split:

* **Murdock**
* **Beth**
* **Landon**

---

## 📦 Installation

1.  Download the latest **Release** zip.
2.  Ensure **.NET 10 Runtime** is installed.
3.  **Important:** The `Images/` folder must remain in the same directory as `Project_Dingleberry.exe`.
4.  Run and survive.

---

© 2026 Dingleberry Entertainment. All Rights Reserved.

# Project Dingleberry 🛸

A high-performance, top-down "Bullet Hell" shooter developed in **C#** using the **Windows Forms** framework. This project demonstrates core game engineering principles, including custom game loops, entity-component logic, and physics normalization.

## 🚀 Features

* **Custom Game Engine:** Built from scratch using a `System.Windows.Forms.Timer` loop running at a smooth 60 FPS (~16ms ticks).
* **Dual-Form Architecture:** Features a dedicated Main Menu (`John_Stick`) and a separate high-performance Battle Zone (`GameStage`).
* **Precision Movement:** Implements a Boolean-based input system to eliminate Windows key-repeat latency.
* **Diagonal Normalization:** Includes mathematical vector scaling ($0.707$ multiplier) to ensure consistent movement speed in all 8 directions.
* **Boundary Enforcement:** Dynamic clamping logic keeps entities within the playable `ClientSize` regardless of window borders.

## 🛠️ Technical Stack

* **Language:** C# 10.0+
* **Framework:** .NET 6.0 / Windows Forms
* **IDE:** Visual Studio 2022

## 📂 Project Structure

* **`John_Stick.cs`**: The Entry Point / Main Menu controller.
* **`GameStage.cs`**: The primary game window; handles keyboard events and the `OnPaint` override.
* **`GameController.cs`**: The "Brain" of the game. Manages the update/draw cycle and entity lists.
* **`Entity.cs`**: The base class for all game objects, providing position, hitboxes, and sprite rendering.
* **`Player.cs`**: Extended entity logic specifically for user-controlled movement and screen clamping.

## 🕹️ Controls

| Key | Action |
| :--- | :--- |
| **W / Up Arrow** | Move Up |
| **S / Down Arrow** | Move Down |
| **A / Left Arrow** | Move Left |
| **D / Right Arrow** | Move Right |

## 🛠️ Setup & Installation

1. Clone the repository to your local machine.
2. Open `Project_Dingleberry.sln` in Visual Studio.
3. Ensure the `Images/` folder in the output directory (`bin/Debug/net10.0-windows/`) contains `Player.png` and `Enemy.png`.
4. Press **F5** to Build and Run.

---
*Developed for IT-145 Final Project.*

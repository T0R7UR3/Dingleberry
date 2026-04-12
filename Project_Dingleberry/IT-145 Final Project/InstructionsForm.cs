using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Project_Dingleberry
{
    public partial class InstructionsForm : Form
    {
        public InstructionsForm()
        {
            // Setup the window - Increased height from 700 to 800!
            this.Size = new Size(800, 800);
            this.Text = "How to Play: Battle Zone";
            this.BackColor = Color.Black;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Moved the Close button down to fit the taller window
            Button closeButton = new Button();
            closeButton.Text = "Close";
            closeButton.Font = new Font("Arial", 12, FontStyle.Bold);
            closeButton.Size = new Size(150, 40);
            closeButton.Location = new Point((this.ClientSize.Width - 150) / 2, this.ClientSize.Height - 60);
            closeButton.BackColor = Color.Gray;
            closeButton.ForeColor = Color.White;
            closeButton.Click += (s, e) => this.Close();
            this.Controls.Add(closeButton);
        }

        private Image LoadSprite(string fileName)
        {
            try
            {
                return Image.FromFile(Path.Combine(AppContext.BaseDirectory, "Assets", "Images", fileName));
            }
            catch
            {
                Bitmap bmp = new Bitmap(32, 32);
                using (Graphics g = Graphics.FromImage(bmp)) { g.Clear(Color.Red); }
                return bmp;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            Font titleFont = new Font("Arial", 24, FontStyle.Bold);
            Font sectionFont = new Font("Arial", 18, FontStyle.Bold);
            Font textFont = new Font("Arial", 12, FontStyle.Regular);
            Brush textBrush = Brushes.White;

            int currentY = 20;

            // --- HEADER ---
            string titleText = "BATTLE ZONE FIELD MANUAL";
            SizeF titleSize = g.MeasureString(titleText, titleFont);
            float titleX = (this.ClientSize.Width - titleSize.Width) / 2;

            g.DrawString(titleText, titleFont, textBrush, new PointF(titleX, currentY));
            currentY += 50;

            // --- CONTROLS ---
            g.DrawString("--- CONTROLS ---", sectionFont, Brushes.Gray, new PointF(20, currentY));
            currentY += 40;

            g.DrawString("Move: W, A, S, D or Arrow Keys", textFont, textBrush, new PointF(20, currentY));
            currentY += 25;
            g.DrawString("Dash: Spacebar (Invincible burst! 3s cooldown)", textFont, textBrush, new PointF(20, currentY));
            currentY += 25;
            g.DrawString("Pause: P or Escape", textFont, textBrush, new PointF(20, currentY));
            currentY += 45;

            // --- ITEMS ---
            g.DrawString("--- ITEMS (Max 5 on screen) ---", sectionFont, Brushes.Gray, new PointF(20, currentY));
            currentY += 35;

            // Draw Item Sprites (With a white background box so black lines are visible!)
            g.FillRectangle(Brushes.White, 30, currentY, 40, 40);
            g.DrawImage(LoadSprite("extra_life.png"), 30, currentY, 40, 40);
            g.DrawString("Extra Life: Grants +1 Life (Max 5).", textFont, textBrush, new PointF(85, currentY + 10));
            currentY += 55;

            g.FillRectangle(Brushes.White, 30, currentY, 40, 40);
            g.DrawImage(LoadSprite("half_enemy.png"), 30, currentY, 40, 40);
            g.DrawString("Smart Bomb: Destroys half of all active enemies!", textFont, textBrush, new PointF(85, currentY + 10));
            currentY += 55;

            g.FillRectangle(Brushes.White, 30, currentY, 40, 40);
            g.DrawImage(LoadSprite("bomb_mine.png"), 30, currentY, 40, 40);
            g.DrawString("Mine: DANGER! Stepping on this costs a life,\nbut removes all others on the screen.", textFont, textBrush, new PointF(85, currentY + 5));

            currentY += 75;

            // --- ENEMIES ---
            g.DrawString("--- THE SWARM ---", sectionFont, Brushes.Gray, new PointF(20, currentY));
            currentY += 35;

            g.FillRectangle(Brushes.White, 30, currentY, 45, 45);
            g.DrawImage(LoadSprite("enemy_chaser.png"), 30, currentY, 45, 45);
            g.DrawString("Chaser: Relentlessly follows your every move.", textFont, textBrush, new PointF(85, currentY + 10));
            currentY += 55;

            g.FillRectangle(Brushes.White, 30, currentY, 45, 45);
            g.DrawImage(LoadSprite("enemy_bouncer.png"), 30, currentY, 45, 45);
            g.DrawString("Bouncer: Ricochets off walls at high speeds.", textFont, textBrush, new PointF(85, currentY + 10));
            currentY += 55;

            g.FillRectangle(Brushes.White, 30, currentY, 45, 45);
            g.DrawImage(LoadSprite("enemy_drifter.png"), 30, currentY, 45, 45);
            g.DrawString("Drifter: Bounces off walls but randomly changes speed.", textFont, textBrush, new PointF(85, currentY + 10));
            currentY += 55;

            g.FillRectangle(Brushes.White, 30, currentY, 45, 45);
            g.DrawImage(LoadSprite("warning.png"), 30, currentY, 45, 45);
            g.DrawString("Telegraph: A 1-second warning before an enemy spawns.", textFont, textBrush, new PointF(85, currentY + 10));
        }
    }
}
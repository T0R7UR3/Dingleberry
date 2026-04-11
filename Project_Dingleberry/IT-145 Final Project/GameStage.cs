using System;
using System.Drawing;
using System.Windows.Forms;

namespace Project_Dingleberry
{
    public partial class GameStage : Form
    {
        private GameController controller;
        private System.Windows.Forms.Timer gameTimer = new System.Windows.Forms.Timer();

        public GameStage()
        {
            // NEW: Set resolution to 1080p!
            this.Size = new Size(1920, 1080);
            this.Text = "Project Dingleberry - Battle Zone";
            this.BackColor = Color.White;
            this.DoubleBuffered = true;
            this.StartPosition = FormStartPosition.CenterScreen;

            this.KeyPreview = true;

            controller = new GameController(this);

            gameTimer.Interval = 16;
            gameTimer.Tick += (s, e) => controller.Update();
            gameTimer.Start();
        }

        public void RestartGame()
        {
            controller = new GameController(this);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            var p = controller.GetPlayer();
            if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up) p.IsMovingUp = true;
            if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down) p.IsMovingDown = true;
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left) p.IsMovingLeft = true;
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right) p.IsMovingRight = true;

            if (e.KeyCode == Keys.P || e.KeyCode == Keys.Escape) controller.TogglePause();
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            var p = controller.GetPlayer();
            if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up) p.IsMovingUp = false;
            if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down) p.IsMovingDown = false;
            if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left) p.IsMovingLeft = false;
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right) p.IsMovingRight = false;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            controller?.Draw(e.Graphics);
        }
    }
}
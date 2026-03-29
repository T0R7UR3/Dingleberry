using System;
using System.Drawing;
using System.Windows.Forms;

namespace Project_Dingleberry
{
    public partial class John_Stick : Form
    {
        private GameController controller;
        private System.Windows.Forms.Timer gameTimer = new System.Windows.Forms.Timer();

        public John_Stick()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Text = "Project Dingleberry - Bullet Hell";

            // Initialize the controller that manages the player and enemies
            controller = new GameController(this);

            // Set up the high-speed game loop (~60 Frames Per Second)
            gameTimer.Interval = 16;
            gameTimer.Tick += (s, e) => controller.Update();
            gameTimer.Start();
        }

        // Handles WASD and Arrow keys
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            controller.HandleKeyPress(e.KeyCode);
        }

        // Draws the game objects whenever the screen refreshes
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            controller.Draw(e.Graphics);
        }

        // FIX: The Designer needs these functions to exist for the buttons to work!
        private void play_Click(object sender, EventArgs e)
        {
            // If you have a separate game form, you can open it here:
            // playButtonForm nextForm = new playButtonForm();
            // nextForm.Show();

            MessageBox.Show("Game Started!");
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
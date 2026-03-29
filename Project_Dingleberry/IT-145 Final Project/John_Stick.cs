using System;
using System.Drawing;
using System.Windows.Forms;

namespace Project_Dingleberry
{
    public partial class John_Stick : Form
    {
        // No timer or controller here anymore! 
        // They live in GameStage.cs now.

        public John_Stick()
        {
            InitializeComponent();
            this.Text = "Project Dingleberry - Main Menu";
        }

        private void play_Click(object sender, EventArgs e)
        {
            // 1. Create the new game window
            GameStage stage = new GameStage();

            // 2. Setup the "Back to Menu" logic
            stage.FormClosed += (s, args) => this.Show();

            // 3. Show the game and hide the menu
            stage.Show();
            stage.Activate();
            stage.Focus();

            this.Hide();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Designer needs these, we keep them empty
        private void high_scores_Click(object sender, EventArgs e) { }
    }
}
// Project: John Stick
// Course: IT145 Foundations of Application Development
// Authors: Murdock MacAskill, Beth, and Landen
// File: John_Stick.cs
// Purpose: Controls the main menu, splash screen, and menu navigation.
// Date: 04/17/2026

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Project_Dingleberry
{
    public partial class John_Stick : Form
    {
        public John_Stick()
        {
            InitializeComponent();
            SoundManager.Initialize();
            SetupSplashScreen();
        }

        private void SetupSplashScreen()
        {
            foreach (Control control in this.Controls)
            {
                control.Visible = false;
            }

            Panel splashPanel = new Panel();
            splashPanel.Dock = DockStyle.Fill;
            splashPanel.BackColor = Color.Black;

            Label studioLabel = new Label();
            studioLabel.Text = "DINGLEBERRY ENTERTAINMENT\n\npresents";
            studioLabel.ForeColor = Color.White;
            studioLabel.Font = new Font("Arial", 36, FontStyle.Bold);
            studioLabel.AutoSize = false;
            studioLabel.TextAlign = ContentAlignment.MiddleCenter;
            studioLabel.Dock = DockStyle.Fill;

            splashPanel.Controls.Add(studioLabel);
            this.Controls.Add(splashPanel);
            splashPanel.BringToFront();

            System.Windows.Forms.Timer splashTimer = new System.Windows.Forms.Timer();
            splashTimer.Interval = 3000;
            splashTimer.Tick += (s, e) =>
            {
                splashTimer.Stop();
                this.Controls.Remove(splashPanel);
                splashPanel.Dispose();

                foreach (Control control in this.Controls)
                {
                    control.Visible = true;
                }
            };

            splashTimer.Start();
        }

        private void play_Click(object sender, EventArgs e)
        {
            SoundManager.PlayMenuStart();

            System.Windows.Forms.Timer startTimer = new System.Windows.Forms.Timer();
            startTimer.Interval = 120;

            startTimer.Tick += (s, args) =>
            {
                startTimer.Stop();
                startTimer.Dispose();

                GameStage gameWindow = new GameStage();

                this.Hide();
                gameWindow.Show();

                gameWindow.FormClosed += (sender2, args2) => this.Close();
            };

            startTimer.Start();
        }

        private void high_scores_Click(object sender, EventArgs e)
        {
            SoundManager.PlayMenuHighScore();

            var topScores = HighScoreManager.GetTopScores();
            string scoreText = string.Join("\n\n", topScores);

            MessageBox.Show(
                $"--- TOP 10 DEFENDERS ---\n\n{scoreText}",
                "Dingleberry Entertainment",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void instructions_Click(object sender, EventArgs e)
        {
            SoundManager.PlayMenuInstructions();

            // Open the new visual instructions window
            InstructionsForm infoWindow = new InstructionsForm();
            infoWindow.ShowDialog(); // ShowDialog freezes the main menu until they close the instructions
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            SoundManager.DisposeAll();
            base.OnFormClosed(e);
        }
    }
}
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
            GameStage gameWindow = new GameStage();

            this.Hide();
            gameWindow.Show();

            gameWindow.FormClosed += (s, args) => this.Close();
        }

        private void high_scores_Click(object sender, EventArgs e)
        {
            // Fetch the sorted list of scores
            var topScores = HighScoreManager.GetTopScores();

            // Join them together with newlines
            string scoreText = string.Join("\n\n", topScores);

            MessageBox.Show(
                $"--- TOP 10 DEFENDERS ---\n\n{scoreText}",
                "Dingleberry Entertainment",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close(); // Fully closes the application!
        }

        private void instructions_Click(object sender, EventArgs e)
       
        {
            
            string myMessage = "Controls:\n" +
                               "W, A, S, D or Up, Down, Left, Right arrow keys to move.\n" +
                               "Avoid enemies and bombs. Both will take a life. \n" +
                               "Collect lives (hearts) and reduce enemy count by collecting the 1/2 symbols.\n" +
                               "A maximum of five items (bombs, lives, and 1/2's) will be available at a time, so choose wisely\n\n" +
                               "Created by Dingleberry Entertainment.";

            
            MessageBox.Show(
                myMessage,                 
                "How to Play",             
                MessageBoxButtons.OK,      
                MessageBoxIcon.Information 
            );
        }
    
    }
}
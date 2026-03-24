using System;
using System.Drawing;
using System.Windows.Forms;

namespace IT_145_Final_Project
{
    public partial class John_Stick : Form
    {
        private Player player;

        public John_Stick()
        {
            InitializeComponent();

            player = new Player(200, 200);

            this.Paint += Form1_Paint;

            this.DoubleBuffered = true;

            this.Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            player.Draw(e.Graphics);
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void play_Click(object sender, EventArgs e)
        {
            playButtonForm nextForm = new playButtonForm();
            nextForm.Show();
        }
    }
}
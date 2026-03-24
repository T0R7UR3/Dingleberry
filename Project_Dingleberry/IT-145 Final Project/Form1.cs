using System;
using System.Windows.Forms;

namespace IT_145_Final_Project
{
    public partial class John_Stick : Form
    {
        private Player player;
        private Entity person;

        private Timer gameTimer = new Timer();

        public John_Stick()
        {
            InitializeComponent();

            // Create player and entity using THIS form
            player = new Player(this);
            person = new Entity(this);

            // Set starting positions
            player.setPos(200, 200);
            person.setPos(300, 200);

            // Timer setup (game loop)
            gameTimer.Interval = 16; // ~60 FPS
            gameTimer.Tick += GameLoop;
            gameTimer.Start();

            this.DoubleBuffered = true;
        }

        private void GameLoop(object sender, EventArgs e)
        {
            // Update positions visually
            player.drawEntity();
            person.drawEntity();

            // Clamp player to screen (fixes your bug)
            player.clampToScreen(this.ClientSize.Width, this.ClientSize.Height);
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
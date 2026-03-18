namespace IT_145_Final_Project
{
    public partial class John_Stick : Form
    {
        public John_Stick()
        {
            InitializeComponent();
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

namespace Project_Dingleberry
{
    partial class John_Stick
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            title = new TextBox();
            play = new Button();
            high_scores = new Button();
            exit = new Button();
            instructions = new Button();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // title
            // 
            title.BackColor = Color.Black;
            title.BorderStyle = BorderStyle.None;
            title.Font = new Font("Arial", 72F, FontStyle.Bold, GraphicsUnit.Point, 0);
            title.ForeColor = Color.Black;
            title.Location = new Point(322, 112);
            title.Margin = new Padding(6);
            title.Name = "title";
            title.Size = new Size(991, 221);
            title.TabIndex = 3;
            title.Text = "John Stick";
            title.TextAlign = HorizontalAlignment.Center;
            // 
            // play
            // 
            play.BackColor = Color.Black;
            play.BackgroundImage = IT_145_Final_Project.Properties.Resources.grey_button;
            play.BackgroundImageLayout = ImageLayout.Stretch;
            play.FlatAppearance.BorderColor = Color.Red;
            play.FlatAppearance.BorderSize = 3;
            play.Font = new Font("Arial", 19.875F, FontStyle.Bold, GraphicsUnit.Point, 0);
            play.ForeColor = SystemColors.InfoText;
            play.Location = new Point(629, 526);
            play.Margin = new Padding(6);
            play.Name = "play";
            play.Size = new Size(383, 162);
            play.TabIndex = 2;
            play.Text = "Start Game";
            play.UseVisualStyleBackColor = false;
            play.Click += play_Click;
            // 
            // high_scores
            // 
            high_scores.BackColor = Color.Black;
            high_scores.BackgroundImage = IT_145_Final_Project.Properties.Resources.grey_button;
            high_scores.BackgroundImageLayout = ImageLayout.Stretch;
            high_scores.Font = new Font("Arial", 10.875F, FontStyle.Bold, GraphicsUnit.Point, 0);
            high_scores.ForeColor = SystemColors.MenuText;
            high_scores.Location = new Point(662, 749);
            high_scores.Margin = new Padding(6);
            high_scores.Name = "high_scores";
            high_scores.Size = new Size(319, 153);
            high_scores.TabIndex = 1;
            high_scores.Text = "High Scores";
            high_scores.UseVisualStyleBackColor = false;
            high_scores.Click += high_scores_Click;
            // 
            // exit
            // 
            exit.BackColor = Color.Black;
            exit.BackgroundImage = IT_145_Final_Project.Properties.Resources.grey_button;
            exit.BackgroundImageLayout = ImageLayout.Stretch;
            exit.Font = new Font("Arial", 10.875F, FontStyle.Bold, GraphicsUnit.Point, 0);
            exit.ForeColor = SystemColors.InfoText;
            exit.Location = new Point(1216, 749);
            exit.Margin = new Padding(6);
            exit.Name = "exit";
            exit.Size = new Size(290, 153);
            exit.TabIndex = 0;
            exit.Text = "Exit";
            exit.UseVisualStyleBackColor = false;
            exit.Click += exit_Click;
            // 
            // instructions
            // 
            instructions.BackColor = Color.Black;
            instructions.BackgroundImage = IT_145_Final_Project.Properties.Resources.grey_button;
            instructions.BackgroundImageLayout = ImageLayout.Stretch;
            instructions.Font = new Font("Arial", 10.875F, FontStyle.Bold, GraphicsUnit.Point, 0);
            instructions.ForeColor = SystemColors.MenuText;
            instructions.Location = new Point(109, 749);
            instructions.Margin = new Padding(6);
            instructions.Name = "instructions";
            instructions.Size = new Size(322, 153);
            instructions.TabIndex = 4;
            instructions.Text = "Instructions";
            instructions.UseVisualStyleBackColor = false;
            instructions.Click += instructions_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = IT_145_Final_Project.Properties.Resources.Title;
            pictureBox1.Location = new Point(155, 21);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(1317, 463);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            // 
            // John_Stick
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(1601, 961);
            Controls.Add(pictureBox1);
            Controls.Add(instructions);
            Controls.Add(exit);
            Controls.Add(high_scores);
            Controls.Add(play);
            Controls.Add(title);
            Margin = new Padding(6);
            Name = "John_Stick";
            Text = "Project Dingleberry: Battle Zone";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox title;
        private System.Windows.Forms.Button play;
        private System.Windows.Forms.Button high_scores;
        private System.Windows.Forms.Button exit;
        private Button instructions;
        private PictureBox pictureBox1;
    }
}
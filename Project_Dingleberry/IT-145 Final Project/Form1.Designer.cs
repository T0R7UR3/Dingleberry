namespace Project_Dingleberry
{
    public partial class John_Stick
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            title = new TextBox();
            play = new Button();
            high_scores = new Button();
            exit = new Button();
            SuspendLayout();
            // 
            // title
            // 
            title.BackColor = SystemColors.ControlText;
            title.BorderStyle = BorderStyle.None;
            title.Font = new Font("Bahnschrift Condensed", 72F, FontStyle.Regular, GraphicsUnit.Point, 0);
            title.ForeColor = Color.Red;
            title.Location = new Point(487, 49);
            title.Name = "title";
            title.Size = new Size(675, 231);
            title.TabIndex = 0;
            title.Text = "TITLE";
            title.TextAlign = HorizontalAlignment.Center;
            // 
            // play
            // 
            play.BackColor = Color.Red;
            play.Location = new Point(123, 543);
            play.Name = "play";
            play.Size = new Size(266, 129);
            play.TabIndex = 1;
            play.Text = "Start Game";
            play.UseVisualStyleBackColor = false;
            play.Click += play_Click;
            // 
            // high_scores
            // 
            high_scores.BackColor = Color.Red;
            high_scores.Location = new Point(697, 543);
            high_scores.Name = "high_scores";
            high_scores.Size = new Size(258, 129);
            high_scores.TabIndex = 2;
            high_scores.Text = "High Scores";
            high_scores.UseVisualStyleBackColor = false;
            // 
            // exit
            // 
            exit.BackColor = Color.Red;
            exit.Location = new Point(1235, 543);
            exit.Name = "exit";
            exit.Size = new Size(223, 129);
            exit.TabIndex = 3;
            exit.Text = "Exit";
            exit.UseVisualStyleBackColor = false;
            exit.Click += exit_Click;
            // 
            // John_Stick
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Black;
            ClientSize = new Size(1590, 973);
            Controls.Add(exit);
            Controls.Add(high_scores);
            Controls.Add(play);
            Controls.Add(title);
            Name = "John_Stick";
            Text = ": THE GAME";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox title;
        private Button play;
        private Button high_scores;
        private Button exit;
    }
}

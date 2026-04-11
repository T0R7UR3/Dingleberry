namespace Project_Dingleberry
{
    public partial class John_Stick
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
            title = new System.Windows.Forms.TextBox();
            play = new System.Windows.Forms.Button();
            high_scores = new System.Windows.Forms.Button();
            exit = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // title
            // 
            title.BackColor = System.Drawing.SystemColors.ControlText;
            title.BorderStyle = System.Windows.Forms.BorderStyle.None;
            title.Font = new System.Drawing.Font("Arial", 50, FontStyle.Bold);
            title.ForeColor = System.Drawing.Color.White;
            title.Location = new System.Drawing.Point(487, 67);
            title.Name = "title";
            title.Size = new System.Drawing.Size(675, 231);
            title.TabIndex = 0;
            title.Text = "John Stick";
            title.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;

            // 
            // play
            // 
            play.BackColor = System.Drawing.Color.White;
            play.Font = new System.Drawing.Font("Arial", 15, FontStyle.Bold);
            play.Location = new System.Drawing.Point(123, 543);
            play.Name = "play";
            play.Size = new System.Drawing.Size(266, 129);
            play.TabIndex = 1;
            play.Text = "Start Game";
            play.UseVisualStyleBackColor = false;
            play.Click += new System.EventHandler(this.play_Click);
            play.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // high_scores
            // 
            high_scores.BackColor = System.Drawing.Color.White;
            high_scores.Font = new System.Drawing.Font("Arial", 15, FontStyle.Bold);
            high_scores.Location = new System.Drawing.Point(697, 543);
            high_scores.Name = "high_scores";
            high_scores.Size = new System.Drawing.Size(258, 129);
            high_scores.TabIndex = 2;
            high_scores.Text = "High Scores";
            high_scores.UseVisualStyleBackColor = false;
            high_scores.Click += new System.EventHandler(this.high_scores_Click);
            // 
            // exit
            // 
            exit.BackColor = System.Drawing.Color.White;
            exit.Font = new System.Drawing.Font("Arial", 15, FontStyle.Bold);
            exit.Location = new System.Drawing.Point(1235, 543);
            exit.Name = "exit";
            exit.Size = new System.Drawing.Size(223, 129);
            exit.TabIndex = 3;
            exit.Text = "Exit";
            exit.UseVisualStyleBackColor = false;
            exit.Click += new System.EventHandler(this.exit_Click);
            // 
            // John_Stick
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.Black;
            ClientSize = new System.Drawing.Size(1590, 973);
            Controls.Add(exit);
            Controls.Add(high_scores);
            Controls.Add(play);
            Controls.Add(title);
            Name = "John_Stick";
            Text = "Project Dingleberry: Battle Zone";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox title;
        private System.Windows.Forms.Button play;
        private System.Windows.Forms.Button high_scores;
        private System.Windows.Forms.Button exit;
    }
}
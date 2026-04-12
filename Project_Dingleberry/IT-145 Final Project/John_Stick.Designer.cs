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
            this.title = new System.Windows.Forms.TextBox();
            this.play = new System.Windows.Forms.Button();
            this.high_scores = new System.Windows.Forms.Button();
            this.exit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // title
            // 
            this.title.BackColor = System.Drawing.Color.Black;
            this.title.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.title.Font = new System.Drawing.Font("Bahnschrift Condensed", 72F);
            this.title.ForeColor = System.Drawing.Color.Red;
            this.title.Location = new System.Drawing.Point(334, 47);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(363, 116);
            this.title.TabIndex = 3;
            this.title.Text = "BATTLE ZONE";
            this.title.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // play
            // 
            this.play.BackColor = System.Drawing.Color.Red;
            this.play.Location = new System.Drawing.Point(444, 211);
            this.play.Name = "play";
            this.play.Size = new System.Drawing.Size(143, 60);
            this.play.TabIndex = 2;
            this.play.Text = "Start Game";
            this.play.UseVisualStyleBackColor = false;
            this.play.Click += new System.EventHandler(this.play_Click);
            // 
            // high_scores
            // 
            this.high_scores.BackColor = System.Drawing.Color.Red;
            this.high_scores.Location = new System.Drawing.Point(444, 281);
            this.high_scores.Name = "high_scores";
            this.high_scores.Size = new System.Drawing.Size(143, 60);
            this.high_scores.TabIndex = 1;
            this.high_scores.Text = "High Scores";
            this.high_scores.UseVisualStyleBackColor = false;
            this.high_scores.Click += new System.EventHandler(this.high_scores_Click);
            // 
            // exit
            // 
            this.exit.BackColor = System.Drawing.Color.Red;
            this.exit.Location = new System.Drawing.Point(444, 352);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(143, 60);
            this.exit.TabIndex = 0;
            this.exit.Text = "Exit";
            this.exit.UseVisualStyleBackColor = false;
            this.exit.Click += new System.EventHandler(this.exit_Click);
            // 
            // John_Stick
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1034, 506);
            this.Controls.Add(this.exit);
            this.Controls.Add(this.high_scores);
            this.Controls.Add(this.play);
            this.Controls.Add(this.title);
            this.Name = "John_Stick";
            this.Text = "Project Dingleberry: Battle Zone";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox title;
        private System.Windows.Forms.Button play;
        private System.Windows.Forms.Button high_scores;
        private System.Windows.Forms.Button exit;
    }
}
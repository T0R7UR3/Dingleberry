using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace RenderingTest
{
    public partial class Form1 : Form
    {
        List<Entity> entities = new List<Entity>();
        Entity player;
        List<Keys> keysDown = new List<Keys>();

        // Load the image ONCE here to prevent memory leaks
        Image monkeySprite = Image.FromFile(@"Images\Monkey Avatar.png");
        Image johnStick = Image.FromFile(@"Images\John Stick.png");

        public Form1()
        {
            InitializeComponent();

            // 1. Enable Double Buffering properly
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint |
                          ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();

            // 2. Initialize Entities using the refactored Entity class
            Entity occlusionTest = new Entity(monkeySprite);
            entities.Add(occlusionTest);

            player = new Entity(johnStick);
            entities.Add(player);

            frameTimer.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        // 3. The Timer only handles Game Logic (Movement)
        private void frameTimer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                // Pass the ClientSize so entities don't move off screen
                entities[i].moveEntity(this);
            }

            // Tell the form it needs to redraw itself next frame
            this.Invalidate();
        }

        // 4. OnPaint handles ALL drawing (Zero Flickering)
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // WinForms automatically clears the background for us because of DoubleBuffering
            // e.Graphics is the safe, buffered graphics object
            for (int i = 0; i < entities.Count; i++)
            {
                entities[i].drawEntity(e.Graphics);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (keysDown.Contains(e.KeyCode))
                return;

            keysDown.Add(e.KeyCode);

            switch (e.KeyCode)
            {
                case Keys.A: player.addVelocity(-10, 0); break;
                case Keys.D: player.addVelocity(10, 0); break;
                case Keys.W: player.addVelocity(0, -10); break;
                case Keys.S: player.addVelocity(0, 10); break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (keysDown.Contains(e.KeyCode))
                keysDown.Remove(e.KeyCode);

            switch (e.KeyCode)
            {
                case Keys.A: player.addVelocity(10, 0); break;
                case Keys.D: player.addVelocity(-10, 0); break;
                case Keys.W: player.addVelocity(0, 10); break;
                case Keys.S: player.addVelocity(0, -10); break;
            }
        }
    }
}
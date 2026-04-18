// Project: John Stick
// Course: IT145 Foundations of Application Development
// Authors: Murdock MacAskill, Beth, and Landen
// File: Program.cs
// Purpose: Starts the application and opens the main menu.
// Date: 04/17/2026

namespace Project_Dingleberry


{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new John_Stick());
        }
    }
}
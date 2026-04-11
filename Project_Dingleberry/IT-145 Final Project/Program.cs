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
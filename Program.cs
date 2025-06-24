using Planes;
namespace laba_6_oop
{
     class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>\ private List<Plane> planes = new();
         
        [STAThread]
        
        static void Main()
        {

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}
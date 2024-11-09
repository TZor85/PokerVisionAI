namespace PokerVisionAI.App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            AppDomain.CurrentDomain.UnhandledException += (sender, error) =>
            {
                System.Diagnostics.Debug.WriteLine(error.ExceptionObject.ToString());
            };

            MainPage = new MainPage();
        }
    }
}

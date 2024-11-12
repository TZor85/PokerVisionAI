
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

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = base.CreateWindow(activationState); 
            
            const int newWidth = 1734;
            const int newHeight = 1399;

            window.X = -7;
            window.Y = 0;

            window.Width = newWidth;
            window.Height = newHeight;

            return window;
        }
    }
}

using Microsoft.Extensions.DependencyInjection;

namespace SeattleAviation
{
    public partial class App : Application
    {
        public App()
        {


            InitializeComponent();

            
        MainPage = new AppShell();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            // 2. Create the window with MainPage
            var window = new Window(MainPage);
            // 3. Remove the title text from the title bar
            window.Title = "";
            return window;
        }
    }
}
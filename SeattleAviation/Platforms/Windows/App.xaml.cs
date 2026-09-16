using System;
using System.IO;
using System.Threading;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using WinRT.Interop;

namespace SeattleAviation.WinUI;

public partial class App : MauiWinUIApplication
{
    private static Mutex? _mutex;

    public App()
    {
        // Single Instance Lock
        const string mutexName = "Global\\SeattleAviation_SingleInstance_Mutex";
        _mutex = new Mutex(true, mutexName, out bool createdNew);

        if (!createdNew)
        {
            Environment.Exit(0);
            return;
        }

        InitializeComponent();

        var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SeattleAviation", "WebView2");
        Directory.CreateDirectory(folder);
        Environment.SetEnvironmentVariable("WEBVIEW2_USER_DATA_FOLDER", folder);
    }

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

    protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
    {
        base.OnLaunched(args);
        Microsoft.Maui.Controls.Application.Current?.Dispatcher.Dispatch(() =>
        {
            var window = Microsoft.Maui.Controls.Application.Current?.Windows?.FirstOrDefault()?.Handler?.PlatformView as Microsoft.UI.Xaml.Window;
            if (window != null)
            {
                var handle = WindowNative.GetWindowHandle(window);
                var id = Win32Interop.GetWindowIdFromWindow(handle);
                var appWindow = AppWindow.GetFromWindowId(id);
                if (appWindow != null)
                {
                    // SET TASKBAR ICON & WINDOW CORNER ICON
                    string iconPath = System.IO.Path.Combine(AppContext.BaseDirectory, "appicon.ico");
                    if (System.IO.File.Exists(iconPath))
                    {
                        appWindow.SetIcon(iconPath);
                    }
                    appWindow.TitleBar.ExtendsContentIntoTitleBar = true;
                    appWindow.TitleBar.ButtonBackgroundColor = Microsoft.UI.Colors.Transparent;
                    appWindow.TitleBar.ButtonInactiveBackgroundColor = Microsoft.UI.Colors.Transparent;
                    var presenter = appWindow.Presenter as OverlappedPresenter;
                    presenter?.Maximize();
                }
            }
        });
    }
}
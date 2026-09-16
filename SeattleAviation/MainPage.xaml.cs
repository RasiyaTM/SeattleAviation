namespace SeattleAviation;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        MyWebView.Source = "https://www.aviomas.com/";
    }
}
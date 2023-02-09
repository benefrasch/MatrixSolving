namespace MatrixSolving.Views;

public partial class AboutPage : ContentPage
{
	public AboutPage()
	{
		InitializeComponent();
		VersionLabel.Text = AppInfo.Current.VersionString;

    }

    private async void Github_Tapped(object sender, TappedEventArgs e)
    {
        await Browser.Default.OpenAsync("https://github.com/benefrasch/MatrixSolving", BrowserLaunchMode.SystemPreferred);
    }
}
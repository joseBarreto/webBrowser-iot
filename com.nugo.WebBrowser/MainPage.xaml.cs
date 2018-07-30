using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace com.nugo.WebBrowser
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        DispatcherTimer dispatcherTimer;

        public MainPage()
        {
            this.InitializeComponent();

            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(1, 0, 0);
            dispatcherTimer.Start();


            webView.LoadCompleted += WebBrowser_LoadCompleted;
            webView.Loading += WebBrowser_Loading;
            webView.NavigationStarting += WebView_NavigationStarting;

            InitUrl();
        }

        private async void InitUrl()
        {
            Windows.Storage.StorageFile sampleFile = await Windows.Storage.KnownFolders.DocumentsLibrary.GetFileAsync("url.txt");
            string text = await Windows.Storage.FileIO.ReadTextAsync(sampleFile);
            webView.Navigate(new Uri(text));
        }

        private void DispatcherTimer_Tick(object sender, object e)
        {
            webView.Refresh();
        }

        private void WebView_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            _progressBar.Visibility = Visibility.Visible;
        }

        private void WebBrowser_Loading(FrameworkElement sender, object args)
        {
            _progressBar.Visibility = Visibility.Visible;
        }

        private void WebBrowser_LoadCompleted(object sender, NavigationEventArgs e)
        {
            _progressBar.Visibility = Visibility.Collapsed;
        }
    }
}

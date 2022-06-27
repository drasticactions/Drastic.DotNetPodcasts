using Drastic.DotNetPodcasts.ViewModels;
using DrasticMedia.Core.Model;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

using Windows.UI.Core;
using WinUIEx;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Drastic.DotNetPodcasts.WinUI
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : WinUIEx.WindowEx
    {
        private PodcastPlayerViewModel vm;

        public MainWindow()
        {
            this.InitializeComponent();
            this.ExtendsContentIntoTitleBar = true;
            this.SetTitleBar(this.AppTitleBar);
            var result = this.NavigationFrame.Navigate(typeof(PodcastShowsPage));
            this.vm = Ioc.Default.ResolveWith<PodcastPlayerViewModel>();
        }

        public PodcastPlayerViewModel PlayerViewModel => this.vm;

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationFrame.CanGoBack)
            {
                this.NavigationFrame.GoBack();
            }
        }
    }
}

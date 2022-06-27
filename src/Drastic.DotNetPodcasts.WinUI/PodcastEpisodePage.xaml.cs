using DrasticMedia.Core.Model;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace Drastic.DotNetPodcasts.WinUI
{
    public partial class PodcastEpisodePage : Page
    {
        public PodcastEpisodePage()
        {
            this.InitializeComponent();
            this.PodcastEpisodeItemViewModel = Ioc.Default.ResolveWith<PodcastEpisodeItemViewModel>();
            this.DispatcherQueue.TryEnqueue(async () => await this.PodcastEpisodeItemViewModel.OnLoad());
        }

        /// <summary>
        /// Gets the PodcastEpisodeItemViewModel.
        /// </summary>
        public PodcastEpisodeItemViewModel? PodcastEpisodeItemViewModel { get; private set; }

        protected override void OnNavigatedTo(Microsoft.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.Parameter is PodcastEpisodeItem item && this.PodcastEpisodeItemViewModel is not null)
            {
                this.PodcastEpisodeItemViewModel.PodcastEpisode = item;
            }
        }
    }
}
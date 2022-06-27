// <copyright file="PodcastShowPage.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DrasticMedia.Core.Model;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace Drastic.DotNetPodcasts.WinUI
{
    public partial class PodcastShowPage : Page
    {
        public PodcastShowPage()
        {
            this.InitializeComponent();
            this.PodcastShowItemViewModel = Ioc.Default.ResolveWith<PodcastShowItemViewModel>();
            this.DispatcherQueue.TryEnqueue(async () => await this.PodcastShowItemViewModel.OnLoad());
        }

        /// <summary>
        /// Gets the PodcastShowItemViewModel.
        /// </summary>
        public PodcastShowItemViewModel? PodcastShowItemViewModel { get; private set; }

        /// <inheritdoc/>
        protected override void OnNavigatedTo(Microsoft.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is PodcastShowItem item && this.PodcastShowItemViewModel is not null)
            {
                this.PodcastShowItemViewModel.Podcast = item;
            }
        }

        private void EpisodeList_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is PodcastEpisodeItem item && this.PodcastShowItemViewModel is not null)
            {
                this.PodcastShowItemViewModel.PlayPodcastEpisodeAsync(item, true).FireAndForgetSafeAsync(this.PodcastShowItemViewModel.ErrorHandler);
                //this.Frame.Navigate(typeof(PodcastEpisodePage), item);
            }
        }
    }
}
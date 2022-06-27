// <copyright file="PodcastShowsPage.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DrasticMedia.Core.Model;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace Drastic.DotNetPodcasts.WinUI
{
    public partial class PodcastShowsPage : Page
    {
        public PodcastShowsPage()
        {
            this.InitializeComponent();
            this.PodcastShowItemListViewModel = Ioc.Default.ResolveWith<PodcastShowItemListViewModel>();
            this.DispatcherQueue.TryEnqueue(async () => await this.PodcastShowItemListViewModel.OnLoad());
        }

        /// <summary>
        /// Gets the PodcastShowItemListViewModel.
        /// </summary>
        public PodcastShowItemListViewModel? PodcastShowItemListViewModel { get; private set; }

        private void PodcastShowGrid_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (this.PodcastShowItemListViewModel is null)
            {
                return;
            }

            if (e.ClickedItem is PodcastShowItem item)
            {
                this.Frame.Navigate(typeof(PodcastShowPage), item);
            }
        }
    }
}
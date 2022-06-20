// <copyright file="PodcastShowItemListViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace Drastic.DotNetPodcasts
{
    /// <summary>
    /// Podcast Show Item List View Model.
    /// </summary>
    public class PodcastShowItemListViewModel : BaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PodcastShowItemListViewModel"/> class.
        /// </summary>
        /// <param name="services"><see cref="IServiceProvider"/>/</param>
        public PodcastShowItemListViewModel(IServiceProvider services)
            : base(services)
        {
            this.Library.NewMediaItemAdded += this.Library_NewMediaItemAdded;
            this.Library.UpdateMediaItemAdded += this.Library_UpdateMediaItemAdded;
            this.Library.NewMediaItemError += this.Library_NewMediaItemError;
            this.Library.RemoveMediaItem += this.Library_RemoveMediaItem;
        }

        private void Library_RemoveMediaItem(object? sender, DrasticMedia.Core.Library.RemoveMediaItemEventArgs e)
        {
        }

        private void Library_NewMediaItemError(object? sender, DrasticMedia.Core.Library.NewMediaItemErrorEventArgs e)
        {
        }

        private void Library_UpdateMediaItemAdded(object? sender, DrasticMedia.Core.Library.UpdateMediaItemEventArgs e)
        {
        }

        private void Library_NewMediaItemAdded(object? sender, DrasticMedia.Core.Library.NewMediaItemEventArgs e)
        {
        }
    }
}

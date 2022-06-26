// <copyright file="PodcastShowItemViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using DrasticMedia.Core.Model;

namespace Drastic.DotNetPodcasts
{
    /// <summary>
    /// Podcast Show Item View Model.
    /// </summary>
    public class PodcastShowItemViewModel : BaseViewModel
    {
        private PodcastShowItem? podcast;

        /// <summary>
        /// Initializes a new instance of the <see cref="PodcastShowItemViewModel"/> class.
        /// </summary>
        /// <param name="services"><see cref="IServiceProvider"/>/</param>
        public PodcastShowItemViewModel(IServiceProvider services)
            : base(services)
        {
            this.Library.NewMediaItemAdded += this.Library_NewMediaItemAdded;
            this.Library.UpdateMediaItemAdded += this.Library_UpdateMediaItemAdded;
            this.Library.NewMediaItemError += this.Library_NewMediaItemError;
            this.Library.RemoveMediaItem += this.Library_RemoveMediaItem;
            this.SelectPodcastCommand = new AsyncCommand<PodcastShowItem>(this.SelectPodcastAsync, (item) => item is not null, this.ErrorHandler);
        }

        /// <summary>
        /// Gets the SelectPodcastCommand Command.
        /// </summary>
        public AsyncCommand<PodcastShowItem> SelectPodcastCommand { get; private set; }

        /// <summary>
        /// Gets or sets the selected podcast.
        /// </summary>
        public PodcastShowItem? Podcast
        {
            get { return this.podcast; }
            set { this.SetProperty(ref this.podcast, value); }
        }

        private void Library_RemoveMediaItem(object? sender, DrasticMedia.Core.Library.RemoveMediaItemEventArgs e)
        {
        }

        private void Library_NewMediaItemError(object? sender, DrasticMedia.Core.Library.NewMediaItemErrorEventArgs e)
        {
            // TODO: Show Error.
        }

        private void Library_UpdateMediaItemAdded(object? sender, DrasticMedia.Core.Library.UpdateMediaItemEventArgs e)
        {
        }

        private void Library_NewMediaItemAdded(object? sender, DrasticMedia.Core.Library.NewMediaItemEventArgs e)
        {
        }

        private Task SelectPodcastAsync(PodcastShowItem item)
        {
            ArgumentNullException.ThrowIfNull(item);
            this.Podcast = item;
            return Task.CompletedTask;
        }
    }
}
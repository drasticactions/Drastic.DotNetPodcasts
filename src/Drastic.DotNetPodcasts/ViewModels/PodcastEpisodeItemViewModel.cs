// <copyright file="PodcastEpisodeItemViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using DrasticMedia.Core.Model;

namespace Drastic.DotNetPodcasts
{
    public class PodcastEpisodeItemViewModel : BaseViewModel
    {
        private PodcastEpisodeItem? podcastEpisode;

        /// <summary>
        /// Initializes a new instance of the <see cref="PodcastEpisodeItemViewModel"/> class.
        /// </summary>
        /// <param name="services"><see cref="IServiceProvider"/>/</param>
        public PodcastEpisodeItemViewModel(IServiceProvider services)
            : base(services)
        {
            this.Library.NewMediaItemAdded += this.Library_NewMediaItemAdded;
            this.Library.UpdateMediaItemAdded += this.Library_UpdateMediaItemAdded;
            this.Library.NewMediaItemError += this.Library_NewMediaItemError;
            this.Library.RemoveMediaItem += this.Library_RemoveMediaItem;
            this.SelectPodcastEpisodeCommand = new AsyncCommand<PodcastEpisodeItem>(this.SelectPodcastEpisodeAsync, (item) => item is not null, this.ErrorHandler);
        }

        /// <summary>
        /// Gets the SelectPodcastEpisodeCommand Command.
        /// </summary>
        public AsyncCommand<PodcastEpisodeItem> SelectPodcastEpisodeCommand { get; private set; }

        /// <summary>
        /// Gets or sets the selected podcast Episode.
        /// </summary>
        public PodcastEpisodeItem? PodcastEpisode
        {
            get { return this.podcastEpisode; }
            set { this.SetProperty(ref this.podcastEpisode, value); }
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

        private Task SelectPodcastEpisodeAsync(PodcastEpisodeItem item)
        {
            ArgumentNullException.ThrowIfNull(item);
            this.PodcastEpisode = item;
            return Task.CompletedTask;
        }
    }
}
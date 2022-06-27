// <copyright file="PodcastShowItemListViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Collections.ObjectModel;
using DrasticMedia.Core.Model;

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
            this.SelectPodcastShowCommand = new AsyncCommand<PodcastShowItem>(this.SelectPodcastShow, null, this.ErrorHandler);
        }

        /// <summary>
        /// Gets the list of podcasts.
        /// </summary>
        public ObservableCollection<PodcastShowItem> Shows { get; } = new ObservableCollection<PodcastShowItem>();

        /// <summary>
        /// Gets the SelectPodcastShowCommand Command.
        /// </summary>
        public AsyncCommand<PodcastShowItem> SelectPodcastShowCommand { get; private set; }

        /// <inheritdoc/>
        public override async Task OnLoad()
        {
            await base.OnLoad();
            var shows = await this.Library.FetchPodcastsAsync();
            foreach (var show in shows)
            {
                this.Shows.Add(show);
            }

            if (!shows.Any())
            {
                foreach (var item in DebugHelpers.DefaultPodcasts)
                {
                    await this.AddOrUpdatePodcastFromUriCommand.ExecuteAsync(new Uri(item));
                }
            }
        }

        private void Library_RemoveMediaItem(object? sender, DrasticMedia.Core.Library.RemoveMediaItemEventArgs e)
        {
            if (e.MediaItem is PodcastShowItem item)
            {
                this.Shows.Remove(item);
            }
        }

        private void Library_NewMediaItemError(object? sender, DrasticMedia.Core.Library.NewMediaItemErrorEventArgs e)
        {
            // TODO: Show Error.
        }

        private void Library_UpdateMediaItemAdded(object? sender, DrasticMedia.Core.Library.UpdateMediaItemEventArgs e)
        {
            if (e.MediaItem is PodcastShowItem item)
            {
                var pod = this.Shows.FirstOrDefault(n => n.Id == item.Id);
                if (pod is null)
                {
                    return;
                }

                this.Shows.Insert(this.Shows.IndexOf(pod), item);
                this.Shows.Remove(pod);
            }
        }

        private void Library_NewMediaItemAdded(object? sender, DrasticMedia.Core.Library.NewMediaItemEventArgs e)
        {
            if (e.MediaItem is PodcastShowItem item)
            {
                this.Dispatcher.Dispatch(() => this.Shows.Add(item));
            }
        }

        private Task SelectPodcastShow(PodcastShowItem item)
        {
            this.Navigate(PageTypes.PodcastShow, item);
            return Task.CompletedTask;
        }
    }
}

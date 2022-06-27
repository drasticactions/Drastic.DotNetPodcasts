// <copyright file="PodcastPlayerViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DrasticMedia.Core;
using DrasticMedia.Core.Services;

namespace Drastic.DotNetPodcasts.ViewModels
{
    /// <summary>
    /// Podcast Player View Model.
    /// </summary>
    public class PodcastPlayerViewModel : BaseViewModel
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="PodcastPlayerViewModel"/> class.
        /// </summary>
        /// <param name="services"><see cref="IServiceProvider"/>.</param>
        public PodcastPlayerViewModel(IServiceProvider services)
            : base(services)
        {
            this.MediaService.MediaChanged += (object? sender, EventArgs e) => this.RaiseCanExecuteChanged();
            this.MediaService.PositionChanged += (object? sender, MediaPlayerPositionChangedEventArgs e) => this.RaiseCanExecuteChanged();
        }
    }
}

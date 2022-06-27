// <copyright file="BaseViewModel.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.ComponentModel;
using System.Runtime.CompilerServices;
using DrasticMedia.Core.Library;
using DrasticMedia.Core.Model;
using DrasticMedia.Core.Services;
using DrasticMedia.Podcast.Library;

namespace Drastic.DotNetPodcasts
{
    /// <summary>
    /// Base View Model.
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        private IMediaService mediaService;
        private bool isBusy;
        private string title = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseViewModel"/> class.
        /// </summary>
        /// <param name="services"><see cref="IServiceProvider"/>.</param>
        public BaseViewModel(IServiceProvider services)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));
            this.Services = services;
            this.ErrorHandler = services.GetService(typeof(IErrorHandlerService)) as IErrorHandlerService ?? throw new NullReferenceException(nameof(IErrorHandlerService));
            this.Dispatcher = services.GetService(typeof(IAppDispatcher)) as IAppDispatcher ?? throw new NullReferenceException(nameof(IAppDispatcher));
            this.Library = services.GetService(typeof(IPodcastLibrary)) as IPodcastLibrary ?? throw new NullReferenceException(nameof(IPodcastLibrary));
            this.mediaService = services.GetService(typeof(IMediaService)) as IMediaService ?? throw new NullReferenceException(nameof(IMediaService));
            this.AddOrUpdatePodcastFromUriCommand = new AsyncCommand<Uri>(this.Library.AddOrUpdatePodcastFromUri, this.IsValidUri, this.ErrorHandler);
        }

        /// <inheritdoc/>
        public event PropertyChangedEventHandler? PropertyChanged;

        public event EventHandler<NavigationEventArgs>? OnNavigationRaised;

        /// <summary>
        /// Gets the media service.
        /// </summary>
        public IMediaService MediaService => this.mediaService;

        /// <summary>
        /// Gets or sets a value indicating whether the VM is busy.
        /// </summary>
        public bool IsBusy
        {
            get { return this.isBusy; }
            set { this.SetProperty(ref this.isBusy, value); }
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title
        {
            get { return this.title; }
            set { this.SetProperty(ref this.title, value); }
        }

        /// <summary>
        /// Gets the AddOrUpdatePodcastFromUri Command.
        /// </summary>
        public AsyncCommand<Uri> AddOrUpdatePodcastFromUriCommand { get; private set; }

        /// <summary>
        /// Gets the <see cref="PodcastLibrary"/>.
        /// </summary>
        internal IPodcastLibrary Library { get; }

        /// <summary>
        /// Gets the <see cref="IServiceProvider"/>.
        /// </summary>
        internal IServiceProvider Services { get; }

        /// <summary>
        /// Gets the Dispatcher.
        /// </summary>
        internal IAppDispatcher Dispatcher { get; }

        /// <summary>
        /// Gets the Error Handler.
        /// </summary>
        public IErrorHandlerService ErrorHandler { get; }

        /// <summary>
        /// Called on VM Load.
        /// </summary>
        /// <returns><see cref="Task"/>.</returns>
        public virtual Task OnLoad()
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Called when wanting to raise a Command Can Execute.
        /// </summary>
        public virtual void RaiseCanExecuteChanged()
        {
            this.OnPropertyChanged(nameof(this.MediaService));
            this.OnPropertyChanged("MediaService.CurrentMedia");
        }

        public async Task PlayPodcastEpisodeAsync(PodcastEpisodeItem item, bool autoPlay = false)
        {
            ArgumentNullException.ThrowIfNull(item);
            this.MediaService.CurrentMedia = item;
            if (autoPlay)
            {
                await this.mediaService.PlayAsync();
            }

            this.OnPropertyChanged(nameof(this.MediaService));
        }

        internal void Navigate(PageTypes page, object parameter)
        {
            this.OnNavigationRaised?.Invoke(this, new NavigationEventArgs(page, parameter));
        }

#pragma warning disable SA1600 // Elements should be documented
        protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = "", Action? onChanged = null)
#pragma warning restore SA1600 // Elements should be documented
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
            {
                return false;
            }

            backingStore = value;
            onChanged?.Invoke();
            this.OnPropertyChanged(propertyName);
            this.RaiseCanExecuteChanged();
            return true;
        }

        /// <summary>
        /// On Property Changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            this.Dispatcher?.Dispatch(() =>
            {
                var changed = this.PropertyChanged;
                if (changed == null)
                {
                    return;
                }

                changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
            });
        }

        private bool IsValidUri(Uri? uri)
        {
            // TODO: Test URI to validate if it's actually a podcast feed.
            if (uri is null)
            {
                return false;
            }

            return true;
        }
    }
}

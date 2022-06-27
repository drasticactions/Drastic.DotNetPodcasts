// <copyright file="App.xaml.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Drastic.DotNetPodcasts.ViewModels;
using DrasticMedia.Core.Database;
using DrasticMedia.Core.Library;
using DrasticMedia.Core.Platform;
using DrasticMedia.Core.Services;
using DrasticMedia.LiteDB.Database;
using DrasticMedia.Podcast.Library;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Microsoft.UI.Xaml;

namespace Drastic.DotNetPodcasts.WinUI
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            var dispatcherQueue = Microsoft.UI.Dispatching.DispatcherQueue.GetForCurrentThread();
            Ioc.Default.ConfigureServices(
                new ServiceCollection()
                .AddSingleton<ILogger, ConsoleLogger>()
                .AddSingleton<IPlatformSettings, WindowsPlatformSettings>()
                .AddSingleton<IAppDispatcher>(new AppDispatcher(dispatcherQueue))
                .AddSingleton<IErrorHandlerService, LoggerErrorHandlerService>()
                .AddSingleton<IPodcastService, PodcastService>()
                .AddSingleton<IPodcastDatabase, PodcastDatabase>()
                .AddSingleton<IPodcastLibrary, PodcastLibrary>()
                .AddSingleton<IMediaService, NativeMediaService>()
                .AddTransient<PodcastShowItemListViewModel>()
                .AddTransient<PodcastShowItemViewModel>()
                .AddTransient<PodcastEpisodeItemViewModel>()
                .AddTransient<PodcastPlayerViewModel>()
                .BuildServiceProvider());
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="args">Details about the launch request and process.</param>
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            this.m_window = new MainWindow();
            this.m_window.Activate();
        }

        private Window? m_window;
    }
}

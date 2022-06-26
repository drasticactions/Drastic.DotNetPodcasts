// <copyright file="ViewModelTest.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using DrasticMedia.Core.Database;
using DrasticMedia.Core.Library;
using DrasticMedia.Core.Platform;
using DrasticMedia.Core.Services;
using DrasticMedia.LiteDB.Database;
using DrasticMedia.Podcast.Library;
using Microsoft.Extensions.DependencyInjection;

namespace Drastic.DotNetPodcasts.Test
{
    [TestClass]
    public class ViewModelTest
    {
        public ViewModelTest()
        {
        }

        /// <summary>
        /// Can parse Podcast Feeds.
        /// </summary>
        /// <param name="feeduri">The feed uri.</param>
        [DataRow(@"https://feeds.fireside.fm/mergeconflict/rss")]
        [DataTestMethod]
        public async Task AddUpdateRemovePodcast(string feeduri)
        {
            var service = this.BuildServiceProvider();
            var vm = service.GetService<PodcastShowItemListViewModel>();
            var uri = new Uri(feeduri);
            await vm.AddOrUpdatePodcastFromUriCommand.ExecuteAsync(uri);
            var podcasts = await vm.Library.FetchPodcastsAsync();
            foreach ( var podcast in podcasts)
            {
                Assert.IsTrue(podcast.Episodes.Any());
            }
        }

        private ServiceProvider BuildServiceProvider()
        {
            var logger = new ConsoleLogger();
            var db = new PodcastDatabase(ExtensionHelpers.PodcastDatabase());
            var podcastService = new PodcastService(logger);
            var library = new PodcastLibrary(podcastService, db);
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IPlatformSettings, MockPlatformSettings>();
            serviceCollection.AddSingleton<IPodcastDatabase>(db);
            serviceCollection.AddSingleton<IPodcastService>(podcastService);
            serviceCollection.AddSingleton<IPodcastLibrary>(library);
            serviceCollection.AddSingleton<IErrorHandlerService, MockErrorHandlerService>();
            serviceCollection.AddSingleton<IAppDispatcher, MockAppDispatcher>();
            serviceCollection.AddTransient<PodcastShowItemListViewModel>();
            var service = serviceCollection.BuildServiceProvider();
            return service;
        }
    }
}
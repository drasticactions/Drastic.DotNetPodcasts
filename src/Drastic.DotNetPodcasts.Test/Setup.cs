// <copyright file="Setup.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;
using Microsoft.Extensions.Logging;

namespace Drastic.DotNetPodcasts.Test
{
    public class NullScope : IDisposable
    {
        public static NullScope Instance { get; } = new NullScope();

        private NullScope() { }

        public void Dispose() { }
    }

    public class ConsoleLogger : ILogger
    {
        public IDisposable BeginScope<TState>(TState state) => NullScope.Instance;

        public bool IsEnabled(LogLevel logLevel) => false;

        public void Log<TState>(LogLevel logLevel,
                                EventId eventId,
                                TState state,
                                Exception exception,
                                Func<TState, Exception, string> formatter)
        {
            string message = formatter(state, exception);
            Console.WriteLine(message);
        }
    }

    public class ConsoleLogger<T> : ILogger<T>
    {
        public IDisposable BeginScope<TState>(TState state) => NullScope.Instance;

        public bool IsEnabled(LogLevel logLevel) => false;

        public void Log<TState>(LogLevel logLevel,
                                EventId eventId,
                                TState state,
                                Exception exception,
                                Func<TState, Exception, string> formatter)
        {
            string message = formatter(state, exception);
            Console.WriteLine(message);
        }
    }

    /// <summary>
    /// Setup.
    /// </summary>
    [TestClass]
    public class Setup
    {
        /// <summary>
        /// Setup tests when the assembly is loaded.
        /// </summary>
        /// <param name="context">Test Context.</param>
        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            DatabaseSetup();
        }

        private static void DatabaseSetup()
        {
            if (File.Exists(ExtensionHelpers.PodcastDatabase()))
            {
                File.Delete(ExtensionHelpers.PodcastDatabase());
            }

            if (File.Exists(ExtensionHelpers.PodcastDatabaseLog()))
            {
                File.Delete(ExtensionHelpers.PodcastDatabaseLog());
            }
        }
    }

    public static class ExtensionHelpers
    {
        public static string PodcastDatabase() => GetPath("podcast.test.db");

        public static string PodcastDatabaseLog() => GetPath("podcast.test-log.db");

        public static string GetPath(string path)
        {
            var assemblyPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            return new DirectoryInfo(System.IO.Path.Join(assemblyPath, path)).FullName;
        }
    }
}
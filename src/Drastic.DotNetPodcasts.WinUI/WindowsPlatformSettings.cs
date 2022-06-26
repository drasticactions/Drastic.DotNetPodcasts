// <copyright file="WindowsPlatformSettings.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using DrasticMedia.Core.Model;
using DrasticMedia.Core.Platform;
using System.Reflection;

namespace Drastic.DotNetPodcasts.WinUI
{
    /// <summary>
    /// Windows Platform Settings.
    /// </summary>
    public class WindowsPlatformSettings : IPlatformSettings
    {
        /// <inheritdoc/>
        public string DatabasePath => this.GetLocalPath();

        /// <inheritdoc/>
        public bool IsDarkTheme => false;

        /// <inheritdoc/>
        public string MetadataPath => string.Empty;

        /// <inheritdoc/>
        public List<MediaFolder> GetDefaultMediaFolders() => new List<MediaFolder>();

        /// <inheritdoc/>
        public bool IsFileAvailable(string path) => System.IO.File.Exists(path);

        private string GetLocalPath()
        {
            var location = Assembly.GetExecutingAssembly()?.Location ?? string.Empty;
            if (string.IsNullOrEmpty(location))
            {
                return string.Empty;
            }

            return Path.GetDirectoryName(location) ?? string.Empty;
        }
    }
}

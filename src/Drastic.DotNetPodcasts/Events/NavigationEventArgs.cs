// <copyright file="NavigationEventArgs.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace Drastic.DotNetPodcasts
{
    public class NavigationEventArgs : EventArgs
    {
        public NavigationEventArgs(PageTypes page, object? parameter)
        {
            this.Page = page;
            this.Parameter = parameter;
        }

        public PageTypes Page { get; }

        public object? Parameter { get; }
    }
}

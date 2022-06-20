// <copyright file="IAppDispatcher.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

namespace Drastic.DotNetPodcasts
{
    public interface IAppDispatcher
    {
        bool Dispatch(Action action);
    }
}

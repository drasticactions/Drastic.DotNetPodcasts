// <copyright file="MockAppDispatcher.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;

namespace Drastic.DotNetPodcasts.Test
{
    public class MockAppDispatcher : IAppDispatcher
    {
        public bool Dispatch(Action action)
        {
            action.Invoke();
            return true;
        }
    }
}
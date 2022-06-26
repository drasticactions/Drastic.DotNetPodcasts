// <copyright file="MockErrorHandlerService.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System;

namespace Drastic.DotNetPodcasts.Test
{
    public class MockErrorHandlerService : IErrorHandlerService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MockErrorHandlerService"/> class.
        /// </summary>
        public MockErrorHandlerService()
        {
        }

        /// <inheritdoc/>
        public event EventHandler<ErrorHandlerEventArgs> OnError;

        /// <inheritdoc/>
        public void HandleError(Exception ex)
        {
            this.OnError?.Invoke(this, new ErrorHandlerEventArgs(ex));
        }
    }
}
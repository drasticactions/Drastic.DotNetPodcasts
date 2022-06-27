// <copyright file="MillisecondsStringConverter.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Microsoft.UI.Xaml.Data;

namespace Drastic.DotNetPodcasts.WinUI
{
    /// <summary>
    /// Milliseconds String Converter.
    /// </summary>
    public class MillisecondsStringConverter : IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type? targetType, object? parameter, string? language)
        {
            return StringsHelper.MillisecondsToString(System.Convert.ToInt64(value));
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}

// <copyright file="PositionToFixedPointConverter.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Microsoft.UI.Xaml.Data;

namespace Drastic.DotNetPodcasts.WinUI
{
    /// <summary>
    /// Position to Fixed Point Converter.
    /// </summary>
    public class PositionToFixedPointConverter : IValueConverter
    {
        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var floatingPoint = (float)value;
            if (float.IsNaN(floatingPoint))
            {
                return 0;
            }

            var point = floatingPoint * 500;
            return (int)point;
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var fixedPoint = (double)value;
            return (float)(fixedPoint / 500.0f);
        }
    }
}

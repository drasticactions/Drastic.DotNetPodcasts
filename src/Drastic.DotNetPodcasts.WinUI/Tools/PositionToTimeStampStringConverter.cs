// <copyright file="PositionToTimeStampStringConverter.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml;

namespace Drastic.DotNetPodcasts.WinUI
{
    /// <summary>
    /// Position to TimeStamp String Converter.
    /// </summary>
    public class PositionToTimeStampStringConverter : DependencyObject, IValueConverter
    {
        public double DurationValue
        {
            get { return (double)GetValue(DurationValueProperty); }
            set { SetValue(DurationValueProperty, value); }
        }

        public static readonly DependencyProperty DurationValueProperty =
            DependencyProperty.Register("DurationValue", typeof(double), typeof(PositionToTimeStampStringConverter), new PropertyMetadata(0d));

        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var maxPos = 500;
            var pos = 0.0;
            var totalMs = 0.0;

            if (value is double player)
            {
                pos = player;
                totalMs = this.DurationValue;

                var milliSeconds = (long)((pos / maxPos) * totalMs);
                return new MillisecondsStringConverter().Convert(milliSeconds, null, null, null);
            }

            return string.Empty;
        }

        /// <inheritdoc/>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}

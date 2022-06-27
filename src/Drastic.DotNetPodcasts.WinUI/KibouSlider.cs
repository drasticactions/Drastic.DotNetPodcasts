// <copyright file="KibouSlider.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;

namespace Drastic.DotNetPodcasts.WinUI
{
    /// <summary>
    /// Kibou Slider.
    /// We don't update the slider until we're done changing its value.
    /// This way, we don't have stuttering audio/video playback.
    /// </summary>
    public class KibouSlider : Slider
    {
        private Binding? SupressedBinding { get; set; }

        /// <inheritdoc/>
        protected override void OnKeyDown(KeyRoutedEventArgs e)
        {
            base.OnKeyDown(e);
            var expression = this.GetBindingExpression(ValueProperty);
            if (expression != null)
            {
                this.SupressedBinding = expression.ParentBinding;
                var value = this.Value;
                this.ClearValue(ValueProperty);
                this.SetValue(ValueProperty, value);
            }
        }

        /// <inheritdoc/>
        protected override void OnKeyUp(KeyRoutedEventArgs e)
        {
            if (this.SupressedBinding != null)
            {
                var value = this.Value;
                this.SetBinding(ValueProperty, this.SupressedBinding);
                this.SetValue(ValueProperty, value);
                this.SupressedBinding = null;
            }

            base.OnKeyUp(e);
        }

        /// <inheritdoc/>
        protected override void OnManipulationStarted(ManipulationStartedRoutedEventArgs e)
        {
            base.OnManipulationStarted(e);
            var expression = this.GetBindingExpression(ValueProperty);
            if (expression != null)
            {
                this.SupressedBinding = expression.ParentBinding;
                var value = this.Value;
                this.ClearValue(ValueProperty);
                this.SetValue(ValueProperty, value);
            }
        }

        /// <inheritdoc/>
        protected override void OnManipulationCompleted(ManipulationCompletedRoutedEventArgs e)
        {
            if (this.SupressedBinding != null)
            {
                var value = this.Value;
                this.SetBinding(ValueProperty, this.SupressedBinding);
                this.SetValue(ValueProperty, value);
                this.SupressedBinding = null;
            }

            base.OnManipulationCompleted(e);
        }
    }
}

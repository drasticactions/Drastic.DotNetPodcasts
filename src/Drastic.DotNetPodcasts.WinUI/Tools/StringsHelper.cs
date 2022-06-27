// <copyright file="StringsHelper.cs" company="Drastic Actions">
// Copyright (c) Drastic Actions. All rights reserved.
// </copyright>

using System.Collections;
using System.Globalization;
using System.Text;

namespace Drastic.DotNetPodcasts.WinUI
{
    public static class StringsHelper
    {
        public static string MillisecondsToString(long value)
        {
            double milliseconds = value;

            TimeSpan time = TimeSpan.FromMilliseconds(milliseconds);
            if (time.Hours > 0)
            {
                return string.Format(CultureInfo.CurrentCulture, "{0:hh\\:mm\\:ss}", time);
            }
            else
            {
                return string.Format(CultureInfo.CurrentCulture, "{0:mm\\:ss}", time);
            }
        }

        public static string SecondsToString(double value)
        {
            long milliseconds = (long)value * 1000;
            return MillisecondsToString(milliseconds);
        }

        public static string ExceptionToString(Exception exception)
        {
            bool inner = false;
            var strBuilder = new StringBuilder();

#pragma warning disable CS8600 // Null リテラルまたは Null の可能性がある値を Null 非許容型に変換しています。
            for (Exception ex = exception; ex != null; ex = ex.InnerException)
            {
                strBuilder.AppendLine(inner ? "InnerException" : "Exception:");
                strBuilder.AppendLine("Message: ");
                strBuilder.AppendLine(ex.Message);
                strBuilder.AppendLine("HelpLink: ");
                strBuilder.AppendLine(ex.HelpLink);
                strBuilder.AppendLine("HResult: ");
                strBuilder.AppendLine(ex.HResult.ToString(CultureInfo.InvariantCulture));
                strBuilder.AppendLine("Source: ");
                strBuilder.AppendLine(ex.Source);
                strBuilder.AppendLine("StackTrace: ");
                strBuilder.AppendLine(ex.StackTrace);
                strBuilder.AppendLine(string.Empty);

                if (exception.Data != null && exception.Data.Count > 0)
                {
                    strBuilder.AppendLine("Additional Data: ");
                    foreach (DictionaryEntry entry in exception.Data)
                    {
                        strBuilder.AppendLine(entry.Key + ";" + entry.Value);
                    }

                    strBuilder.AppendLine(string.Empty);
                }

                inner = true;
            }
#pragma warning restore CS8600 // Null リテラルまたは Null の可能性がある値を Null 非許容型に変換しています。

            return strBuilder.ToString();
        }

        // not available in the currently used .NET Core
        // see https://github.com/dotnet/corefx/blob/472c3d95749910ba600df296bbf01d78d7c4f6d8/src/Common/src/CoreLib/System/Globalization/TextInfo.cs#L441
        public static string ToTitleCase(this TextInfo textInfo, string str)
        {
            var tokens = str.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < tokens.Length; i++)
            {
                var token = tokens[i];
                tokens[i] = token.Substring(0, 1).ToUpper() + token.Substring(1);
            }

            return string.Join(" ", tokens);
        }
    }
}

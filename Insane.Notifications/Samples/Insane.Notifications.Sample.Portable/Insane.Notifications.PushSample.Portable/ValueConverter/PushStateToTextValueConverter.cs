using System;
using MvvmCross.Platform.Converters;

namespace Insane.Notifications.PushSample.Portable.ValueConverter
{
    public class PushStateToTextValueConverter : MvxValueConverter<bool, string>
    {
        protected override string Convert(bool value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value)
                return "You're subscribed to PUSH Service.";

            return "You are not subscribed to PUSH Service";
        }
    }
}

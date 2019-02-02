using System;
using System.Globalization;
using Newtonsoft.Json.Serialization;
using Xamarin.Forms;

namespace StarterProject.Converters
{
    public class EventArgsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ItemTappedEventArgs itemTappedEventArgs)
            {
                return itemTappedEventArgs.Item;
            }
            else if (value is EventArgs eventArgs)
            {
                return eventArgs;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
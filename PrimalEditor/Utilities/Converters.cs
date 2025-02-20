using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace PrimalEditor.Utilities
{
    internal class ButtonTag2ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SolidColorBrush? redBrush = Application.Current.TryFindResource("Editor.RedColorBrush") as SolidColorBrush;
            SolidColorBrush? grayBrush = Application.Current.TryFindResource("Editor.Window.GrayColorBrush6") as SolidColorBrush;

            Debug.Assert(redBrush != null);
            Debug.Assert(grayBrush != null);

            string? tagStr = (value as string);
            if (tagStr == null)
                return grayBrush;
            
            switch(tagStr)
            {
                case "Close": 
                    return redBrush;
                default:
                    return grayBrush;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

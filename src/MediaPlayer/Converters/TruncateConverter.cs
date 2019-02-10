using System;
using System.Windows.Data;

namespace MediaPlayer.Converters
{
    public class TruncateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            if (value == null)
                return "";
            else
            {
                int max = System.Convert.ToInt32(parameter);
                string text = System.Convert.ToString(value);

                if(text.Length < max)
                {
                    return text;
                }
                else
                {
                    return text.Substring(0, max) + "...";
                }
            }

        }
        public object ConvertBack(object value, Type targetType,
            object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

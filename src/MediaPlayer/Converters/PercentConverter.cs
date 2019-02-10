using System;
using System.Windows.Data;
using System.Globalization;

namespace MediaPlayer.Converters
{
    class PercentConverter : IValueConverter
    {
        /*
         * value will be the screen width or height and parameter will be the percentage requierd from the screen size
         * 
         * (OBS) "CultureInfo.InvariantCulture" convert to or from string independent of the "OS" or "OS Version" format
         */
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //value will be sent as an object
            double screenSize = System.Convert.ToDouble(value, CultureInfo.InvariantCulture);
            //parameter will be sent as an object
            double percent = System.Convert.ToDouble(parameter, CultureInfo.InvariantCulture);
            //get needed percentage and return it as string
            return ((int)(screenSize * percent)).ToString("G0", CultureInfo.InvariantCulture);
        }
        //not needed
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

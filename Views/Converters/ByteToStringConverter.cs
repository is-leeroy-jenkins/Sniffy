using System;

namespace Sniffy
{
    using System.Globalization;
    using System.Text;
    using System.Windows.Data;

    public class ByteToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            byte[] bytes = (byte[])value;
            string asciiString = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
            return asciiString;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

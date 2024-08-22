using System;

namespace Sniffy
{
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Windows.Data;

    public class WebSocketConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                string str = "";
                var recv = (ObservableCollection<string>)values[0];
                int cnt = (int)values[1];
                foreach (var item in recv)
                {
                    str += System.Convert.ToString(item);
                }
                return str;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

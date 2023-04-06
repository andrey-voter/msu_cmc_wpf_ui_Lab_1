using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace wpf_lab_1
{
    public class StringToDoubles : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is double[]))
                throw new NotImplementedException();
            
            return ((double[])value)[0].ToString() + ";" + ((double[])value)[1].ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                string[] tokens = value.ToString().Split(';');
                double second_d_l = double.Parse(tokens[0]);
                double second_d_r = double.Parse(tokens[1]);
                double[] values = new double[] { second_d_l, second_d_r };
                return values;
            }
            catch (Exception ex)
            {
                return new double[2] { 0f, 0f };
            }
        }
    }
}

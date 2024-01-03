using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace JiraClone.utils.graphicViewParts.converters
{
    public class NameSurnameConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length != 2) return DependencyProperty.UnsetValue;
            if (values[0].Equals(DependencyProperty.UnsetValue) || values[1].Equals(DependencyProperty.UnsetValue)) return "---";
            if (values[0] is string name && values[1] is string surname) return $"{name} {surname}";
            return DependencyProperty.UnsetValue;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}

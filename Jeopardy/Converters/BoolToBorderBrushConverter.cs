using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Jeopardy.Converters {
	internal class BoolToBorderBrushConverter : IValueConverter {
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			if (value == null) new SolidColorBrush(Color.FromArgb(255, 0, 1, 80)); ;

			bool boolVal = bool.Parse(value.ToString());
			
			if (boolVal) {
				return new SolidColorBrush(Color.FromArgb(255, 255, 255, 255)); ;
			}
			return new SolidColorBrush(Color.FromArgb(255, 0, 1, 80));
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			throw new NotImplementedException();
		}
	}
}

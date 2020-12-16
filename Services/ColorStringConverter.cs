using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Melembre.Source.Services
{
    public static class ColorStringConverter
    {
        public static Brush stringToColor(string color_code)
        {
            var converter = new System.Windows.Media.BrushConverter();
            var brush = (Brush)converter.ConvertFromString(color_code);
            return brush;
        }
    }
}

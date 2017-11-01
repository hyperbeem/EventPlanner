using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace EPlib.Util
{
    public class ColorHelper
    {
        public static Color ExtractColor(Brush br)
        {
            byte r = ((Color)br.GetValue(SolidColorBrush.ColorProperty)).R;
            byte g = ((Color)br.GetValue(SolidColorBrush.ColorProperty)).G;
            byte b = ((Color)br.GetValue(SolidColorBrush.ColorProperty)).B;

            Color c = Color.FromRgb(r, g, b);

            return c;
        }

        public static Color ExtractColor(Pen pen)
        {
            return ExtractColor(pen.Brush);
        }
    }
}

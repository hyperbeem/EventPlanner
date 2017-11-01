using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Media;

namespace EPlib.Drawable.Shapes
{
    public class Rectangle : InteractiveElement
    {
        public Rectangle() : base()
        {
            _ThisType = IElementType.Rectangle;

            using (StreamGeometryContext gC = _Geo.Open())
            {
                gC.BeginFigure(new Point(-10.0d, -15.0d), true, true);

                _PC = new PointCollection
                {
                    new Point(-10.0d,15.0d),
                    new Point(10.0d, 15.0d),
                    new Point(10.0d,-15.0d)
                };
                gC.PolyLineTo(_PC, true, true);
            }
            _Count++;
            _Name = "Rectangle" + _Count;

            _Fill = new SolidColorBrush(Color.FromRgb(192, 0, 0));
        }
    }
}

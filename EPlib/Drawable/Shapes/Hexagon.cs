using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Media;

using EPlib.Util.Interfaces;

namespace EPlib.Drawable.Shapes
{
    public class Hexagon : InteractiveElement
    {
        public Hexagon(ILogger logger) : base(logger)
        {
            _ThisType = IElementType.Hexagon;

            using (StreamGeometryContext gC = _Geo.Open())
            {
                double width = 20d;
                double height = 20d;
                gC.BeginFigure(new Point(width *1.0774, height*0.5), true, true);

                _PC = new PointCollection
                {
                    new Point(width*0.7887,height*0),
                    new Point(width*0.2113,height*0),
                    new Point(width*-0.0774,height*0.5),
                    new Point(width*0.2113,height),
                    new Point(width*0.7887,height)
                };
                gC.PolyLineTo(_PC, true, true);
            }
            _Count++;
            _Name = "Hexagon" + _Count;

            _Fill = new SolidColorBrush(Color.FromRgb(192, 0, 0));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Media;


namespace EPlib.Drawable.Shapes
{
    public class Pentagon : InteractiveElement
    {
        public Pentagon() : base()
        {
            using (StreamGeometryContext gC = _Geo.Open())
            {
                double width = 20d;
                double height = 20d;
                gC.BeginFigure(new Point(width * 1.0257, height * 0.618), true, true);

                _PC = new PointCollection
                {
                    new Point(width*0.5,height),
                    new Point(width*-0.0257,height*0.618),
                    new Point(width*0.1751,height*0),
                    new Point(width*0.8249,height*0)
                };
                gC.PolyLineTo(_PC, true, true);
            }
            _Count++;
            _Name = "Pentagon" + _Count;

            _Fill = new SolidColorBrush(Color.FromRgb(192, 0, 0));
        }
    }
}

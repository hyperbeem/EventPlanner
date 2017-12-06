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

            UpdateVisual();

            _Count++;
            _Name = "Hexagon" + _Count;

            _Fill = new SolidColorBrush(Color.FromRgb(192, 0, 0));

            _logger.LogInfo("Shape " + GetThisType + " has been created.");
        }

        public override void UpdateVisual()
        {
            using (StreamGeometryContext gC = _Geo.Open())
            {
                double s = GetScale;
                double width = 20d;
                double height = 20d;
                gC.BeginFigure(new Point((width * 1.0774) * s, (height * 0.5) * s), true, true);

                _PC = new PointCollection
                {
                    new Point((width*0.7887) *s,(height*0) *s),
                    new Point((width*0.2113)*s,(height*0)*s),
                    new Point((width*-0.0774)*s,(height*0.5)*s),
                    new Point((width*0.2113)*s,height *s),
                    new Point((width*0.7887)*s,height*s)
                };
                gC.PolyLineTo(_PC, true, true);
            }
        }
    }
}

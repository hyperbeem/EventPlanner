using EPlib.Util.Interfaces;
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
        public Rectangle(ILogger logger) : base(logger)
        {
            _ThisType = IElementType.Rectangle;

            UpdateVisual();

            _Count++;
            _Name = "Rectangle" + _Count;

            _Fill = new SolidColorBrush(Color.FromRgb(192, 0, 0));

            _logger.LogInfo("Shape " + GetThisType + " has been created.");
        }

        public override void UpdateVisual()
        {
            using (StreamGeometryContext gC = _Geo.Open())
            {
                double s = GetScale;
                gC.BeginFigure(new Point(-10.0d*s, -15.0d*s), true, true);

                _PC = new PointCollection
                {
                    new Point(-10.0d*s,15.0d*s),
                    new Point(10.0d*s, 15.0d*s),
                    new Point(10.0d*s,-15.0d*s)
                };
                gC.PolyLineTo(_PC, true, true);
            }
        }
    }
}

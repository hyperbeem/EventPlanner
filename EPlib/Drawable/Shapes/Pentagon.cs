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
    public class Pentagon : InteractiveElement
    {
        public Pentagon(ILogger logger) : base(logger)
        {
            _ThisType = IElementType.Pentagon;

            UpdateVisual();
            
            _Count++;
            _Name = "Pentagon" + _Count;

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
                gC.BeginFigure(new Point((width * 1.0257) * s, (height * 0.618)*s), true, true);

                _PC = new PointCollection
                {
                    new Point((width*0.5)*s,height*s),
                    new Point((width*-0.0257)*s,(height*0.618)*s),
                    new Point((width*0.1751)*s,(height*0)*s),
                    new Point((width*0.8249)*s,(height*0)*s)
                };
                gC.PolyLineTo(_PC, true, true);
            }
        }
    }
}

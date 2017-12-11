using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using EPlib.Util.Interfaces;

namespace EPlib.Drawable.Icons.Stands
{
    class Stage : InteractiveElement
    {
        public Stage(ILogger logger) : base(logger)
        {
            _ThisType = IElementType.Stage;
            _IsIcon = true;

            UpdateVisual();

            _Count++;
            _Name = "Stage " + _Count;

            string uri = "pack://application:,,,/EPlib;component/Drawable/Icons/Assets/Stage.png";
            ImageSource imageSource = new ImageSourceConverter().ConvertFromString(uri) as ImageSource;
            _Fill = new ImageBrush(new BitmapImage(new Uri(imageSource.ToString())));

            _logger.LogInfo("Icon " + GetThisType + " has been created.");
        }

        public override void UpdateVisual()
        {
            using (StreamGeometryContext gc = _Geo.Open())
            {
                double s = GetScale;
                gc.BeginFigure(new Point(-30.0d * s, -10.0d * s), true, true);

                _PC = new PointCollection();
                _PC.Add(new Point(30.0d * s, -10.0d * s));
                _PC.Add(new Point(30.0d * s, 10.0d * s));
                _PC.Add(new Point(-30.0d * s, 10.0d * s));

                gc.PolyLineTo(_PC, true, true);
            }
        }
    }
}

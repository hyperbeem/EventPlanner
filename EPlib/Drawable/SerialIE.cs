using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Input;
using EPlib.Drawable;
using EP = EPlib.Drawable.Shapes;
using System.Windows;

using EPlib.Util.Logs;

namespace EPlib.Drawable
{
    public class SerialIE
    {
        public String elementType { get; set; }
        public StreamGeometry Geometry { get; set; } // not needed
        public Point Point { get; set; }
        public PointCollection PointCollection { get; set; } // not needed
        public Color Stroke { get; set; }
        public Color Fill { get; set; }
        public long Count { get; set; }
        public String Name { get; set; }

        public InteractiveElement Load()
        {
            Enum.TryParse<InteractiveElement.IElementType>(elementType, out var Type);

            InteractiveElement _IE = null;

            switch (Type)
            {
                case InteractiveElement.IElementType.Square:
                    _IE = new EP.Square(new FileLogger(null));
                    break;
                case InteractiveElement.IElementType.Rectangle:
                    _IE = new EP.Rectangle(new FileLogger(null));
                    break;
                case InteractiveElement.IElementType.Triangle:
                    _IE = new EP.Triangle(new FileLogger(null));
                    break;
                case InteractiveElement.IElementType.Pentagon:
                    _IE = new EP.Pentagon(new FileLogger(null));
                    break;
                case InteractiveElement.IElementType.Hexagon:
                    _IE = new EP.Hexagon(new FileLogger(null));
                    break;
            }

            //_IE.GetGeometry = Geometry;
            _IE.Point = Point;
            _IE.GetPointCollection = PointCollection;
            _IE.GetStroke = new Pen(Brushes.Black,1); // Should be Stroke, Will convert to enum
            _IE.GetFill = new SolidColorBrush(Fill);
            _IE.GetCount = Count;
            _IE.GetName = Name;

            TranslateTransform tt = new TranslateTransform(Point.X,Point.Y);
            _IE.RenderTransform = tt;
            _IE.InvalidateVisual();

            return _IE;

        }
    }
}

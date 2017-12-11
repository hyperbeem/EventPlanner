using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Input;
using EPlib.Drawable;
using ES = EPlib.Drawable.Shapes;
using EF = EPlib.Drawable.Icons.Facillities;
using System.Windows;

using EPlib.Util.Logs;

namespace EPlib.Drawable
{
    public class SerialIE
    {
        public bool IsIcon { get; set; }
        public String ElementType { get; set; }
        public Point Point { get; set; }
        public Color Stroke { get; set; }
        public Color Fill { get; set; }
        public long Count { get; set; }
        public String Name { get; set; }
        public double Scale { get; set; }
        public string Information { get; set; }
        /// <summary>
        /// Loads all shapes from chosen file
        /// </summary>
        /// <returns></returns>
        public InteractiveElement Load(string loggerPath)
        {
            Enum.TryParse<InteractiveElement.IElementType>(ElementType, out var Type);

            InteractiveElement _IE = null;

            switch (Type)
            {
                case InteractiveElement.IElementType.Square:
                    _IE = new ES.Square(new FileLogger(loggerPath));
                    break;
                case InteractiveElement.IElementType.Rectangle:
                    _IE = new ES.Rectangle(new FileLogger(loggerPath));
                    break;
                case InteractiveElement.IElementType.Triangle:
                    _IE = new ES.Triangle(new FileLogger(loggerPath));
                    break;
                case InteractiveElement.IElementType.Pentagon:
                    _IE = new ES.Pentagon(new FileLogger(loggerPath));
                    break;
                case InteractiveElement.IElementType.Hexagon:
                    _IE = new ES.Hexagon(new FileLogger(loggerPath));
                    break;
            }

            _IE.SetPoint = Point;
            _IE.SetCount = Count;
            _IE.SetName = Name;
            _IE.SetScale = Scale;
            _IE.SetInformation = Information;

            TranslateTransform tt = new TranslateTransform(Point.X,Point.Y);
            _IE.RenderTransform = tt;
            _IE.InvalidateVisual();

            if(!IsIcon)
            {
                _IE.SetStroke = new Pen(Brushes.Black, 1); // Should be Stroke, Will convert to enum
                _IE.GetFill = new SolidColorBrush(Fill);
            }

            return _IE;

        }
    }
}

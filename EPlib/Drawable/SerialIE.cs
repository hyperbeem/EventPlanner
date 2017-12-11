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
using EFO = EPlib.Drawable.Icons.Foliage;
using EL = EPlib.Drawable.Icons.Layout;
using EST = EPlib.Drawable.Icons.Stands;
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
                case InteractiveElement.IElementType.Barriers:
                    _IE = new EL.Barriers(new FileLogger(loggerPath));
                    break;
                case InteractiveElement.IElementType.Camping:
                    _IE = new EF.Camping(new FileLogger(loggerPath));
                    break;
                case InteractiveElement.IElementType.Catering:
                    _IE = new EST.Catering(new FileLogger(loggerPath));
                    break;
                case InteractiveElement.IElementType.Checkpoint:
                    _IE = new EL.Checkpoint(new FileLogger(loggerPath));
                    break;
                case InteractiveElement.IElementType.Display:
                    _IE = new EST.Display(new FileLogger(loggerPath));
                    break;
                case InteractiveElement.IElementType.Fencing:
                    _IE = new EL.Fencing(new FileLogger(loggerPath));
                    break;
                case InteractiveElement.IElementType.FreeArea:
                    _IE = new EL.FreeArea(new FileLogger(loggerPath));
                    break;
                case InteractiveElement.IElementType.Hexagon:
                    _IE = new ES.Hexagon(new FileLogger(loggerPath));
                    break;
                case InteractiveElement.IElementType.Information:
                    _IE = new EF.Information(new FileLogger(loggerPath));
                    break;
                case InteractiveElement.IElementType.Lighting:
                    _IE = new EL.Lighting(new FileLogger(loggerPath));
                    break;
                case InteractiveElement.IElementType.Marquee:
                    _IE = new EL.Lighting(new FileLogger(loggerPath));
                    break;
                case InteractiveElement.IElementType.Medical:
                    _IE = new EF.Medical(new FileLogger(loggerPath));
                    break;
                case InteractiveElement.IElementType.Path:
                    _IE = new EL.Path(new FileLogger(loggerPath));
                    break;
                case InteractiveElement.IElementType.Pentagon:
                    _IE = new ES.Pentagon(new FileLogger(loggerPath));
                    break;
                case InteractiveElement.IElementType.Rectangle:
                    _IE = new ES.Rectangle(new FileLogger(loggerPath));
                    break;
                case InteractiveElement.IElementType.Road:
                    _IE = new EL.Road(new FileLogger(loggerPath));
                    break;
                case InteractiveElement.IElementType.Seating:
                    _IE = new EL.Seating(new FileLogger(loggerPath));
                    break;
                case InteractiveElement.IElementType.Shrubs:
                    _IE = new EFO.Shrubs(new FileLogger(loggerPath));
                    break;
                case InteractiveElement.IElementType.Square:
                    _IE = new ES.Square(new FileLogger(loggerPath));
                    break;
                case InteractiveElement.IElementType.Stage:
                    _IE = new EST.Stage(new FileLogger(loggerPath));
                    break;
                case InteractiveElement.IElementType.Trees:
                    _IE = new EFO.Trees(new FileLogger(loggerPath));
                    break;
                case InteractiveElement.IElementType.Triangle:
                    _IE = new ES.Triangle(new FileLogger(loggerPath));
                    break;
                case InteractiveElement.IElementType.WCRow:
                    _IE = new EF.WCRow(new FileLogger(loggerPath));
                    break;
                case InteractiveElement.IElementType.WCSingle:
                    _IE = new EF.WCSingle(new FileLogger(loggerPath));
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
                _IE.SetStroke = new Pen(Brushes.Black, 1);
                _IE.GetFill = new SolidColorBrush(Fill);
            }

            return _IE;

        }
    }
}

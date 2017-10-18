using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace EPlib.Drawable
{
    public abstract class InteractiveElement : UIElement
    {
        public enum IElementType
        {
            Hexagon,
            Pentagon,
            Rectangle,
            Square,
            Triangle
        }

        protected StreamGeometry _Geo;
        public StreamGeometry GetGeometry
        {
            get { return _Geo; }
            set { _Geo = value; }
        }

        protected PointCollection _PC;
        protected Pen _Stroke;
        protected Brush _Fill;

        protected static long _Count;
        protected String _Name;

        public string Name
        {
            get { return _Name; }
        }

        public InteractiveElement()
        {
            _Stroke = new Pen(Brushes.Black, 1);
            _Fill = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            _Geo = new StreamGeometry();

            _Name = "";
        }

        protected override void OnRender(DrawingContext dc)
        {
            Render(dc);
        }

        protected virtual void Render(DrawingContext dc)
        {
            dc.DrawGeometry(_Fill, _Stroke, _Geo);
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            _Stroke = new Pen(Brushes.White, 1);
            InvalidateVisual();

            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            _Stroke = new Pen(Brushes.Black, 1);
            InvalidateVisual();

            base.OnMouseLeave(e);
        }
    }
}

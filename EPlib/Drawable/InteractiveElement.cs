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

using EPlib.Util.Interfaces;

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

        protected readonly ILogger _logger;

        protected IElementType _ThisType;
        public string GetThisType
        {
            get { return _ThisType.ToString(); }
        }

        protected StreamGeometry _Geo;
        public StreamGeometry GetGeometry
        {
            get { return _Geo; }
            set { _Geo = value; }
        }

        public Point Point { get; set; }

        protected PointCollection _PC;
        public PointCollection GetPointCollection
        {
            get { return _PC; }
            set { _PC = value; }
        }

        protected Pen _Stroke;
        public Pen GetStroke
        {
            get { return _Stroke; }
            set { _Stroke = value; }
        }

        protected Brush _Fill;
        public Brush GetFill
        {
            get { return _Fill; }
            set { _Fill = value; }
        }

        protected static long _Count;
        public long GetCount
        {
            get { return _Count; }
            set { _Count = value; }
        }

        protected String _Name;
        public string GetName
        {
            get { return _Name; }
            set { _Name = value; }
        }


        public InteractiveElement(ILogger logger)
        {
            _logger = logger;
            _Stroke = new Pen(Brushes.Black, 1);
            _Fill = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            _Geo = new StreamGeometry();
            
            _Name = "";
        }

        public InteractiveElement(StreamGeometry geometry, Pen stroke, Brush brush, String name)
        {

        }

        public void LogCreation()
        {
            _logger.LogInfo("Shape " + GetThisType + " has been created.");
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

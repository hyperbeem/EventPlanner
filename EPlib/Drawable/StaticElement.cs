using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

using EPlib.Util.Interfaces;

namespace EPlib.Drawable
{
    public class StaticElement : UIElement
    {
        protected StreamGeometry _Geo;
        protected PointCollection _PointCollection;
        protected long _Count;

        public StaticElement()
        {
            _Geo = new StreamGeometry();

        }

        protected override void OnRender(DrawingContext dc)
        {
            Render(dc);
        }

        protected virtual void Render(DrawingContext dc)
        {
            //dc.DrawGeometry()
        }
    }
}

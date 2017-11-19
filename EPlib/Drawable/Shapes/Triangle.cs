﻿using EPlib.Util.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Media;

namespace EPlib.Drawable.Shapes
{
    public class Triangle : InteractiveElement
    {
        public Triangle(ILogger logger) : base(logger)
        {
            _ThisType = IElementType.Triangle;

            using (StreamGeometryContext gC = _Geo.Open())
            {
                gC.BeginFigure(new Point(-10.0d, -10.0d), true, true);

                _PC = new PointCollection
                {
                    new Point(10.0d,-10.0d),
                    new Point(10.0d, 10.0d)
                };
                gC.PolyLineTo(_PC, true, true);
            }
            _Count++;
            _Name = "Triangle" + _Count;

            _Fill = new SolidColorBrush(Color.FromRgb(192, 0, 0));

            _logger.LogInfo("Shape " + GetThisType + " has been created.");
        }
    }
}

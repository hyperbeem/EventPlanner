using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace EPlib.Util
{
    public class DrawGrid
    {
        /// <summary>
        /// Parveen_Kumar Gupta of CodeProject
        /// https://www.codeproject.com/Tips/1109487/Show-Gridlines-on-canvas-With-Size-Slider-WPF
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="DrawingCanvas"></param>
        public static void Draw(int offset, Canvas DrawingCanvas)
        {
            RemoveGraph(DrawingCanvas);

            Image lines = new Image();
            lines.SetValue(Panel.ZIndexProperty, -100);

            // Draw the grid on canvas
            DrawingVisual gridLinesV = new DrawingVisual();
            DrawingContext dcx = gridLinesV.RenderOpen();
            Pen lightPen = new Pen(new SolidColorBrush(Color.FromRgb(180,180,180)), 0.5), darkPen = new Pen(Brushes.LightSlateGray, 0);
            lightPen.Freeze();
            darkPen.Freeze();

            int xO = offset,
                yO = offset,
                rows = (int)(SystemParameters.PrimaryScreenHeight),
                columns = (int)(SystemParameters.PrimaryScreenWidth),
                alt = yO == 5 ? yO : 1,
                j = 0;

            // Horizontal
            Point x = new Point(0, 0.5);
            Point y = new Point(SystemParameters.PrimaryScreenWidth, 0.5);

            for (int i = 0; i <= rows; i++, j++)
            {
                dcx.DrawLine(j % alt == 0 ? lightPen : darkPen, x, y);
                x.Offset(0, yO);
                y.Offset(0, yO);
            }
            j = 0;

            // Vertical
            x = new Point(0.5, 0);
            y = new Point(0.5, SystemParameters.PrimaryScreenHeight);

            for (int i = 0; i <= columns; i++,j++)
            {
                dcx.DrawLine(j % alt == 0 ? lightPen : darkPen, x, y);
                x.Offset(xO, 0);
                y.Offset(xO, 0);
            }

            dcx.Close();

            RenderTargetBitmap bmp = new RenderTargetBitmap((int)SystemParameters.PrimaryScreenWidth,
                (int)SystemParameters.PrimaryScreenHeight, 96, 96, PixelFormats.Pbgra32);
            bmp.Render(gridLinesV);
            bmp.Freeze();
            lines.Source = bmp;

            DrawingCanvas.Children.Add(lines);

        }

        /// <summary>
        /// Parveen_Kumar Gupta of CodeProject
        /// https://www.codeproject.com/Tips/1109487/Show-Gridlines-on-canvas-With-Size-Slider-WPF
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="DrawingCanvas"></param>
        public static void RemoveGraph(Canvas DrawingCanvas)
        {
            foreach(UIElement ui in DrawingCanvas.Children)
            {
                if(ui is Image)
                {
                    DrawingCanvas.Children.Remove(ui);
                    break;
                }
            }
        }
    }
}

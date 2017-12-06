using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using EPlib.Util.Interfaces;
using EPlib.Util;
using System.Windows.Controls;

namespace EPlib.Application.Preferences
{
    public class PreferencesManager
    {
        // Perferences
        public bool usingGrid { get; set; }

        private int gridAmount;
        public int GridAmount
        {
            get { return gridAmount; }

            set
            {
                gridAmount = value;
            }
        }

        private int canvasWidth;
        public int SetCanvasWidth
        {
            set { canvasWidth = value; }
        }

        private int canvasHeight;
        public int SetCanvasHeight
        {
            set { canvasHeight = value; }
        }

        private string logPath;
        public string GetLogPath
        {
            get { return logPath; }
        }

        public PreferencesManager(string LogPath)
        {
            logPath = LogPath;
            usingGrid = false;
            gridAmount = 1;
        }

        /// <summary>
        /// Returns the closest grid point
        /// </summary>
        /// <param name="mousePosition"></param>
        /// <returns></returns>
        public Point GetGridLocation(Point mousePosition)
        {
            if (usingGrid == false)
                return mousePosition;

            return NearestPoint(mousePosition);

        }

        public void CreateGrid(Canvas canvas)
        {
            if (usingGrid)
                if (GridAmount != 0)
                    if (GridAmount != 1)
                        DrawGrid.Draw(gridAmount, canvas);
        }

        public void RemoveGrid(Canvas canvas)
        {
            DrawGrid.RemoveGraph(canvas);
        }

        /// <summary>
        /// Returns the closets grid point
        /// </summary>
        /// <param name="currentPoint"></param>
        /// <returns></returns>
        private Point NearestPoint(Point currentPoint)
        {
            double x, y, am = gridAmount / 2D;

            double ix = currentPoint.X % gridAmount, iy = currentPoint.Y % gridAmount;

            if (ix > am)
                x = (currentPoint.X - ix) + gridAmount;
            else
                x = currentPoint.X - ix;

            if (iy > am)
                y = (currentPoint.Y - iy) + gridAmount;
            else
                y = currentPoint.Y - iy;

            return new Point(x, y);
        }

    }
}

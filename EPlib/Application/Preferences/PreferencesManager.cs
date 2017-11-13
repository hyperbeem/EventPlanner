using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;
using EPlib.Util.Interfaces;

namespace EPlib.Application.Preferences
{
    public class PreferencesManager
    {
        // Logger
        protected readonly ILogger _logger;

        // Perferences
        public bool usingGrid { get; set; }
        public int canvasSizeX { get; set; }
        public int canvaseSizeY { get; set; }

        private int gridAmount;
        public int GridAmount
        {
            get { return gridAmount; }

            set
            {
                gridAmount = value;
                UpdateGridList();
            }
        }

        private List<int> gridListX;
        private List<int> gridListY;

        private string logPath;
        public string GetLogPath
        {
            get { return logPath; }
        }

        public PreferencesManager(string LogPath, ILogger Logger, int CanvasSizeX, int CanvasSizeY)
        {
            _logger = Logger;
            gridListX = new List<int>();
            gridListY = new List<int>();

            canvasSizeX = CanvasSizeX;
            _logger.LogInfo("Canvas Width = " + CanvasSizeX);
            canvaseSizeY = CanvasSizeY;
            _logger.LogInfo("Canvas Height = " + CanvasSizeY);

            logPath = LogPath;

            usingGrid = false;
            gridAmount = 1;
        }

        public void UpdateGridList()
        {
            UpdateGridList(canvasSizeX, canvaseSizeY);
        }

        public void UpdateGridList(int canvasX, int canvasY)
        {
            gridListX.Clear();
            gridListY.Clear();

            int x = 0, y = 0;

            while (x < canvasX && y < canvasY)
            {
                gridListX.Add(x);
                gridListY.Add(y);

                x += gridAmount;
                y += gridAmount;
            }

            _logger.LogInfo("Grid has been updated");
        }

        [Obsolete]
        public bool OnGrid(Point MousePosition)
        {
            if (usingGrid == false)
                return true;

            if ((int)MousePosition.X % gridAmount == 0 && (int)MousePosition.Y % gridAmount == 0)
                return true;
            else
                return false;
        }

        public Point GetGridLocation(Point mousePosition)
        {
            if (usingGrid == false)
                return mousePosition;

            return NearestPoint(mousePosition);

        }

        // Nick F https://social.msdn.microsoft.com/Forums/en-US/54e0ec6e-319c-438f-a59f-141f7c51e795/how-to-get-the-nearest-one-to-a-given-value-in-a-list-and-the-second-nearest-one-too?forum=csharpgeneral
        private Point NearestPoint(Point currentPoint)
        {
            int i, i2;

            i = gridListX.BinarySearch((int)currentPoint.X);
            i2 = gridListY.BinarySearch((int)currentPoint.Y);

            if (0 >= i)
                i = ~i;
            if (0 >= i2)
                i2 = ~i2;

            return new Point(gridListX[i - 1], gridListY[i2 - 1]);

        }

    }
}

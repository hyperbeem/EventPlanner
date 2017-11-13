using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;

namespace EPlib.Application.Preferences
{
    public class PreferencesManager
    {
        // Perferences
        public bool usingGrid { get; set; }
        public int GridAmount { get; set; }

        private readonly string logPath;
        public string GetLogPath
        {
            get { return logPath; }
        }

        public PreferencesManager()
        {
            logPath = System.IO.Path.GetTempFileName();
            usingGrid = false;
            GridAmount = 1;
        }

        public bool OnGrid(Point MousePosition)
        {
            if (usingGrid == false)
                return true;

            if ((int)MousePosition.X % GridAmount == 0 && (int)MousePosition.Y % GridAmount == 0)
                return true;
            else
                return false;
        }


    }
}

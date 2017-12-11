using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using EPlib.Util.Interfaces;

namespace EPlib.Drawable.Icons.Facillities
{
    public class WCRow : InteractiveElement
    {
        public WCRow(ILogger logger) : base (logger)
        {
            _ThisType = IElementType.WCRow;
            _IsIcon = true;

            UpdateVisual();

            _Count++;
            _Name = "WCRow " + _Count;

            string uri = "pack://application:,,,/EPlib;component/Drawable/Icons/Assets/WCRow.png";
            ImageSource imageSource = new ImageSourceConverter().ConvertFromString(uri) as ImageSource;
            _Fill = new ImageBrush(new BitmapImage(new Uri(imageSource.ToString())));

            _logger.LogInfo("Icon " + GetThisType + " has been created.");
        }

        public override void UpdateVisual()
        {
            base.UpdateVisual();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using EPlib.Util.Interfaces;

namespace EPlib.Drawable.Icons.Layout
{
    class Seating : InteractiveElement
    {
        public Seating(ILogger logger) : base(logger)
        {
            _ThisType = IElementType.Seating;
            _IsIcon = true;

            UpdateVisual();

            _Count++;
            _Name = "Seating " + _Count;

            string uri = "pack://application:,,,/EPlib;component/Drawable/Icons/Assets/Seating.png";
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

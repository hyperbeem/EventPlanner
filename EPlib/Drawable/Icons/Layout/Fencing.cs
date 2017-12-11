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
    class Fencing : InteractiveElement
    {
        public Fencing(ILogger logger) : base(logger)
        {
            _ThisType = IElementType.Fencing;
            _IsIcon = true;

            UpdateVisual();

            _Count++;
            _Name = "Fence " + _Count;

            string uri = "pack://application:,,,/EPlib;component/Drawable/Icons/Assets/Fence.png";
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

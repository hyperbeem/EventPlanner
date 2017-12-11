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
    class Barriers : InteractiveElement
    {
        public Barriers(ILogger logger) : base(logger)
        {
            _ThisType = IElementType.Barriers;
            _IsIcon = true;

            UpdateVisual();

            _Count++;
            _Name = "Barrier " + _Count;

            string uri = "pack://application:,,,/EPlib;component/Drawable/Icons/Assets/Barrier.png";
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

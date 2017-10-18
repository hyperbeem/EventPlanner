using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using EPlib.Drawable;
using EP = EPlib.Drawable.Shapes;

namespace Event_Planner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        InteractiveElement _IE;
        InteractiveElement.IElementType _CurrentType;
        bool _Placing;
        bool _Selecting;
        HitTestResult _HitResult;

        public MainWindow()
        {
            InitializeComponent();
            _CurrentType = InteractiveElement.IElementType.Square;
        }

        private void DrawingCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            switch (_CurrentType)
            {
                case InteractiveElement.IElementType.Square:
                    _IE = new EP.Square();
                    break;
                case InteractiveElement.IElementType.Rectangle:
                    _IE = new EP.Rectangle();
                    break;
                case InteractiveElement.IElementType.Triangle:
                    _IE = new EP.Triangle();
                    break;
                case InteractiveElement.IElementType.Pentagon:
                    _IE = new EP.Pentagon();
                    break;
                case InteractiveElement.IElementType.Hexagon:
                    _IE = new EP.Hexagon();
                    break;
            }

            TranslateTransform tt = new TranslateTransform(e.GetPosition(DrawingCanvas).X, e.GetPosition(DrawingCanvas).Y);
            _IE.RenderTransform = tt;

            DrawingCanvas.Children.Add(_IE);

            _Placing = true;
        }

        private void DrawingCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _Placing = false;
        }

        private void DrawingCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            TranslateTransform tt;

            if(_Selecting)
            {
                if(_HitResult != null)
                {
                    tt = new TranslateTransform(e.GetPosition(DrawingCanvas).X, e.GetPosition(DrawingCanvas).Y);
                    _IE.RenderTransform = tt;

                    Static_ObjectName.Content = _IE.Name;
                }
            }
            else
            {
                if(_IE != null && _Placing)
                {
                    tt = new TranslateTransform(e.GetPosition(DrawingCanvas).X, e.GetPosition(DrawingCanvas).Y);
                    _IE.RenderTransform = tt;

                    Static_ObjectName.Content = "";
                }
            }
        }

        private void DrawingCanvas_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            _Selecting = false;
        }

        private void DrawingCanvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(sender is UIElement)
            {
                Point p = e.GetPosition((UIElement)sender);
                _HitResult = VisualTreeHelper.HitTest(DrawingCanvas, p);

                if(_HitResult != null)
                    if(_HitResult.VisualHit.GetType() != typeof(Canvas))
                    {
                        _IE = (InteractiveElement)_HitResult.VisualHit;
                        _Selecting = true;
                    }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Menu_Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();   
        }

        private void HexagonButton_Click(object sender, RoutedEventArgs e)
        {
            _CurrentType = InteractiveElement.IElementType.Hexagon;
        }

        private void RectangleButton_Click(object sender, RoutedEventArgs e)
        {
            _CurrentType = InteractiveElement.IElementType.Rectangle;
        }

        private void SquareButton_Click(object sender, RoutedEventArgs e)
        {
            _CurrentType = InteractiveElement.IElementType.Square;
        }

        private void TriangleButton_Click(object sender, RoutedEventArgs e)
        {
            _CurrentType = InteractiveElement.IElementType.Triangle;
        }

        private void PentagonButton_Click(object sender, RoutedEventArgs e)
        {
            _CurrentType = InteractiveElement.IElementType.Pentagon;
        }
    }
}

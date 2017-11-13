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

using EPlib.Application.Preferences;
using EPlib.Drawable;
using EPlib.Util;
using EPlib.Util.Logs;
using EP = EPlib.Drawable.Shapes;
using cIO = EPlib.Application.InOut;
using Microsoft.Win32;

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
        Point _Current;
        PreferencesManager _PM;

        public readonly string logPath = System.IO.Path.GetTempFileName();

        // Canvas
        private double _ZoomMax = 10;
        private double _ZoomMin = 0.2;
        private double _ZoomSpeed = 0.001;
        private double _Zoom = 1;

        public MainWindow()
        {
            InitializeComponent();
            _CurrentType = InteractiveElement.IElementType.Square;
            _PM = new PreferencesManager();
        }

        private void DrawingCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                switch (_CurrentType)
                {
                    case InteractiveElement.IElementType.Square:
                        _IE = new EP.Square(new FileLogger(_PM.GetLogPath));
                        break;
                    case InteractiveElement.IElementType.Rectangle:
                        _IE = new EP.Rectangle(new FileLogger(_PM.GetLogPath));
                        break;
                    case InteractiveElement.IElementType.Triangle:
                        _IE = new EP.Triangle(new FileLogger(_PM.GetLogPath));
                        break;
                    case InteractiveElement.IElementType.Pentagon:
                        _IE = new EP.Pentagon(new FileLogger(_PM.GetLogPath));
                        break;
                    case InteractiveElement.IElementType.Hexagon:
                        _IE = new EP.Hexagon(new FileLogger(_PM.GetLogPath));
                        break;
                }

                _IE.LogCreation();

                TranslateTransform tt = new TranslateTransform(e.GetPosition(DrawingCanvas).X, e.GetPosition(DrawingCanvas).Y);
                _IE.RenderTransform = tt;

                _IE.Point = new Point(e.GetPosition(DrawingCanvas).X, e.GetPosition(DrawingCanvas).Y);
                DrawingCanvas.Children.Add(_IE);

                _Placing = true;
            }
        }

        private void DrawingCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _Placing = false;
            DrawingCanvas.ReleaseMouseCapture();
        }

        private void DrawingCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            TranslateTransform tt;

            _Current = e.GetPosition(DrawingCanvas);

            if (_Selecting)
            {
                if (_HitResult != null)
                {
                    if (_PM.OnGrid(_Current))
                    {
                        tt = new TranslateTransform(e.GetPosition(DrawingCanvas).X, e.GetPosition(DrawingCanvas).Y);
                        _IE.RenderTransform = tt;
                        _IE.Point = new Point(tt.X, tt.Y);
                        UpdateProperties(_IE);
                    }
                }
            }
            else
            {
                if (_IE != null && _Placing)
                {
                    if (_PM.OnGrid(_Current))
                    {
                        tt = new TranslateTransform(e.GetPosition(DrawingCanvas).X, e.GetPosition(DrawingCanvas).Y);
                        _IE.RenderTransform = tt;
                        _IE.Point = new Point(tt.X, tt.Y);
                        UpdateProperties(_IE);
                    }
                }
            }
        }

        private void DrawingCanvas_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            _Selecting = false;
        }

        private void DrawingCanvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is UIElement)
            {
                Point p = e.GetPosition(DrawingCanvas);
                _IE = GetElementAtPos(p);
                if (_IE != null)
                {
                    UpdateProperties(_IE);
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

        // https://userinterfacemaker.wordpress.com/2015/09/08/zooming-for-canvas-cwpf/
        private void DrawingCanvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            _Zoom += _ZoomSpeed * e.Delta;

            if (_Zoom < _ZoomMin)
                _Zoom = _ZoomMin;
            if (_Zoom > _ZoomMax)
                _Zoom = _ZoomMax;

            Point mp = e.GetPosition(DrawingCanvas);

            if (_Zoom > 1)
            {
                DrawingCanvas.RenderTransform = new ScaleTransform(_Zoom, _Zoom, mp.X, mp.Y);
            }
            else
            {
                DrawingCanvas.RenderTransform = new ScaleTransform(_Zoom, _Zoom);
            }
        }

        private InteractiveElement GetElementAtPos(Point p)
        {
            _HitResult = VisualTreeHelper.HitTest(DrawingCanvas, p);

            if (_HitResult != null)
            {
                if (_HitResult.VisualHit.GetType() != typeof(Canvas))
                {
                    if (_HitResult.VisualHit.GetType().BaseType == typeof(InteractiveElement))
                    {
                        return (InteractiveElement)_HitResult.VisualHit;
                    }
                }
            }
            return null;
        }

        private void UpdateProperties(InteractiveElement IE)
        {
            // Name
            PropTBoxName.Text = IE.GetName;

            // Position
            PropTBoxX.Text = IE.Point.X.ToString();
            PropTBoxY.Text = IE.Point.Y.ToString();

            // Color
            Color c = ColorHelper.ExtractColor(IE.GetFill);
            PropBoxColor.Fill = IE.GetFill;
            PropTBoxColorR.Text = c.R.ToString();
            PropTBoxColorG.Text = c.G.ToString();
            PropTBoxColorB.Text = c.B.ToString();
        }

        private void PropTBoxX_KeyUp(object sender, KeyEventArgs e)
        {
            XYBox(e);
        }

        private void PropTBoxY_KeyUp(object sender, KeyEventArgs e)
        {
            XYBox(e);
        }

        private void PropTBoxColorR_KeyUp(object sender, KeyEventArgs e)
        {
            RGBBox(e);
        }

        private void PropTBoxColorG_KeyUp(object sender, KeyEventArgs e)
        {
            RGBBox(e);
        }

        private void PropTBoxColorB_KeyUp(object sender, KeyEventArgs e)
        {
            RGBBox(e);
        }

        private void XYBox(KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                bool isNumericX = double.TryParse(PropTBoxX.Text, out var x);
                bool isNumeticY = double.TryParse(PropTBoxY.Text, out var y);

                if (isNumericX && isNumeticY)
                {
                    TranslateTransform tt;
                    tt = new TranslateTransform(x, y);
                    _IE.RenderTransform = tt;
                }
            }
        }

        private void RGBBox(KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                bool isNumericR = byte.TryParse(PropTBoxColorR.Text, out var r);
                bool isNumericG = byte.TryParse(PropTBoxColorG.Text, out var g);
                bool isNumericB = byte.TryParse(PropTBoxColorB.Text, out var b);

                if (isNumericR && isNumericG && isNumericB)
                {
                    _IE.GetFill = new SolidColorBrush(Color.FromRgb(r, g, b));
                    UpdateProperties(_IE);
                    _IE.InvalidateVisual();
                }
            }
        }

        private void GridToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            _PM.usingGrid = true;
        }

        private void GridToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            _PM.usingGrid = false;
        }

        private void GSnapTBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                bool isNumeric = int.TryParse(GSnapTBox.Text, out var am);
                if (isNumeric)
                {
                    _PM.GridAmount = am;
                }
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (sender is UIElement)
            {
                if (_IE != null & e.Key == Key.Delete)
                {
                    DrawingCanvas.Children.Remove(_IE);
                }
            }
        }

        private void Menu_Save_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog();

            sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            sfd.Filter = "XML file (*.xml)|*.xml";

            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = sfd.FileName;
                List<UIElement> toSave = new List<UIElement>();

                foreach (UIElement ch in DrawingCanvas.Children)
                {
                    toSave.Add(ch);
                }
                
                cIO.StreamXML.WriteXMLIElements(path, toSave);
            }
        }

        private void Menu_Open_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
            ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            ofd.Filter = "XML file (*.xml)|*.xml";

            if(ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = ofd.FileName;
                List<SerialIE> processList = cIO.StreamXML.ReadXMLIElements(path);

                if (DrawingCanvas.Children.Count != 0)
                    DrawingCanvas.Children.Clear();

                foreach(SerialIE s in processList)
                {
                    DrawingCanvas.Children.Add(s.Load());
                }
            }
        }
    }
}

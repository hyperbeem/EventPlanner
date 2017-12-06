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
using System.Reflection;

using EPlib.Application.Preferences;
using EPlib.Drawable;
using EPlib.Util;
using EPlib.Util.Logs;
using EP = EPlib.Drawable.Shapes;
using cIO = EPlib.Application.InOut;

namespace Event_Planner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        InteractiveElement _IE;
        InteractiveElement.IElementType _CurrentType;
        int _MenuStyle = 0;
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
            var _TypeList = AsmInfo.GetTypesFromNamespace("EPlib.Drawable.Shapes");
            _CurrentType = InteractiveElement.IElementType.Square;
            _PM = new PreferencesManager(logPath);
        }

        public void sudoGrid()
        {
            _PM.CreateGrid(DrawingCanvas);
        }


        private void DrawingCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                string sType = "";

                if (_MenuStyle == 0)
                    sType = "EPlib.Drawable.Shapes." + _CurrentType.ToString();
                if (_MenuStyle == 1)
                    sType = "EPlib.Drawable.Icons." + _CurrentType.ToString();
                if (_MenuStyle >= 2)
                    throw new ArgumentOutOfRangeException("Woah... how the fak did you do that?");

                // Reflection creation method
                Assembly assembly = Assembly.Load("Eplib");
                Type t = assembly.GetType(sType);
                Object args = new FileLogger(_PM.GetLogPath);

                Object obj = Activator.CreateInstance(t, args);

                // Cast to InteractiveElement
                _IE = (InteractiveElement)obj;

                TranslateTransform tt = new TranslateTransform(e.GetPosition(DrawingCanvas).X, e.GetPosition(DrawingCanvas).Y);
                _IE.RenderTransform = tt;

                _IE.SetPoint = new Point(e.GetPosition(DrawingCanvas).X, e.GetPosition(DrawingCanvas).Y);
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
                    Point currentGrid = _PM.GetGridLocation(_Current);
                    tt = new TranslateTransform(currentGrid.X, currentGrid.Y);
                    _IE.RenderTransform = tt;
                    _IE.SetPoint = new Point(tt.X, tt.Y);
                    UpdateProperties(_IE);
                }
            }
            else
            {
                if (_IE != null && _Placing)
                {
                    Point currentGrid = _PM.GetGridLocation(_Current);
                    tt = new TranslateTransform(currentGrid.X, currentGrid.Y);
                    _IE.RenderTransform = tt;
                    _IE.SetPoint = new Point(tt.X, tt.Y);
                    UpdateProperties(_IE);
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
            _MenuStyle = 0;
            _CurrentType = InteractiveElement.IElementType.Hexagon;
        }

        private void RectangleButton_Click(object sender, RoutedEventArgs e)
        {
            _MenuStyle = 0;
            _CurrentType = InteractiveElement.IElementType.Rectangle;
        }

        private void SquareButton_Click(object sender, RoutedEventArgs e)
        {
            _MenuStyle = 0;
            _CurrentType = InteractiveElement.IElementType.Square;
        }

        private void TriangleButton_Click(object sender, RoutedEventArgs e)
        {
            _MenuStyle = 0;
            _CurrentType = InteractiveElement.IElementType.Triangle;
        }

        private void PentagonButton_Click(object sender, RoutedEventArgs e)
        {
            _MenuStyle = 0;
            _CurrentType = InteractiveElement.IElementType.Pentagon;
        }

        private void TentButton_Click(object sender, RoutedEventArgs e)
        {
            _MenuStyle = 1;
            _CurrentType = InteractiveElement.IElementType.Tent;
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
            PropTBoxX.Text = IE.GetPoint.X.ToString();
            PropTBoxY.Text = IE.GetPoint.Y.ToString();

            // Color
            Color c = ColorHelper.ExtractColor(IE.GetFill);
            PropBoxColor.Fill = IE.GetFill;
            PropTBoxColorR.Text = c.R.ToString();
            PropTBoxColorG.Text = c.G.ToString();
            PropTBoxColorB.Text = c.B.ToString();

            PropTBoxScale.Text = _IE.GetScale.ToString();
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
            _PM.CreateGrid(DrawingCanvas);
        }

        private void GridToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            _PM.usingGrid = false;
            _PM.RemoveGrid(DrawingCanvas);
        }

        private void GSnapTBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                bool isNumeric = int.TryParse(GSnapTBox.Text, out var am);
                if (isNumeric)
                {
                    _PM.GridAmount = am;
                    _PM.CreateGrid(DrawingCanvas);
                }
            }
        }

        private void GSnapTBox_LostFocus(object sender, RoutedEventArgs e)
        {
            bool isNumeric = int.TryParse(GSnapTBox.Text, out var am);
            if (isNumeric)
            {
                _PM.GridAmount = am;
                _PM.CreateGrid(DrawingCanvas);
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

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = ofd.FileName;
                List<SerialIE> processList = cIO.StreamXML.ReadXMLIElements(path);

                if (DrawingCanvas.Children.Count != 0)
                    DrawingCanvas.Children.Clear();

                foreach (SerialIE s in processList)
                {
                    DrawingCanvas.Children.Add(s.Load(_PM.GetLogPath));
                }
            }
        }

        private void SliderScale_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (_IE != null)
            {
                double x = Math.Round(e.NewValue, 2);

                _IE.SetSclae = x;
                PropTBoxScale.Text = x.ToString();
            }
        }

        private void PropTBoxScale_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                ScaleAgjust(PropTBoxScale.Text);
        }

        private void PropTBoxScale_LostFocus(object sender, RoutedEventArgs e)
        {
            ScaleAgjust(PropTBoxScale.Text);
        }

        private void ScaleAgjust(string value)
        {
            bool isDouble = double.TryParse(value, out var oVal);
            oVal = Math.Round(oVal, 2);

            if (isDouble)
            {
                if (oVal > 10)
                {
                    oVal = 10d;
                    PropTBoxScale.Text = "10";
                }

                SliderScale.Value = oVal;
                _IE.SetSclae = oVal;
            }
        }

        private void HideToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if (_IE != null)
            {
                _IE.IsVisable = !_IE.IsVisable;
                _IE.InvalidateVisual();
            }
        }

        private void HideAllToggleButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (UIElement IE in DrawingCanvas.Children)
            {
                if (IE is InteractiveElement)
                {
                    InteractiveElement IEX = (InteractiveElement)IE;
                    if (IEX.IsVisable == true)
                    {
                        IEX.IsVisable = false;
                        IEX.InvalidateVisual();
                    }
                }
            }
        }

        private void UnhideAllToggleButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (UIElement IE in DrawingCanvas.Children)
            {
                if (IE is InteractiveElement)
                {
                    InteractiveElement IEX = (InteractiveElement)IE;
                    if (IEX.IsVisable == false)
                    {
                        IEX.IsVisable = true;
                        IEX.InvalidateVisual();
                    }
                }
            }
        }

        private void Menu_Export_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog
            {
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Filter = "PNG Image (*.png)|*.png" //|JPEG Image (*.jpeg)|*.jpeg";
            };

            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = sfd.FileName;

                // Kris, StackOverflow.org
                // https://stackoverflow.com/questions/8881865/saving-a-wpf-canvas-as-an-image
                Transform tf = DrawingCanvas.LayoutTransform;
                DrawingCanvas.LayoutTransform = null;
                Size size = new Size(DrawingCanvas.Width, DrawingCanvas.Height);

                RenderTargetBitmap rtb = new RenderTargetBitmap((int)DrawingCanvas.RenderSize.Width,
                                                                (int)DrawingCanvas.RenderSize.Height,
                                                                96d,
                                                                96d,
                                                                PixelFormats.Default);
                DrawingCanvas.Measure(size);
                DrawingCanvas.Arrange(new Rect(size));
                rtb.Render(DrawingCanvas);

                PngBitmapEncoder enc = new PngBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(rtb));

                using (var fs = System.IO.File.OpenWrite(path))
                {
                    enc.Save(fs);
                }
                DrawingCanvas.LayoutTransform = tf;
            }
        }
    }
}

using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace Comentsys.Community.Controls
{
    /// <summary>
    /// Stick Control
    /// </summary>
    public class Stick : Grid
    {
        private bool _hasCapture;
        private Ellipse _face;
        private Ellipse _knob;
        private double x = 0;
        private double y = 0;
        private double _m = 0;
        private double _res = 0;
        private double _width = 0;
        private double _height = 0;
        private double _alpha = 0;
        private double _alphaM = 0;
        private double _centreX = 0;
        private double _centreY = 0;
        private double _distance = 0;
        private double _oldAlphaM = -999.0;
        private double _oldDistance = -999.0;

        /// <summary>
        /// Middle
        /// </summary>
        private void Middle()
        {
            Canvas.SetLeft(_knob, (this.Width - _width) / 2);
            Canvas.SetTop(_knob, (this.Height - _height) / 2);
            _centreX = this.Width / 2;
            _centreY = this.Height / 2;
        }

        /// <summary>
        /// Move
        /// </summary>
        /// <param name="e"></param>
        private void Move(PointerRoutedEventArgs e)
        {
            double ToRadians(double angle) => Math.PI * angle / 180.0;
            double ToDegrees(double angle) => angle * (180.0 / Math.PI);
            x = e.GetCurrentPoint(this).Position.X;
            y = e.GetCurrentPoint(this).Position.Y;
            _res = Math.Sqrt((x - _centreX) *
            (x - _centreX) + (y - _centreY) * (y - _centreY));
            _m = (y - _centreY) / (x - _centreX);
            _alpha = ToDegrees(Math.Atan(_m) + Math.PI / 2);
            if (x < _centreX)
            {
                _alpha += 180.0;
            }
            else if (x == _centreX && y <= _centreY)
            {
                _alpha = 0.0;
            }
            else if (x == _centreX)
            {
                _alpha = 180.0;
            }
            if (_res > Radius)
            {
                x = _centreX + Math.Cos(ToRadians(_alpha) - Math.PI / 2) * Radius;
                y = _centreY + Math.Sin(ToRadians(_alpha) - Math.PI / 2) * Radius
                * ((_alpha % 180.0 == 0.0) ? -1.0 : 1.0);
                _res = Radius;
            }
            if (Math.Abs(_alpha - _alphaM) >= Sensitivity ||
                Math.Abs(_distance * Radius - _res) >= Sensitivity)
            {
                _alphaM = _alpha;
                _distance = _res / Radius;
            }
            if (_oldAlphaM != _alphaM ||
                _oldDistance != _distance)
            {
                Angle = _alphaM;
                Ratio = _distance;
                _oldAlphaM = _alphaM;
                _oldDistance = _distance;
                ValueChanged?.Invoke(Angle, Ratio);
            }
            Canvas.SetLeft(_knob, x - _width / 2);
            Canvas.SetTop(_knob, y - _height / 2);
        }

        /// <summary>
        /// Get Ellipse
        /// </summary>
        /// <param name="dimension"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        private Ellipse GetEllipse(double dimension, string path)
        {
            Ellipse ellipse = new Ellipse()
            {
                Height = dimension,
                Width = dimension
            };
            ellipse.SetBinding(Shape.FillProperty, new Binding()
            {
                Source = this,
                Path = new PropertyPath(path),
                Mode = BindingMode.TwoWay
            });
            return ellipse;
        }

        /// <summary>
        /// Has Capture
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HasCapture(object sender, PointerRoutedEventArgs e)
        {
            if (_hasCapture) Move(e);
        }

        /// <summary>
        /// Release Capture
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReleaseCapture(object sender, PointerRoutedEventArgs e)
        {
            _hasCapture = false;
            Middle();
        }

        /// <summary>
        /// Set Capture
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetCapture(object sender, PointerRoutedEventArgs e)
        => _hasCapture = true;

        /// <summary>
        /// Get Layout
        /// </summary>
        private void GetLayout()
        {          
            _knob = GetEllipse(Radius / 2, nameof(Foreground));
            _height = _knob.ActualHeight;
            _width = _knob.ActualWidth;
            this.Width = _width + Radius * 2;
            this.Height = _height + Radius * 2;
            _face = GetEllipse(this.Width, nameof(Fill));
            Middle();
            this.PointerExited -= null;
            this.PointerExited += ReleaseCapture;
            _knob.PointerReleased += ReleaseCapture;
            _knob.PointerPressed += SetCapture;
            _knob.PointerMoved += HasCapture;
            _knob.PointerExited += ReleaseCapture;
            ContentCanvas.Children.Clear();
            ContentCanvas.Children.Add(_face);
            Canvas canvas = new Canvas()
            {
                Width = this.Width,
                Height = this.Height
            };
            canvas.Children.Add(_knob);
            ContentCanvas.Children.Add(canvas);
        }

        /// <summary>
        /// Content Canvas
        /// </summary>
        internal Canvas ContentCanvas { get; set; }

        /// <summary>
        /// Stick Control
        /// </summary>
        public Stick()
        {
            ContentCanvas = new Canvas();
            GetLayout();
            this.Children.Add(ContentCanvas);
        }

        /// <summary>
        /// ValueChangedEventHandler
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="ratio"></param>
        public delegate void ValueChangedEventHandler(double angle, double ratio);

        /// <summary>
        /// Value Changed Event
        /// </summary>
        public event ValueChangedEventHandler ValueChanged;

        /// <summary>
        /// Foreground Property
        /// </summary>
        public static readonly DependencyProperty ForegroundProperty =
        DependencyProperty.Register(nameof(Foreground), typeof(Brush),
        typeof(Stick), new PropertyMetadata(new SolidColorBrush(Colors.White)));

        /// <summary>
        /// Fill Property
        /// </summary>
        public static readonly DependencyProperty FillProperty =
        DependencyProperty.Register(nameof(Fill), typeof(Brush),
        typeof(Stick), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        /// <summary>
        /// Radius Property
        /// </summary>
        public static readonly DependencyProperty RadiusProperty =
        DependencyProperty.Register(nameof(Radius), typeof(int),
        typeof(Stick), new PropertyMetadata(100));

        /// <summary>
        /// Sensitivity Property
        /// </summary>
        public static readonly DependencyProperty SensitivityProperty =
        DependencyProperty.Register(nameof(Sensitivity), typeof(double),
        typeof(Stick), new PropertyMetadata(0.0));

        /// <summary>
        /// Angle Property
        /// </summary>
        public static readonly DependencyProperty AngleProperty =
        DependencyProperty.Register(nameof(Angle), typeof(double),
        typeof(Stick), null);

        /// <summary>
        /// Ratio Property
        /// </summary>
        public static readonly DependencyProperty RatioProperty =
        DependencyProperty.Register(nameof(Ratio), typeof(double),
        typeof(Stick), null);

        /// <summary>
        /// Stick Foreground
        /// </summary>
        public Brush Foreground
        {
            get => (Brush)GetValue(ForegroundProperty);
            set => SetValue(ForegroundProperty, value);
        }

        /// <summary>
        /// Stick Fill
        /// </summary>
        public Brush Fill
        {
            get { return (Brush)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }

        /// <summary>
        /// Radius
        /// </summary>
        public int Radius
        {
            get { return (int)GetValue(RadiusProperty); }
            set { SetValue(RadiusProperty, value); GetLayout(); }
        }

        /// <summary>
        /// Controller Sensitivity
        /// </summary>
        public double Sensitivity
        {
            get => (double)GetValue(SensitivityProperty);
            set => SetValue(SensitivityProperty, value);
        }

        /// <summary>
        /// Stick Rotation Angle
        /// </summary>
        public double Angle
        {
            get { return (double)GetValue(AngleProperty); }
            set { SetValue(AngleProperty, value); }
        }

        /// <summary>
        /// Stick Movement Ratio
        /// </summary>
        public double Ratio
        {
            get { return (double)GetValue(RatioProperty); }
            set { SetValue(RatioProperty, value); }
        }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using Windows.Foundation;
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
    /// Dial Control
    /// </summary>
    public class Dial : Grid
    {
        private const int size = 300;
        private const double dial_min = 0;
        private const double dial_max = 360;

        private static Grid _dial;
        private static Grid _knob;
        private bool _hasCapture = false;

        /// <summary>
        /// Get Angle Quadrant
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        private double GetAngleQuadrant(
            double width, double height, Point point)
        {
            double radius = width / 2;
            Point centre = new Point(radius, height / 2);
            Point start = new Point(0, height / 2);
            double triangleTop = Math.Sqrt(Math.Pow((point.X - centre.X), 2)
              + Math.Pow((centre.Y - point.Y), 2));
            double triangleHeight = (point.Y > centre.Y) ?
              point.Y - centre.Y : centre.Y - point.Y;
            return ((triangleHeight * Math.Sin(90)) / triangleTop) * 100;
        }

        /// <summary>
        /// Get Angle
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        private double GetAngle(Point point)
        {
            double diameter = _dial.ActualWidth;
            double height = _dial.ActualHeight;
            double radius = diameter / 2;
            double rotation = GetAngleQuadrant(diameter, height, point);
            if ((point.X > radius) && 
            (point.Y <= radius))
            {
                rotation = 90.0 + (90.0 - rotation);
            }
            else if ((point.X > radius) && 
            (point.Y > radius))
            {
                rotation = 180.0 + rotation;
            }
            else if ((point.X < radius) && 
            (point.Y > radius))
            {
                rotation = 270.0 + (90.0 - rotation);
            }
            return rotation;
        }

        /// <summary>
        /// Set Position
        /// </summary>
        /// <param name="rotation"></param>
        private void SetPosition(double rotation)
        {
            if (Minimum > dial_min && 
                Maximum > dial_min && 
                Minimum < dial_max && 
                Maximum <= dial_max)
            {
                if (rotation < Minimum) { rotation = Minimum; }
                if (rotation > Maximum) { rotation = Maximum; }
            }
            ((RotateTransform)_knob.RenderTransform).Angle = rotation;
            Value = rotation;
            ValueChanged?.Invoke(Value);
        }

        /// <summary>
        /// Release Capture
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReleaseCapture(object sender, PointerRoutedEventArgs e) 
        => _hasCapture = false;

        /// <summary>
        /// Set Capture
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetCapture(object sender, PointerRoutedEventArgs e)
        {
            _hasCapture = true;
            SetPosition(GetAngle(e.GetCurrentPoint(_dial).Position));
        }

        /// <summary>
        /// Has Capture
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HasCapture(object sender, PointerRoutedEventArgs e)
        {
            if (_hasCapture)
            {
                SetPosition(GetAngle(e.GetCurrentPoint(_dial).Position));
            }
        }

        private void Layout()
        {
            Ellipse ellipse = new Ellipse()
            {
                Height = size,
                Width = size
            };
            ellipse.SetBinding(Ellipse.FillProperty, new Binding()
            {
                Path = new PropertyPath(nameof(DialBackground)),
                Mode = BindingMode.TwoWay,
                Source = this
            });
            _dial.Children.Add(ellipse);
            Rectangle rect = new Rectangle()
            {
                Height = 40,
                Width = 150,
                RadiusX = 20,
                RadiusY = 20,
                Margin = new Thickness(5, 0, 145, 0)
            };
            rect.SetBinding(Ellipse.FillProperty, new Binding()
            {
                Path = new PropertyPath(nameof(DialForeground)),
                Mode = BindingMode.TwoWay,
                Source = this
            });
            _knob = new Grid()
            {
                RenderTransformOrigin = new Point(0.5, 0.5),
                RenderTransform = new RotateTransform()
                {
                    Angle = 0
                }
            };
            _knob.Children.Add(rect);
            _dial.Children.Add(_knob);
        }

        /// <summary>
        /// Dial Control
        /// </summary>
        public Dial()
        {
            _dial = new Grid()
            {
                Height = size,
                Width = size
            };
            Layout();
            _dial.PointerPressed += SetCapture;
            _dial.PointerMoved += HasCapture;
            _dial.PointerExited += ReleaseCapture;
            _dial.PointerReleased += ReleaseCapture;
            Viewbox viewbox = new Viewbox()
            {
                Child = _dial
            };
            this.Children.Add(viewbox);
            if (Minimum > 0 && Minimum < 360) SetPosition(Minimum);
        }

        /// <summary>
        /// ValueChangedEventHandler
        /// </summary>
        /// <param name="value"></param>
        public delegate void ValueChangedEventHandler(double value);

        /// <summary>
        /// Value Changed Event
        /// </summary>
        public event ValueChangedEventHandler ValueChanged;

        /// <summary>
        /// DialBackground Property
        /// </summary>
        public static readonly DependencyProperty DialBackgroundProperty =
        DependencyProperty.Register(nameof(DialBackground), typeof(Brush),
        typeof(Dial), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        /// <summary>
        /// DialForeground Property
        /// </summary>
        public static readonly DependencyProperty DialForegroundProperty =
        DependencyProperty.Register(nameof(DialForeground), typeof(Brush),
        typeof(Dial), new PropertyMetadata(new SolidColorBrush(Colors.White)));

        /// <summary>
        /// Value Property
        /// </summary>
        private readonly DependencyProperty ValueProperty =
        DependencyProperty.Register(nameof(Value), typeof(double),
        typeof(Dial), new PropertyMetadata(0));

        /// <summary>
        /// Minimum Property
        /// </summary>
        private readonly DependencyProperty MinimumProperty =
        DependencyProperty.Register(nameof(Minimum), typeof(double),
        typeof(Dial), new PropertyMetadata(dial_min));

        /// <summary>
        /// Maximum Property
        /// </summary>
        private readonly DependencyProperty MaximumProperty =
        DependencyProperty.Register(nameof(Maximum), typeof(double),
        typeof(Dial), new PropertyMetadata(dial_max));

        /// <summary>
        /// Dial Background
        /// </summary>
        public Brush DialBackground
        {
            get => (Brush)GetValue(DialBackgroundProperty);
            set => SetValue(DialBackgroundProperty, value);
        }

        /// <summary>
        /// Dial Foreground
        /// </summary>
        public Brush DialForeground
        {
            get => (Brush)GetValue(DialForegroundProperty);
            set => SetValue(DialForegroundProperty, value);
        }

        /// <summary>
        /// Dial Value between 0 - 360
        /// </summary>
        [Range(dial_min, dial_max)]
        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        /// <summary>
        /// Dial Minimum between 0 - 360
        /// </summary>
        [Range(dial_min, dial_max)]
        public double Minimum
        {
            get => (double)GetValue(MinimumProperty);
            set => SetValue(MinimumProperty, value);
        }

        /// <summary>
        /// Dial Maximum between 0 - 360
        /// </summary>
        [Range(dial_min, dial_max)]
        public double Maximum
        {
            get => (double)GetValue(MaximumProperty);
            set => SetValue(MaximumProperty, value);
        }
    }
}

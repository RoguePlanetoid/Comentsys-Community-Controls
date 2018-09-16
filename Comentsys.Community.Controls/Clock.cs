using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace Comentsys.Community.Controls
{
    /// <summary>
    /// Clock Control
    /// </summary>
    public class Clock : Grid
    {
        private const int second_width = 1;
        private const int minute_width = 5;
        private const int hour_width = 8;
        private double _diameter = 0;

        private DispatcherTimer _timer = new DispatcherTimer();
        private Canvas _markers = new Canvas();
        private Canvas _face = new Canvas();
        private Rectangle _secondHand;
        private Rectangle _minuteHand;
        private Rectangle _hourHand;
        private int _secondHeight;
        private int _minuteHeight;
        private int _hourHeight;

        /// <summary>
        /// GetHand
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="radiusX"></param>
        /// <param name="radiusY"></param>
        /// <param name="fill"></param>
        /// <param name="thickness"></param>
        /// <returns></returns>
        private Rectangle GetHand(
        double width, double height, 
        double radiusX, double radiusY, 
        string path, double thickness)
        {
            Rectangle element = new Rectangle
            {
                Width = width,
                Height = height,
                RadiusX = radiusX,
                RadiusY = radiusY,
                StrokeThickness = thickness,
                Visibility = Visibility.Collapsed
            };
            element.SetBinding(Rectangle.FillProperty, new Binding()
            {
                Path = new PropertyPath(path),
                Mode = BindingMode.TwoWay,
                Source = this
            });
            return element;
        }

        /// <summary>
        /// Get Transform Group
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private TransformGroup GetTransformGroup(
        double angle, double x, double y)
        {
            TransformGroup group = new TransformGroup();
            TranslateTransform firstTranslate = new TranslateTransform
            {
                X = x, Y = y
            };
            group.Children.Add(firstTranslate);
            RotateTransform rotate = new RotateTransform
            {
                Angle = angle
            };
            group.Children.Add(rotate);
            TranslateTransform secondTranslate = new TranslateTransform
            {
                X = _diameter / 2, Y = _diameter / 2
            };
            group.Children.Add(secondTranslate);
            return group;
        }

        /// <summary>
        /// Set Second Hand
        /// </summary>
        /// <param name="seconds"></param>
        private void SetSecondHand(int seconds)
        {
            _secondHand.Visibility = (ShowMinuteHand ? 
            Visibility.Visible : Visibility.Collapsed);
            _secondHand.RenderTransform = GetTransformGroup(
            seconds * 6,             
            -second_width / 2, -_secondHeight + 4.25);
        }

        /// <summary>
        /// Set Minute Hand
        /// </summary>
        /// <param name="minutes"></param>
        /// <param name="seconds"></param>
        private void SetMinuteHand(int minutes, int seconds)
        {
            _minuteHand.Visibility = (ShowMinuteHand ? 
            Visibility.Visible : Visibility.Collapsed);
            _minuteHand.RenderTransform = GetTransformGroup(
            6 * minutes + seconds / 10,
            -minute_width / 2, -_minuteHeight + 4.25);
        }

        /// <summary>
        /// Set Hour Hand
        /// </summary>
        /// <param name="hours"></param>
        /// <param name="minutes"></param>
        /// <param name="seconds"></param>
        private void SetHourHand(int hours, int minutes, int seconds)
        {
            _hourHand.Visibility = (ShowHourHand ? 
            Visibility.Visible : Visibility.Collapsed);
            _hourHand.RenderTransform = GetTransformGroup(
            30 * hours + minutes / 2 + seconds / 120,
            -hour_width / 2, -_hourHeight + 4.25);
        }

        /// <summary>
        /// Get Middle
        /// </summary>
        private Ellipse GetMiddle()
        {
            Ellipse middle = new Ellipse()
            {
                Height = 15, Width = 15,
            };
            middle.SetBinding(Ellipse.FillProperty, new Binding()
            {
                Path = new PropertyPath(nameof(SecondHandForeground)),
                Mode = BindingMode.TwoWay,
                Source = this
            });
            Canvas.SetLeft(middle, (_diameter - 15) / 2);
            Canvas.SetTop(middle, (_diameter - 15) / 2);
            return middle;
        }

        /// <summary>
        /// Layout
        /// </summary>
        /// <param name="canvas"></param>
        private void Layout(ref Canvas canvas)
        {
            canvas.Children.Clear();
            _diameter = canvas.Width;
            double inner = _diameter - 15;
            Ellipse rim = new Ellipse
            {
                Height = _diameter,
                Width = _diameter,
                StrokeThickness = 20
            };
            rim.SetBinding(Ellipse.StrokeProperty, new Binding()
            {
                Path = new PropertyPath(nameof(RimBackground)),
                Mode = BindingMode.TwoWay,
                Source = this
            });
            canvas.Children.Add(rim);
            _markers.Children.Clear();
            _markers.Width = inner;
            _markers.Height = inner;
            for (int i = 0; i < 60; i++)
            {
                Rectangle marker = new Rectangle();
                marker.SetBinding(Rectangle.FillProperty, new Binding()
                {
                    Path = new PropertyPath(nameof(RimForeground)),
                    Mode = BindingMode.TwoWay,
                    Source = this
                });
                if ((i % 5) == 0)
                {
                    marker.Width = 3;
                    marker.Height = 8;
                    marker.RenderTransform = GetTransformGroup(
                    i * 6, -(marker.Width / 2),
                    -(marker.Height * 2 + 4.5 
                    - rim.StrokeThickness / 2 - inner / 2 - 6));
                }
                else
                {
                    marker.Width = 1;
                    marker.Height = 4;
                    marker.RenderTransform = GetTransformGroup(
                    i * 6, -(marker.Width / 2),
                    -(marker.Height * 2 + 12.75 
                    - rim.StrokeThickness / 2 - inner / 2 - 8));
                }
                _markers.Children.Add(marker);
            }
            canvas.Children.Add(_markers);
            _face.Width = _diameter;
            _face.Height = _diameter;
            canvas.Children.Add(_face);
            _hourHeight = (int)_diameter / 2 - 60;
            _minuteHeight = (int)_diameter / 2 - 40;
            _secondHeight = (int)_diameter / 2 - 20;
            canvas.Children.Add(_hourHand = GetHand(hour_width, _hourHeight,
                3, 3, nameof(HourHandForeground), 0.6));
            canvas.Children.Add(_minuteHand = GetHand(minute_width, _minuteHeight,
                2, 2, nameof(MinuteHandForeground), 0.6));
            canvas.Children.Add(_secondHand = GetHand(second_width, _secondHeight,
                0, 0, nameof(SecondHandForeground), 0));
            canvas.Children.Add(GetMiddle());
        }

        /// <summary>
        /// Set Clock
        /// </summary>
        private void SetClock()
        {
            if (IsCurrentTime) Value = DateTime.Now;
            SetHourHand(Value.Hour, Value.Minute, Value.Second);
            SetMinuteHand(Value.Minute, Value.Second);
            SetSecondHand(Value.Second);
        }

        /// <summary>
        /// Clock Control
        /// </summary>
        public Clock()
        {
            Canvas canvas = new Canvas()
            {
                Height = 300, Width = 300
            };
            Layout(ref canvas);
            Viewbox viewbox = new Viewbox()
            {
                Child = canvas
            };
            this.Children.Add(viewbox);
            SetClock();
            _timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromMilliseconds(250)
            };
            _timer.Tick += (object s, object obj) => SetClock();
            _timer.Start();
        }

        /// <summary>
        /// RimBackground Property
        /// </summary>
        public static readonly DependencyProperty RimBackgroundProperty =
        DependencyProperty.Register(nameof(RimBackground), typeof(Brush),
        typeof(Clock), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        /// <summary>
        /// RimForeground Property
        /// </summary>
        public static readonly DependencyProperty RimForegroundProperty =
        DependencyProperty.Register(nameof(RimForeground), typeof(Brush),
        typeof(Clock), new PropertyMetadata(new SolidColorBrush(Colors.White)));

        /// <summary>
        /// SecondHandForeground Property
        /// </summary>
        public static readonly DependencyProperty SecondHandForegroundProperty =
        DependencyProperty.Register(nameof(SecondHandForeground), typeof(Brush),
        typeof(Clock), new PropertyMetadata(new SolidColorBrush(Colors.Red)));

        /// <summary>
        /// MinuteHandForeground Property
        /// </summary>
        public static readonly DependencyProperty MinuteHandForegroundProperty =
        DependencyProperty.Register(nameof(MinuteHandForeground), typeof(Brush),
        typeof(Clock), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        /// <summary>
        /// HourHandForeground Property
        /// </summary>
        public static readonly DependencyProperty HourHandForegroundProperty =
        DependencyProperty.Register(nameof(HourHandForeground), typeof(Brush),
        typeof(Clock), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        /// <summary>
        /// ShowSecondHand Property
        /// </summary>
        public static readonly DependencyProperty ShowSecondHandProperty =
        DependencyProperty.Register(nameof(ShowSecondHand), typeof(bool),
        typeof(Clock), new PropertyMetadata(true));

        /// <summary>
        /// ShowMinuteHand Property
        /// </summary>
        public static readonly DependencyProperty ShowMinuteHandProperty =
        DependencyProperty.Register(nameof(ShowMinuteHand), typeof(bool),
        typeof(Clock), new PropertyMetadata(true));

        /// <summary>
        /// ShowHourHand Property
        /// </summary>
        public static readonly DependencyProperty ShowHourHandProperty =
        DependencyProperty.Register(nameof(ShowHourHand), typeof(bool),
        typeof(Clock), new PropertyMetadata(true));

        /// <summary>
        /// IsCurentTime Property
        /// </summary>
        public static readonly DependencyProperty IsCurrentTimeProperty =
        DependencyProperty.Register(nameof(IsCurrentTime), typeof(bool),
        typeof(Clock), new PropertyMetadata(true));

        /// <summary>
        /// Value Property
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register(nameof(Value), typeof(DateTime),
        typeof(Clock), new PropertyMetadata(DateTime.Now));

        /// <summary>
        /// Rim Background
        /// </summary>
        public Brush RimBackground
        {
            get => (Brush)GetValue(RimBackgroundProperty);
            set => SetValue(RimBackgroundProperty, value);
        }

        /// <summary>
        /// Rim Foreground
        /// </summary>
        public Brush RimForeground
        {
            get => (Brush)GetValue(RimForegroundProperty);
            set => SetValue(RimForegroundProperty, value);
        }

        /// <summary>
        /// Second Hand Foreground
        /// </summary>
        public Brush SecondHandForeground
        {
            get => (Brush)GetValue(SecondHandForegroundProperty);
            set => SetValue(SecondHandForegroundProperty, value);
        }

        /// <summary>
        /// Minute Hand Foreground
        /// </summary>
        public Brush MinuteHandForeground
        {
            get => (Brush)GetValue(MinuteHandForegroundProperty);
            set => SetValue(MinuteHandForegroundProperty, value);
        }

        /// <summary>
        /// Hour Hand Foreground
        /// </summary>
        public Brush HourHandForeground
        {
            get => (Brush)GetValue(HourHandForegroundProperty);
            set => SetValue(HourHandForegroundProperty, value);
        }

        /// <summary>
        /// Show Second Hand
        /// </summary>
        public bool ShowSecondHand
        {
            get => (bool)GetValue(ShowSecondHandProperty);
            set => SetValue(ShowSecondHandProperty, value);
        }

        /// <summary>
        /// Show Minute Hand
        /// </summary>
        public bool ShowMinuteHand
        {
            get => (bool)GetValue(ShowMinuteHandProperty);
            set => SetValue(ShowMinuteHandProperty, value);
        }

        /// <summary>
        /// Show Hour Hand
        /// </summary>
        public bool ShowHourHand
        {
            get => (bool)GetValue(ShowHourHandProperty);
            set => SetValue(ShowHourHandProperty, value);
        }

        /// <summary>
        /// Is Current Time 
        /// </summary>
        public bool IsCurrentTime
        {
            get => (bool)GetValue(IsCurrentTimeProperty);
            set => SetValue(IsCurrentTimeProperty, value);
        }

        /// <summary>
        /// Clock Value Date Time
        /// </summary>
        public DateTime Value
        {
            get => (DateTime)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }
    }
}
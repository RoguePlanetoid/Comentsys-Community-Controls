using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace Comentsys.Community.Controls
{
    /// <summary>
    /// Segment Control
    /// </summary>
    public class Segment : Grid
    {
        private readonly byte[][] layout =
        {
                      // a, b, c, d, e, f, g
            new byte[] { 1, 1, 1, 1, 1, 1, 0 }, // 0
            new byte[] { 0, 1, 1, 0, 0, 0, 0 }, // 1
            new byte[] { 1, 1, 0, 1, 1, 0, 1 }, // 2
            new byte[] { 1, 1, 1, 1, 0, 0, 1 }, // 3
            new byte[] { 0, 1, 1, 0, 0, 1, 1 }, // 4
            new byte[] { 1, 0, 1, 1, 0, 1, 1 }, // 5
            new byte[] { 1, 0, 1, 1, 1, 1, 1 }, // 6
            new byte[] { 1, 1, 1, 0, 0, 0, 0 }, // 7
            new byte[] { 1, 1, 1, 1, 1, 1, 1 }, // 8
            new byte[] { 1, 1, 1, 0, 0, 1, 1 }, // 9
            new byte[] { 0, 0, 0, 0, 0, 0, 1 }, // 10 (Dash)
            new byte[] { 0, 0, 0, 0, 0, 0, 0 }, // 11 (None)
            new byte[] { 0, 0, 0, 0, 0, 0, 0 }, // 12 (Colon)
        }; // Segment Layout

        private const int none = 11;
        private const int dash = 10;
        private const int colon = 12;
        private const int width = 5;
        private const int height = 25;
        private const string time_format = "HH:mm:ss";
        private const string date_format = "dd-MM-yyyy";
        private const string time_date_format = "HH:mm:ss dd-MM-yyyy";

        private DispatcherTimer _timer = new DispatcherTimer();
        private StackPanel _panel;
        private int _count;

        /// <summary>
        /// Add Element
        /// </summary>
        /// <param name="name"></param>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private Rectangle AddElement(string name, 
        int left, int top, 
        int width, int height)
        {
            Rectangle rect = new Rectangle()
            {
                Tag = name,
                Width = width,
                Height = height,
                RadiusX = 2,
                RadiusY = 2
            };
            rect.SetBinding(Ellipse.FillProperty, new Binding()
            {
                Path = new PropertyPath(nameof(Foreground)),
                Mode = BindingMode.TwoWay,
                Source = this
            });
            Canvas.SetLeft(rect, left);
            Canvas.SetTop(rect, top);
            return rect;
        }

        /// <summary>
        /// Add Segment
        /// </summary>
        /// <param name="name"></param>
        private void AddSegment(string name)
        {
            Canvas segment = new Canvas()
            {
                Margin = new Thickness(2),
                Tag = name,
                Height = 50,
                Width = 25
            };
            segment.Children.Add(AddElement($"{name}.a", 
            width, 0, height, width));
            segment.Children.Add(AddElement($"{name}.h", 
            width + width + width, width + width + width, width, width));
            segment.Children.Add(AddElement($"{name}.f", 
            0, width, width, height));
            segment.Children.Add(AddElement($"{name}.b", 
            height + width, width, width, height));
            segment.Children.Add(AddElement($"{name}.g", 
            width, height + width, height, width));
            segment.Children.Add(AddElement($"{name}.e", 
            0, height + width + width, width, height));
            segment.Children.Add(AddElement($"{name}.c", 
            height + width, height + width + width, width, height));
            segment.Children.Add(AddElement($"{name}.i", 
            width + width + width, height + width + width + width + width, width, width));
            segment.Children.Add(AddElement($"{name}.d", 
            width, height + height + width + width, height, width));
            _panel.Children.Add(segment);
        }

        /// <summary>
        /// Set Layout
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private Canvas SetSegment(string name)
        {
            return _panel.Children.Cast<Canvas>()
            .FirstOrDefault(f => (string)f.Tag == name);
        }

        /// <summary>
        /// Set Element
        /// </summary>
        /// <param name="layout"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private Rectangle SetElement(Canvas layout, string name)
        {
            return layout.Children.Cast<Rectangle>()
            .FirstOrDefault(f => (string)f.Tag == name);
        }

        /// <summary>
        /// Set Segment
        /// </summary>
        /// <param name="name"></param>
        /// <param name="digit"></param>
        private void SetItem(string name, int digit)
        {
            Canvas layout = SetSegment(name);
            byte[] values = this.layout[digit];
            SetElement(layout, $"{name}.a").Opacity = values[0];
            SetElement(layout, $"{name}.b").Opacity = values[1];
            SetElement(layout, $"{name}.c").Opacity = values[2];
            SetElement(layout, $"{name}.d").Opacity = values[3];
            SetElement(layout, $"{name}.e").Opacity = values[4];
            SetElement(layout, $"{name}.f").Opacity = values[5];
            SetElement(layout, $"{name}.g").Opacity = values[6];
            SetElement(layout, $"{name}.h").Opacity = digit > none ? 1 : 0;
            SetElement(layout, $"{name}.i").Opacity = digit > none ? 1 : 0;
        }

        /// <summary>
        /// Get Layout
        /// </summary>
        private void SetLayout()
        {
            char[] array = Value.ToCharArray();
            int length = array.Length;
            List<int> list = Enumerable.Range(0, length).ToList();
            if (_count != length)
            {
                _panel.Children.Clear();
                foreach (int item in list)
                {
                    AddSegment(item.ToString());
                }
                _count = length;
            }
            foreach (int item in list)
            {
                string val = array[item].ToString();
                int digit = none;
                switch(val)
                {
                    case " ":
                        digit = none;
                        break;
                    case "-":
                        digit = dash;
                        break;
                    case ":":
                        digit = colon;
                        break;
                    default:
                        digit = int.Parse(val);
                        break;
                }
                SetItem(item.ToString(), digit);
            }
        }

        /// <summary>
        /// SetValue
        /// </summary>
        private void SetValue()
        {
            if (Source != Sources.Value)
            {
                string format = string.Empty;
                switch (Source)
                {
                    case Sources.Time:
                        format = time_format;
                        break;
                    case Sources.Date:
                        format = date_format;
                        break;
                    case Sources.TimeDate:
                        format = time_date_format;
                        break;
                }
                Value = DateTime.Now.ToString(format);
                SetLayout();
            }
        }

        /// <summary>
        /// Segment Control
        /// </summary>
        public Segment()
        {
            _panel = new StackPanel()
            {
                Spacing = 10,
                Orientation = Orientation.Horizontal
            };
            Viewbox viewbox = new Viewbox()
            {
                Child = _panel
            };
            this.Children.Add(viewbox);
            SetValue();
            _timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromMilliseconds(250)
            };
            _timer.Tick += (object sender, object e) => SetValue();
            _timer.Start();
        }

        /// <summary>
        /// Sources
        /// </summary>
        public enum Sources
        {
            Value = 0,
            Time = 1,
            Date = 2,
            TimeDate = 3
        }

        /// <summary>
        /// Foreground Property
        /// </summary>
        public static readonly DependencyProperty ForegroundProperty =
        DependencyProperty.Register(nameof(Foreground), typeof(Brush),
        typeof(Segment), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        /// <summary>
        /// SourceProperty
        /// </summary>
        public static readonly DependencyProperty SourceProperty =
        DependencyProperty.Register(nameof(Source), typeof(Sources),
        typeof(Segment), new PropertyMetadata(Sources.Time));

        /// <summary>
        /// Value Property
        /// </summary>
        private readonly DependencyProperty ValueProperty =
        DependencyProperty.Register(nameof(Value), typeof(string),
        typeof(Segment), new PropertyMetadata(string.Empty));

        /// <summary>
        /// Foreground
        /// </summary>
        public Brush Foreground
        {
            get => (Brush)GetValue(ForegroundProperty);
            set => SetValue(ForegroundProperty, value);
        }

        /// <summary>
        /// Segment Source
        /// </summary>
        public Sources Source
        {
            get => (Sources)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        /// <summary>
        /// Segment Value
        /// </summary>
        public string Value
        {
            get => (string)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }
    }
}

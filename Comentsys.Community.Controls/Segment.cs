using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Markup;
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
        /// Add Ellipse
        /// </summary>
        /// <param name="name"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        private Path AddPath(string name, int row, int column)
        {
            Path path = new Path()
            {
                Tag = name,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Data = new EllipseGeometry()
                {
                    RadiusX = 8,
                    RadiusY = 8
                }
            };
            path.SetBinding(Path.FillProperty, new Binding()
            {
                Path = new PropertyPath(nameof(Foreground)),
                Mode = BindingMode.TwoWay,
                Source = this
            });
            Grid.SetRow(path, row);
            Grid.SetColumn(path, column);
            return path;
        }

        /// <summary>
        /// Add Path
        /// </summary>
        /// <param name="name"></param>
        /// <param name="data"></param>
        /// <param name="margin"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="columnSpan"></param>
        /// <returns></returns>
        private Path AddPath(
            string name, string data,
            Thickness? margin,
            int? row, int? column,
            int? columnSpan)
        {
            Path path = XamlReader.Load(
            $"<Path xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'><Path.Data>{data}</Path.Data></Path>"
            ) as Path;
            path.Tag = name;
            if (margin != null) path.Margin = margin.Value;
            path.SetBinding(Path.FillProperty, new Binding()
            {
                Path = new PropertyPath(nameof(Foreground)),
                Mode = BindingMode.TwoWay,
                Source = this
            });
            if (row != null) Grid.SetRow(path, row.Value);
            if (column != null) Grid.SetColumn(path, column.Value);
            if (columnSpan != null) Grid.SetColumnSpan(path, columnSpan.Value);
            return path;
        }

        /// <summary>
        /// Add Segment
        /// </summary>
        /// <param name="name"></param>
        private void AddSegment(string name)
        {
            Thickness margin = new Thickness(8, 0, 8, 0);
            GridLength auto = new GridLength(100, GridUnitType.Auto);
            GridLength star = new GridLength(100, GridUnitType.Star);
            Grid segment = new Grid()
            {
                Margin = new Thickness(2),
                Tag = name,
                Height = 188,
                Width = 86,
            };
            // Columns
            segment.ColumnDefinitions.Add(new ColumnDefinition() { Width = auto });
            segment.ColumnDefinitions.Add(new ColumnDefinition() { Width = star });
            segment.ColumnDefinitions.Add(new ColumnDefinition() { Width = auto });
            // Rows
            segment.RowDefinitions.Add(new RowDefinition() { Height = auto });
            segment.RowDefinitions.Add(new RowDefinition() { Height = star });
            segment.RowDefinitions.Add(new RowDefinition() { Height = auto });
            segment.RowDefinitions.Add(new RowDefinition() { Height = star });
            segment.RowDefinitions.Add(new RowDefinition() { Height = auto });
            // Paths
            segment.Children.Add(AddPath($"{name}.a",
            "M 6,0 64,0, 70,6 60,16 10,16 0,6 z",
            margin, 0, null, 3));
            segment.Children.Add(AddPath($"{name}.h",
            1, 1));
            segment.Children.Add(AddPath($"{name}.f",
            "M 6,0 16,10 16,60 6,70 0,64 0,6 z",
            null, 1, 0, null));
            segment.Children.Add(AddPath($"{name}.b",
            "M 0,10 10,0 16,8 16,64 10,70 0,60 0,10 0,16 z",
            null, 1, 3, null));
            segment.Children.Add(AddPath($"{name}.g",
            "M 8,0 60,0,68,8 60,16 8,16 0,8 z",
            margin, 2, null, 3));
            segment.Children.Add(AddPath($"{name}.e",
            "M 6,0 16,10 16,60 6,70 0,64 0,6 z",
            null, 3, 0, null));
            segment.Children.Add(AddPath($"{name}.c",
            "M 0,10 10,0 16,8 16,64 10,70 0,60 0,10 0,16 z",
            null, 3, 3, null));
            segment.Children.Add(AddPath($"{name}.i",
            3, 1));
            segment.Children.Add(AddPath($"{name}.d",
            "M 10,0 60,0 70,10 64,16 6,16 0,10 z",
            margin, 4, null, 3));
            _panel.Children.Add(segment);
        }

        /// <summary>
        /// Set Layout
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private Grid SetSegment(string name)
        {
            return _panel.Children.Cast<Grid>()
            .FirstOrDefault(f => (string)f.Tag == name);
        }

        /// <summary>
        /// Set Element
        /// </summary>
        /// <param name="layout"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private Path SetElement(Grid layout, string name)
        {
            return layout.Children.Cast<Path>()
            .FirstOrDefault(f => (string)f.Tag == name);
        }

        /// <summary>
        /// Set Segment
        /// </summary>
        /// <param name="name"></param>
        /// <param name="digit"></param>
        private void SetItem(string name, int digit)
        {
            Grid layout = SetSegment(name);
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
                switch (val)
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
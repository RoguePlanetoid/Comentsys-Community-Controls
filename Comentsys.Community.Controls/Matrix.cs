using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace Comentsys.Community.Controls
{
    /// <summary>
    /// Matrix Control
    /// </summary>
    public class Matrix : Grid
    {
        private static readonly byte[][] layout =
        {
            new byte[] {
            0,0,0,0,0,0,0,0,
            0,1,1,1,1,1,1,0,
            0,1,1,0,0,1,1,0,
            0,1,1,0,0,1,1,0,
            0,1,1,0,0,1,1,0,
            0,1,1,1,1,1,1,0,
            0,0,0,0,0,0,0,0
            }, // 0
            new byte[] {
            0,0,0,0,0,0,0,0,
            0,0,0,1,1,0,0,0,
            0,1,1,1,1,0,0,0,
            0,0,0,1,1,0,0,0,
            0,0,0,1,1,0,0,0,
            0,0,0,1,1,0,0,0,
            0,0,0,0,0,0,0,0
            }, // 1 
            new byte[] {
            0,0,0,0,0,0,0,0,
            0,1,1,1,1,1,1,0,
            0,0,0,0,0,1,1,0,
            0,1,1,1,1,1,1,0,
            0,1,1,0,0,0,0,0,
            0,1,1,1,1,1,1,0,
            0,0,0,0,0,0,0,0
            }, // 2
            new byte[] {
            0,0,0,0,0,0,0,0,
            0,1,1,1,1,1,1,0,
            0,0,0,0,0,1,1,0,
            0,1,1,1,1,1,1,0,
            0,0,0,0,0,1,1,0,
            0,1,1,1,1,1,1,0,
            0,0,0,0,0,0,0,0
            }, // 3
            new byte[] {
            0,0,0,0,0,0,0,0,
            0,1,1,0,0,1,1,0,
            0,1,1,0,0,1,1,0,
            0,1,1,1,1,1,1,0,
            0,0,0,0,0,1,1,0,
            0,0,0,0,0,1,1,0,
            0,0,0,0,0,0,0,0
            }, // 4
            new byte[] {
            0,0,0,0,0,0,0,0,
            0,1,1,1,1,1,1,0,
            0,1,1,0,0,0,0,0,
            0,1,1,1,1,1,1,0,
            0,0,0,0,0,1,1,0,
            0,1,1,1,1,1,1,0,
            0,0,0,0,0,0,0,0
            }, // 5
            new byte[] {
            0,0,0,0,0,0,0,0,
            0,1,1,1,1,1,1,0,
            0,1,1,0,0,0,0,0,
            0,1,1,1,1,1,1,0,
            0,1,1,0,0,1,1,0,
            0,1,1,1,1,1,1,0,
            0,0,0,0,0,0,0,0
            }, // 6
            new byte[] {
            0,0,0,0,0,0,0,0,
            0,1,1,1,1,1,1,0,
            0,0,0,0,0,1,1,0,
            0,0,0,0,0,1,1,0,
            0,0,0,0,0,1,1,0,
            0,0,0,0,0,1,1,0,
            0,0,0,0,0,0,0,0
            }, // 7
            new byte[] {
            0,0,0,0,0,0,0,0,
            0,1,1,1,1,1,1,0,
            0,1,1,0,0,1,1,0,
            0,1,1,1,1,1,1,0,
            0,1,1,0,0,1,1,0,
            0,1,1,1,1,1,1,0,
            0,0,0,0,0,0,0,0
            }, // 8
            new byte[] {
            0,0,0,0,0,0,0,0,
            0,1,1,1,1,1,1,0,
            0,1,1,0,0,1,1,0,
            0,1,1,1,1,1,1,0,
            0,0,0,0,0,1,1,0,
            0,1,1,1,1,1,1,0,
            0,0,0,0,0,0,0,0
            }, // 9
            new byte[] {
            0,0,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0
            }, // Space
            new byte[] {
            0,0,0,0,0,0,0,0,
            0,0,0,1,1,0,0,0,
            0,0,0,1,1,0,0,0,
            0,0,0,0,0,0,0,0,
            0,0,0,1,1,0,0,0,
            0,0,0,1,1,0,0,0,
            0,0,0,0,0,0,0,0
            }, // Colon
            new byte[] {
            0,0,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,
            0,0,1,1,1,1,0,0,
            0,0,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0,
            0,0,0,0,0,0,0,0
            }, // Dash
            new byte[] {
            0,0,0,0,0,0,0,0,
            0,0,0,0,0,1,1,0,
            0,0,0,0,1,1,0,0,
            0,0,0,1,1,0,0,0,
            0,0,1,1,0,0,0,0,
            0,1,1,0,0,0,0,0,
            0,0,0,0,0,0,0,0
            } // Slash
        };

        private static readonly List<char> glyphs = new List<char>
        {
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ' ', ':', '-', '/'
        };
        private const int size = 4;
        private const int columns = 8;
        private const int rows = 7;
        private const int padding = 1;
        private const string time_format = "HH:mm:ss";
        private const string date_format = "dd-MM-yyyy";
        private const string time_date_format = "HH:mm:ss dd-MM-yyyy";

        private DispatcherTimer _timer = new DispatcherTimer();
        private StackPanel _panel;
        private int _count;

        /// <summary>
        /// Add Rect
        /// </summary>
        /// <param name="name"></param>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        private Rectangle AddRect(string name, int left, int top)
        {
            Rectangle rect = new Rectangle()
            {
                Tag = name,
                Height = size,
                Width = size,
                RadiusX = 1,
                RadiusY = 1,
                Opacity = 0,
                Margin = new Thickness(2)
            };
            rect.SetBinding(Rectangle.FillProperty, new Binding()
            {
                Source = this,
                Path = new PropertyPath(nameof(Foreground)),
                Mode = BindingMode.TwoWay
            });
            rect.SetValue(Canvas.LeftProperty, left);
            rect.SetValue(Canvas.TopProperty, top);
            return rect;
        }

        /// <summary>
        /// Add Section
        /// </summary>
        /// <param name="name"></param>
        private void AddSection(string name)
        {
            Canvas canvas = new Canvas()
            {
                Tag = name,
                Height = rows * size,
                Width = columns * size
            };
            int x = 0;
            int y = 0;
            int index = 0;
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    canvas.Children.Add(AddRect($"{name}.{index}", x, y));
                    x = (x + size + padding);
                    index++;
                }
                x = 0;
                y = (y + size + padding);
            }
            _panel.Children.Add(canvas);
        }

        /// <summary>
        /// Set Rect
        /// </summary>
        /// <param name="layout"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private Rectangle SetRect(ref Canvas layout, string name)
        {
            return layout.Children.Cast<Rectangle>()
            .FirstOrDefault(f => (string)f.Tag == name);
        }

        /// <summary>
        /// Set Section
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private Canvas SetSection(string name)
        {
            return _panel.Children.Cast<Canvas>()
            .FirstOrDefault(f => (string)f.Tag == name);
        }

        /// <summary>
        /// Set Item
        /// </summary>
        /// <param name="name"></param>
        /// <param name="glyph"></param>
        private void SetItem(string name, char glyph)
        {
            Canvas canvas = SetSection(name);
            int pos = glyphs.IndexOf(glyph);
            byte[] values = layout[pos];
            for (int index = 0; index < canvas.Children.Count; index++)
            {
                SetRect(ref canvas, $"{name}.{index}").Opacity = values[index];
            }
        }

        /// <summary>
        /// Set Layout
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
                    AddSection(item.ToString());
                }
                _count = length;
            }
            foreach (int item in list)
            {
                SetItem(item.ToString(), array[item]);
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
        /// Matrix Control
        /// </summary>
        public Matrix()
        {
            _panel = new StackPanel()
            {
                Spacing = 0,
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
        typeof(Matrix), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        /// <summary>
        /// SourceProperty
        /// </summary>
        public static readonly DependencyProperty SourceProperty =
        DependencyProperty.Register(nameof(Source), typeof(Sources),
        typeof(Matrix), new PropertyMetadata(Sources.TimeDate));

        /// <summary>
        /// Value Property
        /// </summary>
        private readonly DependencyProperty ValueProperty =
        DependencyProperty.Register(nameof(Value), typeof(string),
        typeof(Matrix), new PropertyMetadata(string.Empty));

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

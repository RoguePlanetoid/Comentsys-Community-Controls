using System.ComponentModel.DataAnnotations;
using System.Linq;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace Comentsys.Community.Controls
{
    /// <summary>
    /// Card Control
    /// </summary>
    public class Card : Grid
    {
        private const string name_pip = "pip";
        private const string name_num = "num";
        private const string name_card = "card";
        private const string top_left = "topleft";
        private const string bottom_left = "bottomleft";
        private const string bottom_right = "bottomright";
        private const string top_right = "topright";

        private const string club = 
        "M 19.5155,51.3 C 17.1155,50.1 15.9155,43.7 " +
        "21.5155,43.7 C 27.1155,43.7 25.9155,50.1 23.5155,51.3 C 25.1155,50.1 " +
        "30.3155,48.5 30.3155,54.1 C 30.3155,59.7 23.1155,59.3 22.7155,54.9 L " +
        "21.9155,54.9 C 21.9155,54.9 22.3155,59.3 23.5155,61.3 L 19.5155,61.3 C " +
        "20.7155,59.3 21.1155,54.9 21.1155,54.9 L 20.3155,54.9 C 19.9155,59.3 " +
        "12.3155,59.7 12.3155,54.1 C 12.3155,49.3 17.9155,50.1 19.5155,51.3 z";
        private const string diamond = 
        "M 170.1155,199.8 L 177.3155,191 L 184.5155,199.4 " +
        "L 177.3155,209 L 170.1155,199.8 z";
        private const string heart = 
        "M 99.5,99.75 C 99.5,93.75 89.5,81.75 79.5,81.75 C " +
        "69.5,81.75 59.5,89.75 59.5,103.75 C 59.5,125.75 91.5,161.75 99.5,171.75 C " +
        "107.5,161.75 139.5,125.75 139.5,103.75 C 139.5,89.75 129.5,81.75 119.5,81.75 C " +
        "109.45012,81.75 99.5,93.75 99.5,99.75 z";
        private const string spade = 
        "M 21.1155,43.3 C 17.9155,48.1 13.7155,50.9 13.7155,54.9 " +
        "C 13.7155,58.5 15.7155,59.3 16.9155,59.3 C 18.5155,59.3 19.7155,58.5 19.7155,55.7 " +
        "C 19.7155,54.9 20.5155,54.9 20.5155,55.7 C 20.5155,59.7 19.7155,59.7 18.9155,61.7 " +
        "L 23.3155,61.7 C 22.5155,59.7 21.7155,59.7 21.7155,55.7 C 21.7155,54.9 22.5155,54.9 " +
        "22.5155,55.7 C 22.5155,58.9 23.7155,59.3 25.3155,59.3 C 26.5155,59.3 28.9155,58.5 " +
        "28.9155,54.9 C 28.9155,50.84784 24.3155,48.1 21.1155,43.3 z";

        private static readonly string[] card_pips = 
        {
            "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r"
        };
        private static readonly string[] card_values = 
        {
            "K", "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q"
        };
        private static readonly int[][] layout =
        {
            //          a, b, c, d, e, f, g, h, i, j, k, l, m, n, o, p, q, r
            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, // 0
            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, // 1
            new int[] { 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, // 2
            new int[] { 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0 }, // 3
            new int[] { 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0 }, // 4
            new int[] { 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0 }, // 5
            new int[] { 1, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 1, 0 }, // 6
            new int[] { 1, 0, 0, 1, 1, 0, 1, 0, 0, 0, 0, 0, 1, 1, 0, 0, 1, 0 }, // 7
            new int[] { 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0 }, // 8
            new int[] { 1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 1, 1, 1, 0 }, // 9
            new int[] { 1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 0, 1, 0, 1, 1, 1, 1, 0 }, // 10
            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, // 11
            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }, // 12
            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, // 13
        };

        /// <summary>
        /// Get Geometry
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static Geometry GetGeometry(string data)
        {
            return (Geometry)XamlReader.Load(
            $"<Geometry xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'>{data}</Geometry>"
            );
        }

        /// <summary>
        /// Add Path
        /// </summary>
        /// <param name="name"></param>
        /// <param name="data"></param>
        /// <param name="height"></param>
        /// <param name="margin"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        private Path AddPath(string name, string data, 
        int height, int margin, Color color)
        {
            Path path = new Path
            {
                Tag = name,
                Data = GetGeometry(data),
                Fill = new SolidColorBrush(color),
                Height = height,
                Stretch = Stretch.Uniform,
                Margin = new Thickness(margin),
                Opacity = 0
            };
            return path;
        }

        /// <summary>
        /// Add Path
        /// </summary>
        /// <param name="name"></param>
        /// <param name="data"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="rowSpan"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        private void AddPath(ref Grid grid,
            string name, string data,
            int row, int rowSpan, int column, 
            Color color)
        {
            bool flip = row > 2;
            Path path = AddPath(name, data, 100, 5, color);
            if (flip)
            {
                path.RenderTransform = new ScaleTransform()
                {
                    ScaleY = -1, CenterY = 50
                };
            }
            Grid.SetRow(path, row);
            Grid.SetColumn(path, column);
            Grid.SetRowSpan(path, rowSpan);
            grid.Children.Add(path);
        }

        /// <summary>
        /// Add Item
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="name"></param>
        /// <param name="symbol"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="value"></param>
        /// <param name="flip"></param>
        /// <param name="color"></param>
        private void AddItem(
        ref Grid grid, string name, string symbol,
        int row, int column, string value,
        bool flip, Color color)
        {
            StackPanel item = new StackPanel()
            {
                Tag = $"{name}"
            };
            Path path = AddPath($"{name}.{name_pip}", symbol, 40, 0, color);
            if (flip)
            {
                path.RenderTransform = new ScaleTransform()
                {
                    ScaleY = -1, CenterY = 20
                };
            }
            item.Children.Add(path);
            item.Children.Add(new TextBlock()
            {
                Tag = $"{name}.{name_num}",
                Text = value,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontWeight = new FontWeight() { Weight = 300 },
                Foreground = new SolidColorBrush(color),
                FontSize = 40,
                Opacity = 0
            });
            item.SetValue(Grid.ColumnProperty, column);
            item.SetValue(Grid.RowProperty, row);
            grid.Children.Add(item);
        }

        /// <summary>
        /// Add Face
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="row"></param>
        /// <param name="rowspan"></param>
        /// <param name="column"></param>
        /// <param name="colspan"></param>
        /// <param name="color"></param>
        /// <param name="value"></param>
        /// <param name="name"></param>
        private void AddFace(
        ref Grid grid, string name,
        int row, int rowspan,
        int column, int colspan,
        string value, Color color)
        {
            TextBlock face = new TextBlock()
            {
                Tag = name,
                HorizontalAlignment = HorizontalAlignment.Center,
                Text = value,
                Foreground = new SolidColorBrush(color),
                FontSize = 300,
                Opacity = 0
            };
            face.SetValue(Grid.RowProperty, row);
            face.SetValue(Grid.RowSpanProperty, rowspan);
            face.SetValue(Grid.ColumnProperty, column);
            face.SetValue(Grid.ColumnSpanProperty, colspan);
            grid.Children.Add(face);
        }

        /// <summary>
        /// Get Card
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private Grid GetCard(string name)
        {
            string suit = club;
            Grid grid = new Grid()
            {
                Margin = new Thickness(10),
                Padding = new Thickness(10, 20, 10, 10),
                CornerRadius = new CornerRadius(10),
                BorderThickness = new Thickness(5),
                BorderBrush = new SolidColorBrush(Colors.Black)
            };
            grid.SetBinding(Grid.BackgroundProperty, new Binding()
            {
                Path = new PropertyPath(nameof(Fill)),
                Mode = BindingMode.TwoWay,
                Source = this
            });
            for (int c = 0; c < 5; c++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition()
                {
                    Width = new GridLength(80)
                });
            }
            for (int r = 0; r < 6; r++)
            {
                grid.RowDefinitions.Add(new RowDefinition()
                {
                    Height = new GridLength(100)
                });
            }
            AddItem(ref grid, $"{name}.{top_left}", suit, 0, 0, "1", false, Colors.Black);
            AddItem(ref grid, $"{name}.{bottom_left}", suit, 6, 0, "1", true, Colors.Black);
            AddItem(ref grid, $"{name}.{bottom_right}", suit, 6, 4, "1", true, Colors.Black);
            AddItem(ref grid, $"{name}.{top_right}", suit, 0, 4, "1", false, Colors.Black);
            int count = 0;
            for (int i = 1; i < 5; i++)
            {
                AddPath(ref grid, $"{name}.{card_pips[count]}", suit, i, 1, 1, Colors.Black);
                count++;
            }
            AddPath(ref grid, $"{name}.{card_pips[count]}", suit, 2, 2, 1, Colors.Black);
            count++;
            for (int i = 1; i < 5; i++)
            {
                AddPath(ref grid, $"{name}.{card_pips[count]}", suit, i, 1, 2, Colors.Black);
                count++;
            }
            for (int i = 1; i < 4; i++)
            {
                AddPath(ref grid, $"{name}.{card_pips[count]}", suit, i, 2, 2, Colors.Black);
                count++;
            }
            AddPath(ref grid, $"{name}.{card_pips[count]}", suit, 2, 2, 3, Colors.Black);
            count++;
            for (int i = 1; i < 5; i++)
            {
                AddPath(ref grid, $"{name}.{card_pips[count]}", suit, i, 1, 3, Colors.Black);
                count++;
            }
            AddFace(ref grid, $"{name}.{card_pips[count]}", 1, 4, 1, 3, "A", Colors.Black);
            return grid;
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <typeparam name="TOutput"></typeparam>
        /// <typeparam name="TInput"></typeparam>
        /// <param name="input"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private static TOutput Get<TOutput, TInput>
        (ref TInput input, string name)
        where TOutput : FrameworkElement 
        where TInput : Panel
        {
            return (TOutput)input.Children.
            FirstOrDefault(f => (string)((TOutput)f).Tag == name);
        }

        /// <summary>
        /// Set Path
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="symbol"></param>
        /// <param name="color"></param>
        /// <param name="opacity"></param>
        /// <param name="name"></param>
        private static void SetPath(ref Grid grid, string symbol,
        Color color, int opacity, string name)
        {
            Path path = (Path)Get<FrameworkElement, Grid>
            (ref grid, name);
            path.Data = GetGeometry(symbol);
            path.Fill = new SolidColorBrush(color);
            path.Opacity = opacity;
        }

        /// <summary>
        /// Set Text
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="color"></param>
        /// <param name="value"></param>
        /// <param name="opacity"></param>
        /// <param name="name"></param>
        private static void SetText(ref Grid grid, Color color,
        string value, int opacity, string name)
        {
            TextBlock text = (TextBlock)Get<FrameworkElement, Grid>
            (ref grid, name);
            text.Text = value;
            text.Foreground = new SolidColorBrush(color);
            text.Opacity = opacity;
        }

        /// <summary>
        /// Set Item
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="symbol"></param>
        /// <param name="color"></param>
        /// <param name="value"></param>
        /// <param name="name"></param>
        private static void SetItem(ref Grid grid, string symbol,
            Color color, string value, string name)
        {
            StackPanel panel = Get<StackPanel, Grid>
            (ref grid, name);
            Path path = (Path)Get<FrameworkElement, StackPanel>
            (ref panel, $"{name}.{name_pip}");
            path.Data = GetGeometry(symbol);
            path.Fill = new SolidColorBrush(color);
            path.Opacity = 1;
            TextBlock text = (TextBlock)Get<FrameworkElement, StackPanel>
            (ref panel, $"{name}.{name_num}");
            text.Foreground = new SolidColorBrush(color);
            text.Text = value.ToString();
            text.Opacity = 1;
        }

        /// <summary>
        /// Set Card
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="name"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        private static void SetCard(Grid grid, string name, int number)
        {
            int index = (number % 13);
            string suit = club;
            switch (number)
            {
                case int c when (number > 1 && number <= 13):
                    suit = club;
                    break;
                case int d when (number > 14 && number <= 26):
                    suit = diamond;
                    break;
                case int h when (number >= 27 && number <= 39):
                    suit = heart;
                    break;
                case int s when (number >= 40 && number <= 52):
                    suit = spade;
                    break;
            }
            Color color = (suit == heart || suit == diamond) ? Colors.Red : Colors.Black;
            string value = card_values[index];
            SetItem(ref grid, suit, color, value, $"{name}.{top_left}");
            SetItem(ref grid, suit, color, value, $"{name}.{bottom_left}");
            SetItem(ref grid, suit, color, value, $"{name}.{bottom_right}");
            SetItem(ref grid, suit, color, value, $"{name}.{top_right}");
            int[] values = layout[index];
            SetPath(ref grid, suit, color, values[0], $"{name}.a");
            SetPath(ref grid, suit, color, values[1], $"{name}.b");
            SetPath(ref grid, suit, color, values[2], $"{name}.c");
            SetPath(ref grid, suit, color, values[3], $"{name}.d");
            SetPath(ref grid, suit, color, values[4], $"{name}.e");
            SetPath(ref grid, suit, color, values[5], $"{name}.f");
            SetPath(ref grid, suit, color, values[6], $"{name}.g");
            SetPath(ref grid, suit, color, values[7], $"{name}.h");
            SetPath(ref grid, suit, color, values[8], $"{name}.i");
            SetPath(ref grid, suit, color, values[9], $"{name}.j");
            SetPath(ref grid, suit, color, values[10], $"{name}.k");
            SetPath(ref grid, suit, color, values[11], $"{name}.l");
            SetPath(ref grid, suit, color, values[12], $"{name}.m");
            SetPath(ref grid, suit, color, values[13], $"{name}.n");
            SetPath(ref grid, suit, color, values[14], $"{name}.o");
            SetPath(ref grid, suit, color, values[15], $"{name}.p");
            SetPath(ref grid, suit, color, values[16], $"{name}.q");
            SetText(ref grid, color, value, values[17], $"{name}.r");
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="obj">Object</param>
        /// <param name="e">Event Args</param>
        private static void Update(DependencyObject obj,
        DependencyPropertyChangedEventArgs e)
        => SetCard(((Card)obj).ContentGrid, name_card, (int)e.NewValue);

        /// <summary>
        /// Content Grid
        /// </summary>
        internal Grid ContentGrid { get; set; }

        /// <summary>
        /// Card Control
        /// </summary>
        public Card()
        {
            ContentGrid = GetCard(name_card);
            Viewbox viewbox = new Viewbox()
            {
                Child = ContentGrid
            };
            this.Children.Add(viewbox);
        }

        /// <summary>
        /// Fill Property
        /// </summary>
        public static readonly DependencyProperty FillProperty =
        DependencyProperty.Register(nameof(Fill), typeof(Brush),
        typeof(Card), new PropertyMetadata(new SolidColorBrush(Colors.White)));

        /// <summary>
        /// Value Property
        /// </summary>
        private readonly DependencyProperty ValueProperty =
        DependencyProperty.Register(nameof(Value), typeof(int),
        typeof(Card), new PropertyMetadata(0,
        new PropertyChangedCallback(Update)));

        // <summary>
        /// Fill
        /// </summary>
        public Brush Fill
        {
            get => (Brush)GetValue(FillProperty);
            set => SetValue(FillProperty, value);
        }

        /// <summary>
        /// Card Value between 0 - 52
        /// </summary>
        [Range(0, 52)]
        public int Value
        {
            get => (int)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }
    }
}

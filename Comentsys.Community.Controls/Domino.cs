using System.ComponentModel.DataAnnotations;
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
    /// Domino Control
    /// </summary>
    public class Domino : Grid
    {
        private const int size = 3; // Section Size
        private const string name_domino = "domino";
        private const string name_upper = "upper";
        private const string name_lower = "lower";

        private static readonly string[] tags = 
        {
            "a", "b", "c", "d", "e", "f", "g", "h", "i"
        };
        private static readonly string[] tiles =
        {
            "0,0",
            "0,1", "1,1",
            "0,2", "1,2", "2,2",
            "0,3", "1,3", "2,3", "3,3",
            "0,4", "1,4", "2,4", "3,4", "4,4",
            "0,5", "1,5", "2,5", "3,5", "4,5", "5,5",
            "0,6", "1,6", "2,6", "3,6", "4,6", "5,6", "6,6"
        };
        private static readonly int[][] layout =
        {
                     // a, b, c, d, e, f, g, h, i
            new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 }, // 0
            new int[] { 0, 0, 0, 0, 1, 0, 0, 0, 0 }, // 1
            new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 1 }, // 2
            new int[] { 1, 0, 0, 0, 1, 0, 0, 0, 1 }, // 3
            new int[] { 1, 0, 1, 0, 0, 0, 1, 0, 1 }, // 4
            new int[] { 1, 0, 1, 0, 1, 0, 1, 0, 1 }, // 5
            new int[] { 1, 0, 1, 1, 0, 1, 1, 0, 1 }, // 6
        }; // Section Layout

        private static Grid _grid = null;
        private static StackPanel _panel = null;

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="row">Row</param>
        /// <param name="column">Column</param>
        private void Add(ref Grid grid, int row, int column, string name)
        {
            Ellipse element = new Ellipse()
            {
                Tag = name,
                Margin = new Thickness(5),
                Opacity = 0
            };
            element.SetBinding(Ellipse.FillProperty, new Binding()
            {
                Path = new PropertyPath(nameof(Foreground)),
                Mode = BindingMode.TwoWay,
                Source = this
            });
            element.SetValue(Grid.ColumnProperty, column);
            element.SetValue(Grid.RowProperty, row);
            grid.Children.Add(element);
        }

        /// <summary>
        /// Add Portion
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private Grid AddPortion(string name)
        {
            Grid grid = new Grid()
            {
                Tag = name,
                Width = 100,
                Height = 100,
                Padding = new Thickness(5)
            };
            grid.SetBinding(Grid.BackgroundProperty, new Binding()
            {
                Path = new PropertyPath(nameof(Fill)),
                Mode = BindingMode.TwoWay,
                Source = this
            });
            // Setup Grid Layout
            for (int index = 0; index < size; index++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            int count = 0;
            for (int row = 0; row < size; row++)
            {
                for (int column = 0; column < size; column++)
                {
                    Add(ref grid, row, column, $"{name}.{tags[count]}");
                    count++;
                }
            }
            return grid;
        }

        /// <summary>
        /// Add Domino
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private StackPanel AddDomino()
        {
            StackPanel panel = new StackPanel()
            {
                Orientation = Orientation.Vertical,
                Margin = new Thickness(25),
            };
            panel.Children.Add(AddPortion(name_upper));
            panel.Children.Add(AddPortion(name_lower));
            return panel;
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
        /// Set Portion
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        private static void SetPortion(string name, int value)
        {
            Grid portion = Get<Grid, StackPanel>(ref _panel, name);
            int[] values = layout[value];
            Get<Ellipse, Grid>(ref portion, $"{name}.a").Opacity = values[0];
            Get<Ellipse, Grid>(ref portion, $"{name}.b").Opacity = values[1];
            Get<Ellipse, Grid>(ref portion, $"{name}.c").Opacity = values[2];
            Get<Ellipse, Grid>(ref portion, $"{name}.d").Opacity = values[3];
            Get<Ellipse, Grid>(ref portion, $"{name}.e").Opacity = values[4];
            Get<Ellipse, Grid>(ref portion, $"{name}.f").Opacity = values[5];
            Get<Ellipse, Grid>(ref portion, $"{name}.g").Opacity = values[6];
            Get<Ellipse, Grid>(ref portion, $"{name}.h").Opacity = values[7];
            Get<Ellipse, Grid>(ref portion, $"{name}.i").Opacity = values[8];
        }

        /// <summary>
        /// Set Domino
        /// </summary>
        /// <param name="tile"></param>
        private static void SetDomino(string tile)
        {
            SetPortion(name_upper, 0);
            SetPortion(name_lower, 0);
            string[] values = tile.Split(',');
            int upper = int.Parse(values[0]);
            int lower = int.Parse(values[1]);
            SetPortion(name_upper, upper);
            SetPortion(name_lower, lower);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="obj">Object</param>
        /// <param name="e">Event Args</param>
        private static void Update(DependencyObject obj,
        DependencyPropertyChangedEventArgs e)
        {
            SetDomino(tiles[(int)e.NewValue]);
        }

        /// <summary>
        /// Domino Control
        /// </summary>
        public Domino()
        {
            _grid = new Grid()
            {
                Padding = new Thickness(5)
            };
            _grid.Children.Add(_panel = AddDomino());
            Viewbox viewbox = new Viewbox()
            {
                Child = _grid
            };
            this.Children.Add(viewbox);
        }

        /// <summary>
        /// Foreground Property
        /// </summary>
        public static readonly DependencyProperty ForegroundProperty =
        DependencyProperty.Register(nameof(Foreground), typeof(Brush),
        typeof(Domino), new PropertyMetadata(new SolidColorBrush(Colors.White)));

        /// <summary>
        /// Fill Property
        /// </summary>
        public static readonly DependencyProperty FillProperty =
        DependencyProperty.Register(nameof(Fill), typeof(Brush),
        typeof(Domino), new PropertyMetadata(
            new LinearGradientBrush(
            new GradientStopCollection()
            {
                new GradientStop()
                {
                    Color = Colors.Black, Offset = 0.0
                },
                new GradientStop()
                {
                    Color = Colors.Gray, Offset = 1.0
                }
            }, 
            90)
        ));

        /// <summary>
        /// Value Property
        /// </summary>
        private readonly DependencyProperty ValueProperty =
        DependencyProperty.Register(nameof(Value), typeof(int),
        typeof(Domino), new PropertyMetadata(0,
        new PropertyChangedCallback(Update)));

        /// <summary>
        /// Foreground Brush
        /// </summary>
        public Brush Foreground
        {
            get => (Brush)GetValue(ForegroundProperty);
            set => SetValue(ForegroundProperty, value);
        }

        // <summary>
        /// Fill
        /// </summary>
        public Brush Fill
        {
            get => (Brush)GetValue(FillProperty);
            set => SetValue(FillProperty, value);
        }

        /// <summary>
        /// Domino Value Tile between 0 - 28
        /// </summary>
        [Range(0, 28)]
        public int Value
        {
            get => (int)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }
    }
}

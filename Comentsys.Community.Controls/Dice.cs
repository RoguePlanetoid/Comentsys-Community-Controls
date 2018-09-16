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
    /// Dice Control
    /// </summary>
    public class Dice : Grid
    {
        private const int size = 3; // Dice Size

        private static readonly byte[][] layout =
        {
                      // a, b, c, d, e, f, g, h, i
            new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 }, // 0
            new byte[] { 0, 0, 0, 0, 1, 0, 0, 0, 0 }, // 1
            new byte[] { 1, 0, 0, 0, 0, 0, 0, 0, 1 }, // 2
            new byte[] { 1, 0, 0, 0, 1, 0, 0, 0, 1 }, // 3
            new byte[] { 1, 0, 1, 0, 0, 0, 1, 0, 1 }, // 4
            new byte[] { 1, 0, 1, 0, 1, 0, 1, 0, 1 }, // 5
            new byte[] { 1, 0, 1, 1, 0, 1, 1, 0, 1 }, // 6
        }; // Dice Layout

        private static Grid _grid = null;

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="row">Row</param>
        /// <param name="column">Column</param>
        private void Add(int row, int column)
        {
            Ellipse element = new Ellipse()
            {
                Margin = new Thickness(5),
                Opacity = 0
            };
            element.SetBinding(Rectangle.FillProperty, new Binding()
            {
                Path = new PropertyPath(nameof(Foreground)),
                Mode = BindingMode.TwoWay,
                Source = this
            });
            element.SetValue(Grid.ColumnProperty, column);
            element.SetValue(Grid.RowProperty, row);
            _grid.Children.Add(element);
        }

        /// <summary>
        /// Set
        /// </summary>
        /// <param name="row">Row</param>
        /// <param name="column">Column</param>
        /// <param name="opacity">Opacity</param>
        private static void Set(int row, int column, byte opacity)
        {
            Ellipse ellipse =
            _grid.Children.Cast<Ellipse>().FirstOrDefault(f =>
            Grid.GetRow(f) == row && Grid.GetColumn(f) == column);
            if (ellipse != null) ellipse.Opacity = opacity;
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="obj">Object</param>
        /// <param name="e">Event Args</param>
        private static void Update(DependencyObject obj,
        DependencyPropertyChangedEventArgs e)
        {
            int count = 0;
            for (int row = 0; row < size; row++)
            {
                for (int column = 0; column < size; column++)
                {
                    Set(row, column, layout[(int)e.NewValue][count]);
                    count++;
                }
            }
        }

        /// <summary>
        /// Dice Control
        /// </summary>
        public Dice()
        {
            _grid = new Grid()
            {
                Width = 100,
                Height = 100,
                Padding = new Thickness(5)
            };
            // Setup Grid Layout
            for (int index = 0; index < size; index++)
            {
                _grid.RowDefinitions.Add(new RowDefinition());
                _grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            for (int row = 0; row < size; row++)
            {
                for (int column = 0; column < size; column++)
                {
                    Add(row, column);
                }
            }
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
        typeof(Dice), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        /// <summary>
        /// Value Property
        /// </summary>
        private readonly DependencyProperty ValueProperty =
        DependencyProperty.Register(nameof(Value), typeof(int),
        typeof(Dice), new PropertyMetadata(0,
        new PropertyChangedCallback(Update)));

        /// <summary>
        /// Foreground Brush
        /// </summary>
        public Brush Foreground
        {
            get => (Brush)GetValue(ForegroundProperty);
            set => SetValue(ForegroundProperty, value);
        }

        /// <summary>
        /// Dice Value between 0 - 6
        /// </summary>
        [Range(0, 6)]
        public int Value
        {
            get => (int)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }
    }
}
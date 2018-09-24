using System;
using Windows.Devices.Input;
using Windows.UI;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace Comentsys.Community.Controls
{
    /// <summary>
    /// Direct Control
    /// </summary>
    public class Direct : Grid
    {
        private const int size = 3;
        private const string path_up = "M 0,0 40,0 40,60 20,80 0,60 0,0 z";
        private const string path_down = "M 0,20 20,0 40,20 40,80 0,80 z";
        private const string path_left = "M 0,0 60,0 80,20 60,40 0,40 z";
        private const string path_right = "M 0,20 20,0 80,0 80,40 20,40 z";
        private const string name_up = "Up";
        private const string name_down = "Down";
        private const string name_left = "Left";
        private const string name_right = "Right";

        private Grid _grid;

        // <summary>
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
        /// Release Capture
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReleaseCapture(object sender, PointerRoutedEventArgs e) => 
        ((Path)sender).Opacity = 1.0;

        /// <summary>
        /// Has Capture
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetCapture(object sender, PointerRoutedEventArgs e)
        {
            Path path = ((Path)sender);
            PointerPoint point = e.GetCurrentPoint(this);
            bool fire = (e.Pointer.PointerDeviceType == PointerDeviceType.Mouse) ?
            point.Properties.IsLeftButtonPressed : point.IsInContact;
            if (fire)
            {
                path.Opacity = 0.75;
                Value = (Direction)Enum.Parse(typeof(Direction), path.Name);
                ValueChanged?.Invoke(Value);
            }
        }

        /// <summary>
        /// Add Path
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="rowspan"></param>
        /// <param name="columnspan"></param>
        /// <param name="vertical"></param>
        /// <param name="horizontal"></param>
        private void AddPath(ref Grid grid,
            string name, string value,
            int row, int column,
            int? rowspan, int? columnspan,
            VerticalAlignment? vertical = null,
            HorizontalAlignment? horizontal = null)
        {
            Path path = new Path
            {
                Name = name,
                Margin = new Thickness(5),
                Data = GetGeometry(value)
            };
            if (vertical != null) path.VerticalAlignment = vertical.Value;
            if (horizontal != null) path.HorizontalAlignment = horizontal.Value;
            path.SetBinding(Path.FillProperty, new Binding()
            {
                Source = this,
                Path = new PropertyPath(nameof(Foreground)),
                Mode = BindingMode.TwoWay
            });
            path.PointerMoved += SetCapture;
            path.PointerExited += ReleaseCapture;
            path.SetValue(Grid.RowProperty, row);
            path.SetValue(Grid.ColumnProperty, column);
            if (rowspan != null) path.SetValue(Grid.RowSpanProperty, rowspan);
            if (columnspan != null) path.SetValue(Grid.ColumnSpanProperty, columnspan);
            grid.Children.Add(path);
        }

        /// <summary>
        /// Get Layout
        /// </summary>
        private void GetLayout()
        {
            _grid = new Grid()
            {
                Height = 180,
                Width = 180
            };
            _grid.ColumnDefinitions.Clear();
            _grid.RowDefinitions.Clear();
            for (int index = 0; (index < size); index++)
            {
                _grid.RowDefinitions.Add(new RowDefinition()
                {
                    Height = (index == 1) ? GridLength.Auto : 
                    new GridLength(100, GridUnitType.Star)
                });
                _grid.ColumnDefinitions.Add(new ColumnDefinition()
                {
                    Width = (index == 1) ? GridLength.Auto : 
                    new GridLength(100, GridUnitType.Star)
                });
            }
            AddPath(ref _grid, "Up", path_up, 0, 1, 2, null, VerticalAlignment.Top, null);
            AddPath(ref _grid, "Down", path_down, 1, 1, 2, null, VerticalAlignment.Bottom, null);
            AddPath(ref _grid, "Left", path_left, 1, 0, null, 2, null, HorizontalAlignment.Left);
            AddPath(ref _grid, "Right", path_right, 1, 1, null, 2, null, HorizontalAlignment.Right);
        }

        /// <summary>
        /// Direct Control
        /// </summary>
        public Direct()
        {
            GetLayout();
            Viewbox viewbox = new Viewbox()
            {
                Child = _grid
            };
            this.Children.Add(viewbox);
        }

        /// <summary>
        /// Direction
        /// </summary>
        public enum Direction
        {
            None = 0,
            Up = 1,
            Down = 2,
            Left = 3,
            Right = 4,
        }

        /// <summary>
        /// ValueChangedEventHandler
        /// </summary>
        /// <param name="value"></param>
        public delegate void ValueChangedEventHandler(Direction value);

        /// <summary>
        /// Value Changed Event
        /// </summary>
        public event ValueChangedEventHandler ValueChanged;

        /// <summary>
        /// Foreground Property
        /// </summary>
        public static readonly DependencyProperty ForegroundProperty =
        DependencyProperty.Register(nameof(Foreground), typeof(Brush),
        typeof(Direct), new PropertyMetadata(new SolidColorBrush(Colors.Black)));

        /// <summary>
        /// Value Property
        /// </summary>
        private readonly DependencyProperty ValueProperty =
        DependencyProperty.Register(nameof(Value), typeof(Direction),
        typeof(Direct), new PropertyMetadata(Direction.None));

        /// <summary>
        /// Foreground
        /// </summary>
        public Brush Foreground
        {
            get => (Brush)GetValue(ForegroundProperty);
            set => SetValue(ForegroundProperty, value);
        }

        /// <summary>
        /// Direct Value
        /// </summary>
        public Direction Value
        {
            get => (Direction)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }
    }
}

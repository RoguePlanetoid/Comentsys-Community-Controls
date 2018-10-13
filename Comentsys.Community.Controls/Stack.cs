using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace Comentsys.Community.Controls
{
    /// <summary>
    /// Stack Chart Control
    /// </summary>
    public class Stack : Grid
    {
        /// <summary>
        /// Get Percentages
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private static List<double> GetPercentages(List<ChartItem> list)
        {
            IEnumerable<double> values = list.Select(s => s.Value);
            List<double> results = new List<double>();
            double total = values.Sum();
            foreach (double item in values)
            {
                results.Add((item / total) * 100);
            }
            return results.OrderBy(o => o).ToList();
        }

        /// <summary>
        /// Get Rect
        /// </summary>
        /// <param name="stack"></param>
        /// <param name="index"></param>
        /// <param name="brush"></param>
        /// <returns></returns>
        private static Rectangle GetRect(Stack stack, int index, Brush brush)
        {
            Rectangle rect = new Rectangle()
            {
                Fill = brush
            };
            rect.SetValue((stack.Orientation == Orientation.Horizontal)
            ? Grid.ColumnProperty : Grid.RowProperty, index);
            return rect;
        }
 
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="stack"></param>
        private static void Update(Stack stack)
        {
            List<double> percentages = GetPercentages(stack.Values);
            stack.ContentGrid.Children.Clear();
            stack.ContentGrid.RowDefinitions.Clear();
            stack.ContentGrid.ColumnDefinitions.Clear();
            for (int index = 0; index < percentages.Count(); index++)
            {
                double percentage = percentages[index];
                GridLength gridLength = new GridLength(percentage, GridUnitType.Star);
                if (stack.Orientation == Orientation.Horizontal)
                {
                    stack.ContentGrid.ColumnDefinitions.Add(new ColumnDefinition()
                    {
                        Width = gridLength
                    });
                }
                else
                {
                    stack.ContentGrid.RowDefinitions.Add(new RowDefinition()
                    {
                        Height = gridLength
                    });
                }
                stack.ContentGrid.Children.Add(GetRect(stack, index, 
                stack.Values[index].Brush));
            }
        }

        /// <summary>
        /// Update Orientation
        /// </summary>
        /// <param name="obj">Object</param>
        /// <param name="e">Event Args</param>
        private static void UpdateOrientation(DependencyObject obj,
        DependencyPropertyChangedEventArgs e) =>
        Update((Stack)obj);

        /// <summary>
        /// Update Values
        /// </summary>
        /// <param name="obj">Object</param>
        /// <param name="e">Event Args</param>
        private static void UpdateValues(DependencyObject obj,
        DependencyPropertyChangedEventArgs e) =>
        Update((Stack)obj);

        /// <summary>
        /// Content Grid
        /// </summary>
        internal Grid ContentGrid { get; set; }

        /// <summary>
        /// Stack Chart Control
        /// </summary>
        public Stack()
        {
            ContentGrid = new Grid()
            {
                Width = 100,
                Height = 100
            };
            Viewbox viewbox = new Viewbox()
            {
                StretchDirection = StretchDirection.Both,
                Stretch = Stretch.UniformToFill,
                Child = ContentGrid
            };
            this.Children.Add(viewbox);
        }

        /// <summary>
        /// Orientation Property
        /// </summary>
        private readonly DependencyProperty OrientationProperty =
        DependencyProperty.Register(nameof(Orientation), typeof(Orientation),
        typeof(Stack), new PropertyMetadata(Orientation.Horizontal, UpdateOrientation));

        /// <summary>
        /// Values Property
        /// </summary>
        private readonly DependencyProperty ValuesProperty =
        DependencyProperty.Register(nameof(Values), typeof(List<ChartItem>),
        typeof(Stack), new PropertyMetadata(new List<ChartItem>(), UpdateValues));

        /// <summary>
        /// Orientation
        /// </summary>
        public Orientation Orientation
        {
            get => (Orientation)GetValue(OrientationProperty);
            set => SetValue(OrientationProperty, value);
        }

        /// <summary>
        /// Stack Values
        /// </summary>
        public List<ChartItem> Values
        {
            get => (List<ChartItem>)GetValue(ValuesProperty);
            set => SetValue(ValuesProperty, value);
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace Comentsys.Community.Controls
{
    /// <summary>
    /// Donut Chart Control
    /// </summary>
    public class Donut : Grid
    {
        private const double circle = 360;
        private const double total = 100;
        private const int radius = 100;

        /// <summary>
        /// Get Percentages
        /// </summary>
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
        /// Get Point
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="hole"></param>
        /// <returns></returns>
        private static Point GetPoint(double angle, double hole)
        {
            double radians = (Math.PI / 180) * (angle - 90);
            double x = hole * Math.Cos(radians);
            double y = hole * Math.Sin(radians);
            return new Point(x += radius, y += radius);
        }

        /// <summary>
        /// Get Path
        /// </summary>
        /// <param name="sweep"></param>
        /// <param name="hole"></param>
        /// <param name="brush"></param>
        /// <returns></returns>
        private static Path GetPath(ref double angle, double sweep, double hole, Brush brush)
        {
            bool isLargeArc = sweep > 180.0;
            Point start = new Point(radius, radius);
            Point innerArcStart = GetPoint(angle, hole);
            Point innerArcEnd = GetPoint(angle + sweep, hole);
            Point outerArcStart = GetPoint(angle, radius);
            Point outerArcEnd = GetPoint(angle + sweep, radius);
            Size outerArcSize = new Size(radius, radius);
            Size innerArcSize = new Size(hole, hole);
            LineSegment lineOne = new LineSegment()
            {
                Point = outerArcStart
            };
            ArcSegment arcOne = new ArcSegment()
            {
                SweepDirection = SweepDirection.Clockwise,
                IsLargeArc = isLargeArc,
                Point = outerArcEnd,
                Size = outerArcSize
            };
            LineSegment lineTwo = new LineSegment()
            {
                Point = innerArcEnd
            };
            ArcSegment arcTwo = new ArcSegment()
            {
                SweepDirection = SweepDirection.Counterclockwise,
                IsLargeArc = isLargeArc,
                Point = innerArcStart,
                Size = innerArcSize
            };
            PathFigure figure = new PathFigure()
            {
                StartPoint = innerArcStart,
                IsClosed = true,
                IsFilled = true
            };
            figure.Segments.Add(lineOne);
            figure.Segments.Add(arcOne);
            figure.Segments.Add(lineTwo);
            figure.Segments.Add(arcTwo);
            PathGeometry pathGeometry = new PathGeometry();
            pathGeometry.Figures.Add(figure);
            Path path = new Path
            {
                Fill = brush,
                Data = pathGeometry
            };
            angle += sweep;
            return path;
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="donut"></param>
        private static void Update(Donut donut)
        {
            double angle = 0;
            double sweep = 0;
            double value = (circle / total);
            List<double> percentages = GetPercentages(donut.Values);
            donut.ContentCanvas.Children.Clear();
            for (int index = 0; index < percentages.Count(); index++)
            {
                double percentage = percentages[index];
                sweep = value * percentage;
                Path path = GetPath(ref angle, sweep, donut.Hole, 
                donut.Values[index].Brush);
                donut.ContentCanvas.Children.Add(path);
            }
        }

        /// <summary>
        /// Update Hole
        /// </summary>
        /// <param name="obj">Object</param>
        /// <param name="e">Event Args</param>
        private static void UpdateHole(DependencyObject obj,
        DependencyPropertyChangedEventArgs e) =>
        Update((Donut)obj);

        /// <summary>
        /// Update Values
        /// </summary>
        /// <param name="obj">Object</param>
        /// <param name="e">Event Args</param>
        private static void UpdateValues(DependencyObject obj,
        DependencyPropertyChangedEventArgs e) =>
        Update((Donut)obj);

        /// <summary>
        /// Content Canvas
        /// </summary>
        internal Canvas ContentCanvas { get; set; }

        /// <summary>
        /// Stack Chart Control
        /// </summary>
        public Donut()
        {
            ContentCanvas = new Canvas()
            {
                Width = radius * 2,
                Height = radius * 2
            };
            Viewbox viewbox = new Viewbox()
            {
                StretchDirection = StretchDirection.Both,
                Stretch = Stretch.UniformToFill,
                Child = ContentCanvas
            };
            this.Children.Add(viewbox);
        }

        /// <summary>
        /// Hole Property
        /// </summary>
        private readonly DependencyProperty HoleProperty =
        DependencyProperty.Register(nameof(Hole), typeof(double),
        typeof(Stack), new PropertyMetadata(25.0, UpdateHole));

        /// <summary>
        /// Values Property
        /// </summary>
        private readonly DependencyProperty ValuesProperty =
        DependencyProperty.Register(nameof(Values), typeof(List<ChartItem>),
        typeof(Stack), new PropertyMetadata(new List<ChartItem>(), UpdateValues));

        /// <summary>
        /// Hole Size 0 - 100
        /// </summary>
        [Range(0, 100)]
        public double Hole
        {
            get => (double)GetValue(HoleProperty);
            set => SetValue(HoleProperty, value);
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
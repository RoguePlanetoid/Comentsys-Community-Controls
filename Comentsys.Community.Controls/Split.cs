using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Comentsys.Community.Controls
{
    public class Split : StackPanel
    {
        private const char space = ' ';

        private const string time_format = "HH mm ss";
        private const string date_format = "dd MM yyyy";
        private const string time_date_format = "HH mm ss dd MM yyyy";

        private DispatcherTimer _timer = new DispatcherTimer();
        private int _count;

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="name"></param>
        private void Add(string name)
        {
            Flipper flipper = new Flipper()
            {
                Tag = name
            };
            flipper.SetBinding(Flipper.ForegroundProperty, new Binding()
            {
                Path = new PropertyPath(nameof(Foreground)),
                Mode = BindingMode.TwoWay,
                Source = this
            });
            FrameworkElement element = flipper;          
            if (name == null)
            {
                element = new Canvas
                {
                    Width = 5
                };
            }
            this.Children.Add(element);
        }

        /// <summary>
        /// Set Split
        /// </summary>
        /// <param name="name"></param>
        /// <param name="glyph"></param>
        private void SetSplit(string name, char glyph)
        {
            FrameworkElement element = 
            this.Children.Cast<FrameworkElement>()
            .FirstOrDefault(f => (string)f.Tag == name);
            if (element is Flipper)
            {
                ((Flipper)element).Value = glyph.ToString();
            }
        }

        /// <summary>
        /// Set Layout
        /// </summary>
        private void SetLayout()
        {
            char[] array = Value.ToCharArray();
            int length = array.Length;
            IEnumerable<int> list = Enumerable.Range(0, length);
            if (_count != length)
            {
                this.Children.Clear();
                foreach (int item in list)
                {
                    Add((array[item] == space)
                    ? null : item.ToString());
                }
                _count = length;
            }
            foreach (int item in list)
            {
                SetSplit(item.ToString(), array[item]);
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
        /// Split Control
        /// </summary>
        public Split()
        {
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
        typeof(Split), new PropertyMetadata(new SolidColorBrush(Colors.White)));

        /// <summary>
        /// SourceProperty
        /// </summary>
        public static readonly DependencyProperty SourceProperty =
        DependencyProperty.Register(nameof(Source), typeof(Sources),
        typeof(Split), new PropertyMetadata(Sources.Time));

        /// <summary>
        /// Value Property
        /// </summary>
        private readonly DependencyProperty ValueProperty =
        DependencyProperty.Register(nameof(Value), typeof(string),
        typeof(Split), new PropertyMetadata(string.Empty));

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
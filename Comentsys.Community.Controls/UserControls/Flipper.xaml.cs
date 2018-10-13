using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Comentsys.Community.Controls.UserControls
{
    /// <summary>
    /// Flipper Control
    /// </summary>
    internal sealed partial class Flipper : UserControl
    {
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="value"></param>
        private static void Update(DependencyObject obj,
        DependencyPropertyChangedEventArgs e)
        {
            Flipper flipper = (Flipper)obj;
            flipper.To = (string)e.NewValue;
            if (flipper.From != null)
            {
                if (flipper.From != flipper.To)
                {
                    flipper.TextBlockTop.Text = flipper.TextBlockFlipBottom.Text = flipper.To;
                    flipper.TextBlockFlipTop.Text = flipper.From;
                    flipper.FlipAnimation.Begin();
                    flipper.FlipAnimation.Completed -= (s, ev) => { };
                    flipper.FlipAnimation.Completed += (s, ev) => 
                    flipper.TextBlockBottom.Text = flipper.From;
                }
            }
            if (flipper.From == null)
            {
                flipper.TextBlockFlipTop.Text = flipper.TextBlockBottom.Text = flipper.To;
            }
            flipper.From = flipper.To;
        }

        /// <summary>
        /// To Value
        /// </summary>
        internal string To { get; set; }

        /// <summary>
        /// From Value
        /// </summary>
        internal string From { get; set; }

        /// <summary>
        /// Flipper Control
        /// </summary>
        public Flipper()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Value Property
        /// </summary>
        private readonly DependencyProperty ValueProperty =
        DependencyProperty.Register(nameof(Value), typeof(string),
        typeof(Flipper), new PropertyMetadata(string.Empty,
        new PropertyChangedCallback(Update)));

        public string Value
        {
            get => (string)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }
    }
}
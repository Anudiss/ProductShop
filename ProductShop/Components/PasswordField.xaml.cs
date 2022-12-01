using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ProductShop.Components
{
    /// <summary>
    /// Логика взаимодействия для PasswordField.xaml
    /// </summary>
    public partial class PasswordField : UserControl
    {
        #region Placeholder
        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Placeholder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register("Placeholder", typeof(string), typeof(PasswordField));
        #endregion
        #region Password
        public string Password => PasswordBox.Password;
        #endregion
        #region Label
        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Label.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string), typeof(PasswordField));
        #endregion
        #region LabelFontSize
        public int LabelFontSize
        {
            get { return (int)GetValue(LabelFontSizeProperty); }
            set { SetValue(LabelFontSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LabelFontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelFontSizeProperty =
            DependencyProperty.Register("LabelFontSize", typeof(int), typeof(PasswordField), new PropertyMetadata(14));
        #endregion
        #region LabelFontWeight
        public FontWeight LabelFontWeight
        {
            get { return (FontWeight)GetValue(LabelFontWeightProperty); }
            set { SetValue(LabelFontWeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LabelFontWeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelFontWeightProperty =
            DependencyProperty.Register("LabelFontWeight", typeof(FontWeight), typeof(PasswordField), new PropertyMetadata(FontWeights.SemiBold));
        #endregion
        #region LabelForeground
        public SolidColorBrush LabelForeground
        {
            get { return (SolidColorBrush)GetValue(LabelForegroundProperty); }
            set { SetValue(LabelForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelForegroundProperty =
            DependencyProperty.Register("LabelForeground", typeof(SolidColorBrush), typeof(PasswordField));
        #endregion

        public PasswordField()
        {
            InitializeComponent();
        }
    }
}

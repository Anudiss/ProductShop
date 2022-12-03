using FontAwesome.WPF;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ProductShop.Components
{
    public class NavigationButton : RadioButton
    {
        #region Icon
        public FontAwesomeIcon Icon
        {
            get { return (FontAwesomeIcon)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(FontAwesomeIcon), typeof(NavigationButton));
        #endregion
        #region IconColor
        public SolidColorBrush IconColor
        {
            get { return (SolidColorBrush)GetValue(IconColorProperty); }
            set { SetValue(IconColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IconColorProperty =
            DependencyProperty.Register("IconColor", typeof(SolidColorBrush), typeof(NavigationButton), new PropertyMetadata(new SolidColorBrush(Colors.Black)));
        #endregion
        #region Title
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(NavigationButton));
        #endregion
        #region IndicatorColorBrush
        public SolidColorBrush IndicatorColorBrush
        {
            get { return (SolidColorBrush)GetValue(IndicatorColorBrushProperty); }
            set { SetValue(IndicatorColorBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IndicatorColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IndicatorColorBrushProperty =
            DependencyProperty.Register("IndicatorColorBrush", typeof(SolidColorBrush), typeof(NavigationButton), new PropertyMetadata(Application.Current.FindResource("NavigationButtonIndicatorFill")));
        #endregion

        static NavigationButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof(NavigationButton),
                new FrameworkPropertyMetadata(typeof(NavigationButton)));
        }
    }
}

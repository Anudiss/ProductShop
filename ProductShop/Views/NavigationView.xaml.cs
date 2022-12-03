using FontAwesome.WPF;
using System.Windows;
using System.Windows.Controls;

namespace ProductShop.Views
{
    /// <summary>
    /// Логика взаимодействия для NavigationView.xaml
    /// </summary>
    public partial class NavigationView : UserControl
    {
        #region Icon
        public FontAwesomeIcon Icon
        {
            get { return (FontAwesomeIcon)GetValue(MyPropertyProperty); }
            set { SetValue(MyPropertyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.Register("Icon", typeof(FontAwesomeIcon), typeof(NavigationView));
        #endregion
        #region Title
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Title.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(NavigationView));
        #endregion

        public NavigationView()
        {
            InitializeComponent();
        }
    }
}

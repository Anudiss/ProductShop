﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ProductShop.Components
{
    /// <summary>
    /// Логика взаимодействия для InputField.xaml
    /// </summary>
    public partial class InputField : UserControl
    {
        #region Placeholder
        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Placeholder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register("Placeholder", typeof(string), typeof(InputField));
        #endregion
        #region Text
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Text.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(InputField));
        #endregion
        #region Label
        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Label.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string), typeof(InputField));
        #endregion
        #region LabelFontSize
        public int LabelFontSize
        {
            get { return (int)GetValue(LabelFontSizeProperty); }
            set { SetValue(LabelFontSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LabelFontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelFontSizeProperty =
            DependencyProperty.Register("LabelFontSize", typeof(int), typeof(InputField), new PropertyMetadata(14));
        #endregion
        #region LabelFontWeight
        public FontWeight LabelFontWeight
        {
            get { return (FontWeight)GetValue(LabelFontWeightProperty); }
            set { SetValue(LabelFontWeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LabelFontWeight.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelFontWeightProperty =
            DependencyProperty.Register("LabelFontWeight", typeof(FontWeight), typeof(InputField), new PropertyMetadata(FontWeights.SemiBold));
        #endregion
        #region LabelForeground
        public SolidColorBrush LabelForeground
        {
            get { return (SolidColorBrush)GetValue(LabelForegroundProperty); }
            set { SetValue(LabelForegroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelForegroundProperty =
            DependencyProperty.Register("LabelForeground", typeof(SolidColorBrush), typeof(InputField));
        #endregion

        public InputField()
        {
            InitializeComponent();
        }
    }
}
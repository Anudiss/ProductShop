using ProductShop.Windows.Main;
using System;
using System.Windows;
using System.Windows.Controls;

namespace ProductShop.Components
{
    public class NumericalBox : Control
    {
        private RelayCommand _increementCommand;
        private RelayCommand _decreementCommand;

        public RelayCommand IncreementCommand =>
            _increementCommand ?? (_increementCommand = new RelayCommand((arg) => Number++));
        public RelayCommand DecreementCommand =>
            _decreementCommand ?? (_decreementCommand = new RelayCommand((arg) => Number--));

        #region Number
        public int Number
        {
            get { return (int)GetValue(NumberProperty); }
            set { SetValue(NumberProperty, Math.Max(LowerBound, Math.Min(UpperBound, value))); }
        }

        // Using a DependencyProperty as the backing store for Number.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NumberProperty =
            DependencyProperty.Register("Number", typeof(int), typeof(NumericalBox));
        #endregion
        #region Lower bound
        public int LowerBound
        {
            get { return (int)GetValue(LowerBoundProperty); }
            set { SetValue(LowerBoundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LowerBound.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LowerBoundProperty =
            DependencyProperty.Register("LowerBound", typeof(int), typeof(NumericalBox), new PropertyMetadata(int.MinValue));
        #endregion
        #region Upper bound
        public int UpperBound
        {
            get { return (int)GetValue(UpperBoundProperty); }
            set { SetValue(UpperBoundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UpperBound.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UpperBoundProperty =
            DependencyProperty.Register("UpperBound", typeof(int), typeof(NumericalBox), new PropertyMetadata(int.MaxValue));
        #endregion

        public NumericalBox() =>
            OverridesDefaultStyleProperty.OverrideMetadata(typeof(NumericalBox), new PropertyMetadata(typeof(NumericalBox)));

    }
}

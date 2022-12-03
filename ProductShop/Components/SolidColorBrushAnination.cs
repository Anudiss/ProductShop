using System.Windows.Media;
using System.Windows.Media.Animation;

namespace ProductShop.Components
{
    public class SolidColorBrushAnination : ColorAnimation
    {
        public SolidColorBrush ToBrush
        {
            get => To == null ? null : new SolidColorBrush(To.Value);
            set => To = value?.Color;
        }
    }
}

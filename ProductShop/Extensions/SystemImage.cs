using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ProductShop.Connection
{
    public partial class SystemImage
    {
        public BitmapSource BitmapSource => (BitmapSource)new ImageSourceConverter().ConvertFrom(Data);
    }
}

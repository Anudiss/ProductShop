using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ProductShop.Connection
{
    public partial class Product
    {
        public BitmapSource BitmapSource
        {
            get
            {
                try
                {
                    return (BitmapSource)new ImageSourceConverter().ConvertFrom(Photo);
                }
                catch
                {
                    return SystemImage.GetSystemImage("noimage");
                }
            }
        }
    }
}

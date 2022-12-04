using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ProductShop.Connection
{
    public partial class SystemImage
    {
        public BitmapSource BitmapSource => (BitmapSource)new ImageSourceConverter().ConvertFrom(Data);

        public static BitmapSource GetSystemImage(string name) =>
            DatabaseContext.Entities.SystemImage.Local.FirstOrDefault(image => image.Name == name).BitmapSource;

        public static byte[] GetSystemImageBytes(string name) =>
            DatabaseContext.Entities.SystemImage.Local.FirstOrDefault(image => image.Name == name).Data;
    }
}

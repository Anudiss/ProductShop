using System.Windows.Media;

namespace ProductShop.Connection
{
    public partial class Country
    {
        public Color CountryColor
        {
            get => Color != null ? new Color()
                   {
                       A = Color[0],
                       R = Color[1],
                       G = Color[2],
                       B = Color[3]
                   } : Colors.Black;
            set => Color = new byte[]
                           {
                               value.A,
                               value.R,
                               value.G,
                               value.B,
                           };
        }
    }
}

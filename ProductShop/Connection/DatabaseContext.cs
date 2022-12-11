using System.Data.Entity;

namespace ProductShop.Connection
{
    public static class DatabaseContext
    {
        public static ProductShopEntities Entities { get; }

        static DatabaseContext()
        {
            Entities = new ProductShopEntities();

            Entities.SystemImage.Load();
            Entities.UnitType.Load();
            Entities.Country.Load();
            Entities.Product.Load();
            Entities.Supply.Load();
            Entities.Supplier.Load();
            Entities.Supply_Product.Load();
            Entities.Country_Product.Load();
        }
    }
}

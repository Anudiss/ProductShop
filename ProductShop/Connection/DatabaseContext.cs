using System.Data.Entity;

namespace ProductShop.Connection
{
    public static class DatabaseContext
    {
        public static ProductShopEntities Entities { get; }

        static DatabaseContext()
        {
            Entities = new ProductShopEntities();

            Entities.User.Load();
            Entities.Role.Load();
            Entities.SystemImage.Load();
        }
    }
}

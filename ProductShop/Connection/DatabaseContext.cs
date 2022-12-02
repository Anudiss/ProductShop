﻿using System.Data.Entity;

namespace ProductShop.Connection
{
    public static class DatabaseContext
    {
        public static ProductShopEntities Entities { get; }

        static DatabaseContext()
        {
            Entities = new ProductShopEntities();

            Entities.SystemImage.Load();
        }
    }
}

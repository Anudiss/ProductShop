using System.Collections.Generic;

namespace ProductShop.Permission
{
    public static class Permissions
    {
        public static readonly Dictionary<UserRole, Permission[]> BanPermissions = new Dictionary<UserRole, Permission[]>()
        {
            { UserRole.Customer, new[]
                {
                    Permission.AddProduct,
                    Permission.EditProduct,
                    Permission.RemoveProduct
                }
            },
            { UserRole.Manager, new []
                {
                    Permission.Pay
                }
            }
        };
    }

    public enum Permission
    {
        AddProduct,
        EditProduct,
        RemoveProduct,
        EditOrderCustomer,
        Pay
    }

    public enum UserRole
    {
        Admin = 1,
        Customer,
        Manager,
        Storekeeper
    }
}

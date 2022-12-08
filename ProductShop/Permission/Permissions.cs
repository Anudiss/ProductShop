using System.Collections.Generic;

namespace ProductShop.Permission
{
    public static class Permissions
    {
        public static readonly Dictionary<UserRole, Permission[]> BanPermissions = new Dictionary<UserRole, Permission[]>()
        {
            { UserRole.Manager, new[]
                {
                    Permission.AddProduct
                }
            }
        };
    }

    public enum Permission
    {
        AddProduct
    }

    public enum UserRole
    {
        Admin = 1,
        Customer,
        Manager,
        Storekeeper
    }
}

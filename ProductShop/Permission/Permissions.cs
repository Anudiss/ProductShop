﻿using System.Collections.Generic;

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
                    Permission.RemoveProduct,
                    Permission.Finish
                }
            },
            { UserRole.Manager, new []
                {
                    Permission.AddProduct,
                    Permission.EditProduct,
                    Permission.RemoveProduct,
                    Permission.Pay,
                    Permission.Finish
                }
            },
            { UserRole.Admin, new[]
                {
                    Permission.Pay,
                    Permission.EditOrderCustomer,
                    Permission.Finish
                }
            },
            { UserRole.Storekeeper, new[]
                {
                    Permission.AddProduct,
                    Permission.EditProduct,
                    Permission.RemoveProduct,
                    Permission.Pay,
                    Permission.EditOrderCustomer
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
        Pay,
        Finish
    }

    public enum UserRole
    {
        Admin = 1,
        Customer,
        Manager,
        Storekeeper
    }
}

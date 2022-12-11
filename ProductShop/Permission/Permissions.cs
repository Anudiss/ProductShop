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
                    Permission.RemoveProduct,
                    Permission.EditOrderCustomer,
                    Permission.Finish,
                    Permission.ShowDocument,
                    Permission.AddSupply,
                    Permission.AddDocument
                }
            },
            { UserRole.Manager, new []
                {
                    Permission.AddProduct,
                    Permission.EditProduct,
                    Permission.RemoveProduct,
                    Permission.ShowDocument,
                    Permission.Pay,
                    Permission.Finish,
                    Permission.AddSupply,
                    Permission.AddDocument,
                    Permission.RemoveOrder
                }
            },
            { UserRole.Admin, new[]
                {
                    Permission.Pay,
                    Permission.EditOrderCustomer,
                    Permission.Finish,
                    Permission.AddOrder,
                    Permission.OpenOrder,
                    Permission.AddSupply,
                    Permission.AddDocument,
                    Permission.RemoveOrder
                }
            },
            { UserRole.Storekeeper, new[]
                {
                    Permission.AddProduct,
                    Permission.EditProduct,
                    Permission.AddOrder,
                    Permission.RemoveProduct,
                    Permission.OpenOrder,
                    Permission.Pay,
                    Permission.EditOrderCustomer,
                    Permission.RemoveOrder
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
        OpenOrder,
        AddOrder,
        Finish,
        AddSupply,
        AddDocument,
        ShowDocument,
        RemoveOrder
    }

    public enum UserRole
    {
        Admin = 1,
        Customer,
        Manager,
        Storekeeper
    }
}

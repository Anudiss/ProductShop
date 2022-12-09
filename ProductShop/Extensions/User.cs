using ProductShop.Permission;
using System.Linq;

namespace ProductShop.Connection
{
    public partial class User
    {
        public UserRole UserRole
        {
            get => (UserRole)Role_id;
            set => Role_id = (int)value;
        }

        public Employee Executor => Employee.FirstOrDefault(employee => employee.User == this);
        public Customer SingleCustomer => Customer.FirstOrDefault(customer => customer.User == this);

        public bool HasPermission(Permission.Permission permission) =>
            !Permissions.BanPermissions.ContainsKey(UserRole) || !Permissions.BanPermissions[UserRole].Contains(permission);
    }
}

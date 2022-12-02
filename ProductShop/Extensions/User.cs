using ProductShop.Permission;

namespace ProductShop.Connection
{
    public partial class User
    {
        public UserRole UserRole
        {
            get => (UserRole)Role_id;
            set => Role_id = (int)value;
        }
    }
}

namespace ProductShop.Connection
{
    public partial class Customer
    {
        public string FullName => $"{Surname} {Name[0]}. {Patronymic[0]}.";
    }
}

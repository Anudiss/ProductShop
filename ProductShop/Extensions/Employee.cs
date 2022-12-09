namespace ProductShop.Connection
{
    public partial class Employee
    {
        public string FullName => $"{Surname} {Name[0]}. {Patronymic[0]}.";
    }
}

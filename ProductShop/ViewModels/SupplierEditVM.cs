using ProductShop.Connection;

namespace ProductShop.ViewModels
{
    public class SupplierEditVM : ViewModelBase
    {
        private Supplier _supplier;



        public SupplierEditVM(Supplier supplier = null) =>
            _supplier = supplier ?? new Supplier();

        private void Save()
        {

        }
    }
}

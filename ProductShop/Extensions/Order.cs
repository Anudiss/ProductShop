namespace ProductShop.Connection
{
    public partial class Order
    {
        public Stage Stage
        {
            get => (Stage)OrderStage_id;
            set => OrderStage_id = (int)value;
        }
    }

    public enum Stage
    {
        New = 1,
        Processing,
        Denied,
        ForPayment,
        Paid,
        Execution,
        Done
    }
}

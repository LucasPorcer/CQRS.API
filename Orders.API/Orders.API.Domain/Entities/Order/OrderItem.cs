namespace Orders.API.Domain.Entities.Order
{
    public class OrderItem
    {
        public string ItemSku { get; set; }
        public int ItemCode { get; set; }
        public string ItemDescription { get; set; }

        public OrderItem(string itemSku, int itemCode, string itemDescription)
        {
            ItemSku = itemSku;
            ItemCode = itemCode;
            ItemDescription = itemDescription;
        }
    }
}

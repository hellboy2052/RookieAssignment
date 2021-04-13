namespace ShareVM
{
    public class OrderDetailVm
    {
        public int orderId { get; set; }

        public int productId { get; set; }

        public string Image { get; set; }

        public string BrandName { get; set; }
        public string Name { get; set; }
        public decimal price { get; set; }

        public int quantity { get; set; }
        
        public decimal totalPrice { get; set; }
    }
}
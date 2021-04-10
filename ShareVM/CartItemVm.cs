namespace ShareVM
{
    public class CartItemVm
    {
        public int productId { get; set; }

        public ProductVm Product { get; set; }

        public int quantity { get; set; }
    }
}
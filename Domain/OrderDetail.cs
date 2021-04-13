using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class OrderDetail
    {
        public int orderId { get; set; }
        public Order Order { get; set; }

        [ForeignKey("Product")]
        public int productId { get; set; }
        public Product Product { get; set; }

        public decimal price { get; set; }

        public int quantity { get; set; }
        
        public decimal totalPrice { get; set; }
        
    }
}
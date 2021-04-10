using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class CartItem
    {
        public string userId { get; set; }
        
        public User User { get; set; }


        [ForeignKey("Product")]
        public int productId { get; set; }

        public Product Product { get; set; }

        public int quantity { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string userId { get; set; }

        public User User { get; set; }

        public ICollection<OrderDetail> orders { get; set; } = new List<OrderDetail>();
    }
}
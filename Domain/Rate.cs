using System;

namespace Domain
{
    public class Rate
    {
        public int productId { get; set; }

        public Product product { get; set; }

        public string userId { get; set; }

        public User user { get; set; }

        public double rate { get; set; }

        public DateTime createdAt { get; set; } = DateTime.UtcNow;
    }
}
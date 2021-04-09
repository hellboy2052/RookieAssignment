using System;

namespace ShareVM
{
    public class RatingVm
    {
        
        public int productId { get; set; }

        public string userId { get; set; }

        public double rate { get; set; }

        public DateTime createdAt { get; set; } = DateTime.Now;
    }
}
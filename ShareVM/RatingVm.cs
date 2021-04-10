using System;

namespace ShareVM
{
    public class RatingVm
    {
        public double rate { get; set; }

        public DateTime createdAt { get; set; } = DateTime.Now;
    }
}
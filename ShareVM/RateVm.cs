using System;

namespace ShareVM
{
    public class RateVm
    {
        public double rate { get; set; }

        public DateTime createdAt { get; set; } = DateTime.UtcNow;
    }
}
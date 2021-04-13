using System;
using System.Collections.Generic;

namespace ShareVM
{
    public class OrderVm
    {
        public int Id { get; set; }
        public string userId { get; set; }
        public string Username { get; set; }

        public string Fullname { get; set; }
        
        public DateTime CreatedAt { get; set; }


        public ICollection<OrderDetailVm> orders { get; set; }
        
    }
}
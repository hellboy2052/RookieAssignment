using System.Collections.Generic;
using Domain;

namespace ShareVM
{
    public class ProfileVm
    {
        public string Username { get; set; }

        public string Fullname { get; set; }

        public ICollection<CartItemVm> Cart { get; set; }

        public ICollection<RateVm> rating { get; set; }
    }
}
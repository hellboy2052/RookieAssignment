using System.Collections.Generic;

namespace ShareVM
{
    public class AccountVm
    {
        public string Email { get; set; }

        public string Username { get; set; }

        public string Fullname { get; set; }

        public IList<string> Roles { get; set; }
    }
}
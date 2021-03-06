using System.Collections.Generic;

namespace ShareVM
{
    public class UserVm
    {
        public string Username { get; set; }

        public string FullName { get; set; }

        public string Token { get; set; }

        public IList<string> Roles { get; set; }
    }
}
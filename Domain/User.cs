using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class User : IdentityUser
    {
        public User() : base()
        {
        }

        public User(string username) : base(username){

        }

        [PersonalData]
        public string FullName { get; set; }

        public ICollection<Rating> rating { get; set; }
    }
}
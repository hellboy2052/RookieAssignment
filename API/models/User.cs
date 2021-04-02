using Microsoft.AspNetCore.Identity;

namespace API.models
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
    }
}
using Microsoft.AspNetCore.Identity;

namespace IdentityApp.Data.Entity
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

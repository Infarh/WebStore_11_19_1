using Microsoft.AspNetCore.Identity;

namespace WebStore.Domain.Entities.Identity
{
    public class User : IdentityUser
    {
        public const string Administrator = "Administrator";
        public const string AdminPasswordDefault = "AdminPassword";

        public string Description { get; set; }
    }
}

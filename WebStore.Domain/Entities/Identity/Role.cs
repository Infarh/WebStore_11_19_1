using Microsoft.AspNetCore.Identity;

namespace WebStore.Domain.Entities.Identity
{
    public class Role : IdentityRole
    {
        public const string Administrator = "Administrator";
        public const string User = "User";

        public string Description { get; set; }
    }
}
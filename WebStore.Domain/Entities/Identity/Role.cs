using Microsoft.AspNetCore.Identity;

namespace WebStore.Domain.Entities.Identity
{
    public class Role : IdentityRole
    {
        public string Description { get; set; }
    }
}
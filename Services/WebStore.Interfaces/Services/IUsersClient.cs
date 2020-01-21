using Microsoft.AspNetCore.Identity;
using WebStore.Domain.Entities.Identity;

namespace WebStore.Interfaces.Services
{
    public interface IUsersClient :
        IUserRoleStore<User>,
        IUserPasswordStore<User>,
        IUserEmailStore<User>,
        IUserPhoneNumberStore<User>,
        IUserClaimStore<User>,
        IUserTwoFactorStore<User>,
        IUserLockoutStore<User>,
        IUserLoginStore<User>
    {
    }
}

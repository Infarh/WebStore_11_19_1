using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebStore.Clients.Base;
using WebStore.Domain.DTO.Identity;
using WebStore.Domain.Entities.Identity;
using WebStore.Interfaces.Services;

namespace WebStore.Clients.Identity
{
    public class UsersClient : BaseClient, IUsersClient
    {
        private readonly ILogger<UsersClient> _Logger;
        public UsersClient(IConfiguration config, ILogger<UsersClient> Logger) : base(config, "api/users") => _Logger = Logger;

        #region Implementation of IUserStore<User>

        public async Task<string> GetUserIdAsync(User user, CancellationToken cancel)
        {
            return await (await PostAsync($"{_ServiceAddress}/UserId", user, cancel))
               .Content
               .ReadAsAsync<string>(cancel)
               .ConfigureAwait(false);
        }

        public async Task<string> GetUserNameAsync(User user, CancellationToken cancel)
        {
            return await (await PostAsync($"{_ServiceAddress}/UserName", user, cancel))
               .Content
               .ReadAsAsync<string>(cancel)
               .ConfigureAwait(false);
        }

        public async Task SetUserNameAsync(User user, string name, CancellationToken cancel)
        {
            _Logger.LogInformation("Изменение имени пользователя {0} с {1} на {2}", user.Id, user.UserName, name);
            user.UserName = name;

            await PostAsync($"{_ServiceAddress}/UserName/{name}", user, cancel);
        }

        public async Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancel)
        {
            return await (await PostAsync($"{_ServiceAddress}/NormalUserName/", user, cancel))
               .Content
               .ReadAsAsync<string>(cancel)
               .ConfigureAwait(false);
        }

        public async Task SetNormalizedUserNameAsync(User user, string name, CancellationToken cancel)
        {
            user.NormalizedUserName = name;
            await PostAsync($"{_ServiceAddress}/NormalUserName/{name}", user, cancel);
        }

        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancel)
        {
            return await (await PostAsync($"{_ServiceAddress}/User", user, cancel))
               .Content
               .ReadAsAsync<bool>(cancel)
                ? IdentityResult.Success
                : IdentityResult.Failed();
        }

        public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancel)
        {
            return await (await PutAsync($"{_ServiceAddress}/User", user, cancel))
               .Content
               .ReadAsAsync<bool>(cancel)
                ? IdentityResult.Success
                : IdentityResult.Failed();
        }

        public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancel)
        {
            return await (await PostAsync($"{_ServiceAddress}/User/Delete", user, cancel))
               .Content
               .ReadAsAsync<bool>(cancel)
                ? IdentityResult.Success
                : IdentityResult.Failed();
        }

        public async Task<User> FindByIdAsync(string id, CancellationToken cancel)
        {
            return await GetAsync<User>($"{_ServiceAddress}/User/Find/{id}", cancel);
        }

        public async Task<User> FindByNameAsync(string name, CancellationToken cancel)
        {
            return await GetAsync<User>($"{_ServiceAddress}/User/Normal/{name}", cancel);
        }

        #endregion

        #region Implementation of IUserRoleStore<User>

        public async Task AddToRoleAsync(User user, string role, CancellationToken cancel)
        {
            _Logger.LogInformation("Пользователю {0} назначается роль {1}", user.UserName ?? user.Id, role);
            await PostAsync($"{_ServiceAddress}/Role/{role}", user, cancel).ConfigureAwait(false);
        }

        public async Task RemoveFromRoleAsync(User user, string role, CancellationToken cancel)
        {
            _Logger.LogInformation("У пользователя {0} удаляется роль {1}", user.UserName ?? user.Id, role);
            await PostAsync($"{_ServiceAddress}/Role/Delete/{role}", user, cancel);
        }

        public async Task<IList<string>> GetRolesAsync(User user, CancellationToken cancel)
        {
            return await (await PostAsync($"{_ServiceAddress}/roles", user, cancel))
               .Content
               .ReadAsAsync<IList<string>>(cancel);
        }

        public async Task<bool> IsInRoleAsync(User user, string role, CancellationToken cancel)
        {
            return await (await PostAsync($"{_ServiceAddress}/InRole/{role}", user, cancel))
               .Content
               .ReadAsAsync<bool>(cancel);
        }

        public async Task<IList<User>> GetUsersInRoleAsync(string role, CancellationToken cancel)
        {
            return await GetAsync<List<User>>($"{_ServiceAddress}/UsersInRole/{role}", cancel);
        }

        #endregion

        #region Implementation of IUserPasswordStore<User>

        public async Task SetPasswordHashAsync(User user, string hash, CancellationToken cancel)
        {
            user.PasswordHash = hash;
            await PostAsync(
                $"{_ServiceAddress}/SetPasswordHash", new PasswordHashDTO { Hash = hash, User = user },
                cancel);
        }

        public async Task<string> GetPasswordHashAsync(User user, CancellationToken cancel)
        {
            return await (await PostAsync($"{_ServiceAddress}/GetPasswordHash", user, cancel))
               .Content
               .ReadAsAsync<string>(cancel);
        }

        public async Task<bool> HasPasswordAsync(User user, CancellationToken cancel)
        {
            return await (await PostAsync($"{_ServiceAddress}/HasPassword", user, cancel))
               .Content
               .ReadAsAsync<bool>(cancel);
        }

        #endregion

        #region Implementation of IUserEmailStore<User>

        public async Task SetEmailAsync(User user, string email, CancellationToken cancel)
        {
            user.Email = email;
            await PostAsync($"{_ServiceAddress}/SetEmail/{email}", user, cancel);
        }

        public async Task<string> GetEmailAsync(User user, CancellationToken cancel)
        {
            return await (await PostAsync($"{_ServiceAddress}/GetEmail", user, cancel))
               .Content
               .ReadAsAsync<string>(cancel);
        }

        public async Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancel)
        {
            return await (await PostAsync($"{_ServiceAddress}/GetEmailConfirmed", user, cancel))
               .Content
               .ReadAsAsync<bool>(cancel);
        }

        public async Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancel)
        {
            user.EmailConfirmed = confirmed;
            await PostAsync($"{_ServiceAddress}/SetEmailConfirmed/{confirmed}", user, cancel);
        }

        public async Task<User> FindByEmailAsync(string email, CancellationToken cancel)
        {
            return await GetAsync<User>($"{_ServiceAddress}/User/FindByEmail/{email}", cancel);
        }

        public async Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancel)
        {
            return await (await PostAsync($"{_ServiceAddress}/User/GetNormalizedEmail", user, cancel))
               .Content
               .ReadAsAsync<string>(cancel);
        }

        public async Task SetNormalizedEmailAsync(User user, string email, CancellationToken cancel)
        {
            user.NormalizedEmail = email;
            await PostAsync($"{_ServiceAddress}/SetNormalizedEmail/{email}", user, cancel);
        }

        #endregion

        #region Implementation of IUserPhoneNumberStore<User>

        public async Task SetPhoneNumberAsync(User user, string phone, CancellationToken cancel)
        {
            user.PhoneNumber = phone;
            await PostAsync($"{_ServiceAddress}/SetPhoneNumber/{phone}", user, cancel);
        }

        public async Task<string> GetPhoneNumberAsync(User user, CancellationToken cancel)
        {
            return await (await PostAsync($"{_ServiceAddress}/GetPhoneNumber", user, cancel))
               .Content
               .ReadAsAsync<string>(cancel);
        }

        public async Task<bool> GetPhoneNumberConfirmedAsync(User user, CancellationToken cancel)
        {
            return await (await PostAsync($"{_ServiceAddress}/GetPhoneNumberConfirmed", user, cancel))
               .Content
               .ReadAsAsync<bool>(cancel);
        }

        public async Task SetPhoneNumberConfirmedAsync(User user, bool confirmed, CancellationToken cancel)
        {
            user.PhoneNumberConfirmed = confirmed;
            await PostAsync($"{_ServiceAddress}/SetPhoneNumberConfirmed/{confirmed}", user, cancel);
        }

        #endregion

        #region Implementation of IUserLoginStore<User>

        public async Task AddLoginAsync(User user, UserLoginInfo login, CancellationToken cancel)
        {
            await PostAsync($"{_ServiceAddress}/AddLogin", new AddLoginDTO { User = user, UserLoginInfo = login }, cancel);
        }

        public async Task RemoveLoginAsync(User user, string LoginProvider, string ProviderKey, CancellationToken cancel)
        {
            await PostAsync($"{_ServiceAddress}/RemoveLogin/{LoginProvider}/{ProviderKey}", user, cancel);
        }

        public async Task<IList<UserLoginInfo>> GetLoginsAsync(User user, CancellationToken cancel)
        {
            return await (await PostAsync($"{_ServiceAddress}/GetLogins", user, cancel))
               .Content
               .ReadAsAsync<List<UserLoginInfo>>(cancel);
        }

        public async Task<User> FindByLoginAsync(string LoginProvider, string ProviderKey, CancellationToken cancel)
        {
            return await GetAsync<User>($"{_ServiceAddress}/User/FindByLogin/{LoginProvider}/{ProviderKey}", cancel);
        }

        #endregion

        #region Implementation of IUserLockoutStore<User>

        public async Task<DateTimeOffset?> GetLockoutEndDateAsync(User user, CancellationToken cancel)
        {
            return await (await PostAsync($"{_ServiceAddress}/GetLockoutEndDate", user, cancel))
               .Content
               .ReadAsAsync<DateTimeOffset?>(cancel);
        }

        public async Task SetLockoutEndDateAsync(User user, DateTimeOffset? EndDate, CancellationToken cancel)
        {
            user.LockoutEnd = EndDate;
            await PostAsync(
                $"{_ServiceAddress}/SetLockoutEndDate",
                new SetLockoutDTO { User = user, LockoutEnd = EndDate }, cancel);
        }

        public async Task<int> IncrementAccessFailedCountAsync(User user, CancellationToken cancel)
        {
            return await (await PostAsync($"{_ServiceAddress}/IncrementAccessFailedCount", user, cancel))
               .Content
               .ReadAsAsync<int>(cancel);
        }

        public async Task ResetAccessFailedCountAsync(User user, CancellationToken cancel)
        {
            await PostAsync($"{_ServiceAddress}/ResetAccessFailedCont", user, cancel);
        }

        public async Task<int> GetAccessFailedCountAsync(User user, CancellationToken cancel)
        {
            return await (await PostAsync($"{_ServiceAddress}/GetAccessFailedCount", user, cancel))
               .Content
               .ReadAsAsync<int>(cancel);
        }

        public async Task<bool> GetLockoutEnabledAsync(User user, CancellationToken cancel)
        {
            return await (await PostAsync($"{_ServiceAddress}/GetLockoutEnabled", user, cancel))
               .Content
               .ReadAsAsync<bool>(cancel);
        }

        public async Task SetLockoutEnabledAsync(User user, bool enabled, CancellationToken cancel)
        {
            user.LockoutEnabled = enabled;
            await PostAsync($"{_ServiceAddress}/SetLockoutEnabled/{enabled}", user, cancel);
        }

        #endregion

        #region Implementation of IUserTwoFactorStore<User>

        public async Task SetTwoFactorEnabledAsync(User user, bool enabled, CancellationToken cancel)
        {
            user.TwoFactorEnabled = enabled;
            await PostAsync($"{_ServiceAddress}/SetTwoFactor/{enabled}", user, cancel);
        }

        public async Task<bool> GetTwoFactorEnabledAsync(User user, CancellationToken cancel)
        {
            return await (await PostAsync($"{_ServiceAddress}/GetTwoFactorEnabled", user, cancel))
               .Content
               .ReadAsAsync<bool>(cancel);
        }

        #endregion

        #region Implementation of IUserClaimStore<User>

        public async Task<IList<Claim>> GetClaimsAsync(User user, CancellationToken cancel)
        {
            return await (await PostAsync($"{_ServiceAddress}/GetClaims", user, cancel))
               .Content
               .ReadAsAsync<List<Claim>>(cancel);
        }

        public async Task AddClaimsAsync(User user, IEnumerable<Claim> claims, CancellationToken cancel)
        {
            await PostAsync($"{_ServiceAddress}/AddClaims", new AddClaimDTO { User = user, Claims = claims }, cancel);
        }

        public async Task ReplaceClaimAsync(User user, Claim OldClaim, Claim NewClaim, CancellationToken cancel)
        {
            await PostAsync(
                $"{_ServiceAddress}/ReplaceClaim",
                new ReplaceClaimDTO { User = user, Claim = OldClaim, NewClaim = NewClaim }, cancel);
        }

        public async Task RemoveClaimsAsync(User user, IEnumerable<Claim> claims, CancellationToken cancel)
        {
            await PostAsync(
                $"{_ServiceAddress}/RemoveClaims", new RemoveClaimDTO { User = user, Claims = claims },
                cancel);
        }

        public async Task<IList<User>> GetUsersForClaimAsync(Claim claim, CancellationToken cancel)
        {
            return await (await PostAsync($"{_ServiceAddress}/GetUsersForClaim", claim, cancel))
               .Content
               .ReadAsAsync<List<User>>(cancel);
        }

        #endregion
    }
}

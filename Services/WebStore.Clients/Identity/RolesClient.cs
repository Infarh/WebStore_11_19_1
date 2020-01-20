using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using WebStore.Clients.Base;
using WebStore.Domain.Entities.Identity;
using WebStore.Interfaces.Services;

namespace WebStore.Clients.Identity
{
    public class RolesClient : BaseClient, IRolesClient
    {
        public RolesClient(IConfiguration config) : base(config, "api/roles") { }

        #region IRoleStore<Role>

        public async Task<IdentityResult> CreateAsync(Role role, CancellationToken cancel) =>
            await (await PostAsync(_ServiceAddress, role, cancel))
                .Content
                .ReadAsAsync<bool>(cancel)
                ? IdentityResult.Success
                : IdentityResult.Failed();

        public async Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancel) =>
            await (await PutAsync(_ServiceAddress, role, cancel))
                .Content
                .ReadAsAsync<bool>(cancel)
                ? IdentityResult.Success
                : IdentityResult.Failed();

        public async Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancel) =>
            await (await PostAsync($"{_ServiceAddress}/Delete", role, cancel))
                .Content
                .ReadAsAsync<bool>(cancel)
                ? IdentityResult.Success
                : IdentityResult.Failed();

        public async Task<string> GetRoleIdAsync(Role role, CancellationToken cancel) =>
            await (await PostAsync($"{_ServiceAddress}/GetRoleId", role, cancel))
                .Content
                .ReadAsAsync<string>(cancel);

        public async Task<string> GetRoleNameAsync(Role role, CancellationToken cancel) =>
            await (await PostAsync($"{_ServiceAddress}/GetRoleName", role, cancel))
                .Content
                .ReadAsAsync<string>(cancel);

        public async Task SetRoleNameAsync(Role role, string name, CancellationToken cancel)
        {
            role.Name = name;
            await PostAsync($"{_ServiceAddress}/SetRoleName/{name}", role, cancel);
        }

        public async Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancel) =>
            await (await PostAsync($"{_ServiceAddress}/GetNormalizedRoleName", role, cancel))
                .Content
                .ReadAsAsync<string>(cancel);

        public async Task SetNormalizedRoleNameAsync(Role role, string name, CancellationToken cancel)
        {
            role.NormalizedName = name;
            await PostAsync($"{_ServiceAddress}/SetNormalizedRoleName/{name}", role, cancel);
        }

        public async Task<Role> FindByIdAsync(string id, CancellationToken cancel) =>
            await GetAsync<Role>($"{_ServiceAddress}/FindById/{id}", cancel);

        public async Task<Role> FindByNameAsync(string name, CancellationToken cancel) =>
            await GetAsync<Role>($"{_ServiceAddress}/FindByName/{name}", cancel);

        #endregion
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;

namespace WebStore.ServiceHosting.Controllers
{
    [Route("api/roles")]
    //[Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class RolesApiController : ControllerBase
    {
        private readonly RoleStore<Role> _RoleStore;

        public RolesApiController(WebStoreContext db) => _RoleStore = new RoleStore<Role>(db);

        /* ---------------------------------------------------------------- */

        [HttpGet("AllRoles")]
        public async Task<IEnumerable<Role>> GetAllRoles() => await _RoleStore.Roles.ToArrayAsync();

        /* ---------------------------------------------------------------- */

        [HttpPost]
        public async Task<bool> CreateAsync(Role role) => (await _RoleStore.CreateAsync(role)).Succeeded;

        [HttpPut]
        public async Task<bool> UpdateAsync(Role role) => (await _RoleStore.UpdateAsync(role)).Succeeded;

        [HttpPost("Delete")]
        public async Task<bool> DeleteAsync(Role role) => (await _RoleStore.DeleteAsync(role)).Succeeded;

        [HttpPost("GetRoleId")]
        public async Task<string> GetRoleIdAsync(Role role) => await _RoleStore.GetRoleIdAsync(role);

        [HttpPost("GetRoleName")]
        public async Task<string> GetRoleNameAsync(Role role) => await _RoleStore.GetRoleNameAsync(role);

        [HttpPost("SetRoleName/{name}")]
        public async Task SetRoleNameAsync(Role role, string name) => await _RoleStore.SetRoleNameAsync(role, name);

        [HttpPost("GetNormalizedRoleName")]
        public async Task<string> GetNormalizedRoleNameAsync(Role role) => await _RoleStore.GetNormalizedRoleNameAsync(role);

        [HttpPost("SetNormalizedRoleName/{name}")]
        public async Task SetNormalizedRoleNameAsync(Role role, string name) => await _RoleStore.SetNormalizedRoleNameAsync(role, name);

        [HttpGet("FindById/{id}")]
        public async Task<Role> FindByIdAsync(string id) => await _RoleStore.FindByIdAsync(id);

        [HttpGet("FindByName/{name}")]
        public async Task<Role> FindByNameAsync(string name) => await _RoleStore.FindByNameAsync(name);
    }
}
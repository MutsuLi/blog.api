using System.Threading.Tasks;
using Blog.IServices.Base;
using Blog.Api.Models;
using System.Collections.Generic;

namespace Blog.IServices
{
    /// <summary>
    /// RoleServices
    /// </summary>	
    public interface IRoleServices : IBaseServices<Role>
    {
        Task<Role> SaveRole(string roleName);
        Task<string> GetLastRoleIdByUid(int rid);
        Task<List<string>> GetRoleNameByRid(object[] rids);
    }
}

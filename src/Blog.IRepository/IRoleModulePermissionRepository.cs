using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.IRepository.Base;
using Blog.Api.Models;

namespace Blog.IRepository
{
    public interface IRoleModulePermissionRepository : IBaseRepository<RoleModulePermission>
    {
        //Task<List<TestMuchTableResult>> QueryMuchTable();
        Task<List<RoleModulePermission>> RoleModuleMaps();
        Task<List<RoleModulePermission>> GetRMPMaps();
    }
}

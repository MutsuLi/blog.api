using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Common;
using Blog.IRepository;
using Blog.IServices;
using Blog.Model.Models;
using Blog.Services.Base;

namespace Blog.Services
{
    public class RoleModulePermissionServices : BaseServices<RoleModulePermission>, IRoleModulePermissionServices
    {
        private readonly IRoleModulePermissionRepository _dal;
        private readonly IModuleRepository _moduleRepository;
        private readonly IRoleRepository _roleRepository;


        public RoleModulePermissionServices(IRoleModulePermissionRepository dal, IModuleRepository moduleRepository, IRoleRepository roleRepository)
        {
            this._dal = dal;
            this._moduleRepository = moduleRepository;
            this._roleRepository = roleRepository;
            base.BaseDal = dal;
        }

        /// <summary>
        /// 获取全部 角色接口(按钮)关系数据 注意我使用咱们之前的AOP缓存，很好的应用上了
        /// </summary>
        /// <returns></returns>
        [Caching(AbsoluteExpiration = 10)]
        public async Task<List<RoleModulePermission>> GetRoleModule()
        {
            var roleModulePermissions = await _dal.Query(a => a.IsDeleted == false);
            if (roleModulePermissions.Count > 0)
            {
                foreach (var item in roleModulePermissions)
                {
                    item.Role = await _roleRepository.QueryById(item.RoleId);
                    item.Module = await _moduleRepository.QueryById(item.ModuleId);
                }

            }
            return roleModulePermissions;
        }

        public Task<List<RoleModulePermission>> RoleModuleMaps()
        {
            throw new NotImplementedException();
        }

        public Task<List<RoleModulePermission>> TestModelWithChildren()
        {
            throw new NotImplementedException();
        }
    }
}

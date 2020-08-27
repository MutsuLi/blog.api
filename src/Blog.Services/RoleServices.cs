using System.Linq;
using System.Threading.Tasks;
using Blog.Common;
using Blog.IRepository;
using Blog.IServices;
using Blog.Api.Models;
using Blog.Services.Base;
using System.Collections.Generic;

namespace Blog.Core.Services
{
    /// <summary>
    /// RoleServices
    /// </summary>	
    public class RoleServices : BaseServices<Role>, IRoleServices
    {

        IRoleRepository _dal;
        public RoleServices(IRoleRepository dal)
        {
            this._dal = dal;
            base.BaseDal = dal;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public async Task<Role> SaveRole(string roleName)
        {
            Role role = new Role(roleName);
            Role model = new Role();
            var userList = await base.Query(a => a.Name == role.Name && a.Enabled);
            if (userList.Count > 0)
            {
                model = userList.FirstOrDefault();
            }
            else
            {
                var id = await base.Add(role);
                model = await base.QueryById(id);
            }

            return model;

        }

        [Caching(AbsoluteExpiration = 30)]
        public async Task<string> GetLastRoleIdByUid(int rid)
        {
            return ((await base.QueryById(rid))?.Name);
        }

        [Caching(AbsoluteExpiration = 30)]
        public async Task<List<string>> GetRoleNameByRid(object[] rids)
        {
            return ((await base.QueryByIDs(rids)).OrderByDescending(d => d.Id).Select(d => d.Name).ToList());
        }
    }
}

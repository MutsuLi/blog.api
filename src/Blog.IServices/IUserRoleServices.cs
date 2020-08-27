using System.Threading.Tasks;
using Blog.IServices.Base;
using Blog.Api.Models;
using System.Collections.Generic;
namespace Blog.IServices
{
    /// <summary>
    /// UserRoleServices
    /// </summary>	
    public interface IUserRoleServices : IBaseServices<UserRole>
    {

        Task<UserRole> SaveUserRole(int uid, int rid);
        Task<int> GetLastRoleIdByUid(int uid);
        Task<List<int>> GetRoleIdByUid(int uid);
    }
}


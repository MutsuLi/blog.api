using System.Threading.Tasks;
using Blog.IServices.Base;
using Blog.Model.Models;

namespace Blog.IServices
{
    /// <summary>
    /// sysUserInfoServices
    /// </summary>	
    public interface ISysUserInfoServices :IBaseServices<sysUserInfo>
	{
        Task<sysUserInfo> SaveUserInfo(string loginId, string loginPwd, string Uname = "", string desc = "", string title = "");
        Task<string> GetUserRoleNameStr(string loginName, string loginPwd);
    }
}

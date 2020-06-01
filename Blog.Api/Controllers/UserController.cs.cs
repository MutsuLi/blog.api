using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Blog.IServices;
using Blog.Common;
using Blog.Common.Helper;
using Blog.Common.HttpContextUser;
using Blog.Model;
using Blog.Model.Models;
using Microsoft.Extensions.Logging;
using Blog.IRepository.IUnitOfWork;
using System.Threading.Tasks;
using Blog.Models;
using System.Linq.Expressions;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Blog.Api.Controllers
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize(Permissions.Name)]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        readonly ISysUserInfoServices _sysUserInfoServices;
        readonly IUserRoleServices _userRoleServices;
        readonly IRoleServices _roleServices;
        //private readonly IUser _user;
        private readonly ILogger<UserController> _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="sysUserInfoServices"></param>
        /// <param name="userRoleServices"></param>
        /// <param name="roleServices"></param>
        /// <param name="user"></param>
        /// <param name="logger"></param>
        public UserController(IUnitOfWork unitOfWork, ISysUserInfoServices sysUserInfoServices, IUserRoleServices userRoleServices, IRoleServices roleServices, ILogger<UserController> logger)
        {
            _unitOfWork = unitOfWork;
            _sysUserInfoServices = sysUserInfoServices;
            _userRoleServices = userRoleServices;
            _roleServices = roleServices;
            //_user = user;
            _logger = logger;
        }


        /// <summary>
        /// 获取用户详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<object> Get(int id)
        {
            var data = await _sysUserInfoServices.QueryById(id);

            return new MessageModel<sysUserInfo>()
            {
                msg = "获取成功",
                success = true,
                response = data
            };
        }

        /// <summary>
        /// 获取全部用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("list")]
        public async Task<MessageModel<PageModel<sysUserInfo>>> Get(int page = 1, int pageSize = 25, string key = "")
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrWhiteSpace(key))
            {
                key = "";
            }

            var data = await _sysUserInfoServices.QueryPage(a => a.tdIsDelete != true && a.uStatus >= 0 && ((a.uLoginName != null && a.uLoginName.Contains(key)) || (a.uRealName != null && a.uRealName.Contains(key))), page, pageSize, " uID desc ");

            // 这里可以封装到多表查询，此处简单处理
            var allUserRoles = await _userRoleServices.Query(d => d.IsDeleted == false);
            var allRoles = await _roleServices.Query(d => d.IsDeleted == false);

            var sysUserInfos = data.data;
            foreach (var item in sysUserInfos)
            {
                var currentUserRoles = allUserRoles.Where(d => d.UserId == item.uID).Select(d => d.RoleId).ToList();
                item.RIDs = currentUserRoles;
                item.RoleNames = allRoles.Where(d => currentUserRoles.Contains(d.Id)).Select(d => d.Name).ToList();
            }
            data.data = sysUserInfos;


            return new MessageModel<PageModel<sysUserInfo>>()
            {
                msg = "获取成功",
                success = data.dataCount >= 0,
                response = data
            };

        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="BlogArticle"></param>
        /// <returns></returns>
        // PUT: api/User/5
        [HttpPut]
        [Route("Update")]
        public async Task<MessageModel<string>> Put([FromBody] sysUserInfo sysUserInfo)
        {
            var data = new MessageModel<string>();
            if (sysUserInfo == null || sysUserInfo.uID <= 0)
            {
                return data;
            }

            try
            {
                _unitOfWork.BeginTran();
                // 无论 Update Or Add , 先删除当前用户的全部 U_R 关系
                var usreroles = (await _userRoleServices.Query(d => d.UserId == sysUserInfo.uID)).Select(d => d.Id.ToString()).ToArray();
                if (usreroles.Count() > 0)
                {
                    var isAllDeleted = await _userRoleServices.DeleteByIds(usreroles);
                }

                // 然后再执行添加操作
                var userRolsAdd = new List<UserRole>();
                sysUserInfo.RIDs.ForEach(rid =>
                {
                    userRolsAdd.Add(new UserRole(sysUserInfo.uID, rid));
                });

                await _userRoleServices.Add(userRolsAdd);
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/ApiWithActions/5
        [HttpDelete]
        public async Task<MessageModel<string>> Delete(int id)
        {


        }
    }
}

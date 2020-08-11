using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Model.ViewModels
{
    /// <summary>
    /// 用户信息DTO
    /// </summary>
    public class sysUserInfoModels
    {
        /// 用户ID
        public int uId { get; set; }

        /// 登录邮箱
        public string uEmail { get; set; }

        /// 昵称
        public string uName { get; set; }

        /// 昵称
        public string uAvatar { get; set; }

        /// 状态
        public int uStatus { get; set; }

        /// 职位/头衔
        public string uTitle { get; set; }

        /// 简介
        public string uDescription { get; set; }

        /// 备注
        public string uRemark { get; set; }
        /// 创建时间
        public DateTime uCreateTime { get; set; } = DateTime.Now;
        public DateTime uUpdateTime { get; set; } = DateTime.Now;

        ///最后登录时间 
        public DateTime uLastErrTime { get; set; } = DateTime.Now;

        ///错误次数 
        public int uErrorCount { get; set; }

        // 性别
        public int gender { get; set; } = 0;
        // 年龄
        public int age { get; set; }
        // 生日
        public DateTime birth { get; set; } = DateTime.Now;
        // 地址
        public string addr { get; set; }
        public bool tdIsDelete { get; set; }

        public List<int> RIDs { get; set; }
        public List<string> RoleNames { get; set; }

    }
}

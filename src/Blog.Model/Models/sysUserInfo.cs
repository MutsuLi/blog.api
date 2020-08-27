using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Api.Models
{
    /// <summary>
    /// 用户信息表
    /// </summary>
    public class sysUserInfo
    {
        public sysUserInfo() { }

        public sysUserInfo(string email, string password, string userName = "", string avatars = "", string desc = "", string title = "")
        {
            uEmail = email;
            uPassword = password;
            uName = userName;
            uName = avatars;
            uStatus = 0;
            uCreateTime = DateTime.Now;
            uUpdateTime = DateTime.Now;
            uLastErrTime = DateTime.Now;
            uErrorCount = 0;
            uDescription = desc;
            uTitle = title;
        }

        /// <summary>
        /// 用户ID
        /// </summary>
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int uId { get; set; }

        /// <summary>
        /// 登录邮箱
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string uEmail { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string uPassword { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string uName { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string uAvatar { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int uStatus { get; set; }

        // <summary>
        /// 职位/头衔
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string uTitle { get; set; }

        // <summary>
        /// 简介
        /// </summary>
        [SugarColumn(ColumnDataType = "text", IsNullable = true)]
        public string uDescription { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(ColumnDataType = "text", IsNullable = true)]
        public string uRemark { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime uCreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime uUpdateTime { get; set; } = DateTime.Now;

        /// <summary>
        ///最后登录时间 
        /// </summary>
        public DateTime uLastErrTime { get; set; } = DateTime.Now;

        /// <summary>
        ///错误次数 
        /// </summary>
        public int uErrorCount { get; set; }

        // 性别
        [SugarColumn(IsNullable = true)]
        public int gender { get; set; } = 0;
        // 年龄
        [SugarColumn(IsNullable = true)]
        public int age { get; set; }
        // 生日
        [SugarColumn(IsNullable = true)]
        public DateTime birth { get; set; } = DateTime.Now;
        // 地址
        [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string addr { get; set; }

        [SugarColumn(IsNullable = true)]
        public bool tdIsDelete { get; set; }


        [SugarColumn(IsIgnore = true)]
        public List<int> RIDs { get; set; }
        [SugarColumn(IsIgnore = true)]
        public List<string> RoleNames { get; set; }

    }
}

using System;
using System.Collections.Generic;

namespace Blog.Model.ViewModels
{
    public class TagViewModels
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int tId { get; set; }

        /// <summary>
        /// 标签名 Tag name
        /// </summary>
        public string tName { get; set; }

        /// <summary>
        /// DisplayName
        /// </summary>
        public string tDispalyName { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string tDescription { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string tsubmitter { get; set; }

        /// <summary>
        /// 创建人Id
        /// </summary>
        public string tsubmitterId { get; set; }

        /// <summary>
        /// icon
        /// </summary>
        public string tIcon { get; set; }

        /// <summary> 
        /// 修改时间
        /// </summary>
        public DateTime tModifyTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime tCreateTime { get; set; }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        public bool? IsDeleted { get; set; }
    }
}

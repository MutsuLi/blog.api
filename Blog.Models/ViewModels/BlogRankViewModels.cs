using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Model.ViewModels
{
    /// <summary>
    /// 博客信息展示类
    /// </summary>
    public class BlogRankViewModels
    {
        /// <summary>
        ///  blogid
        /// </summary>
        public int id { get; set; }
        
        /// <summary>创建人
        /// 
        /// </summary>
        public string bsubmitter { get; set; }

        /// <summary>创建人ID
        /// 
        /// </summary>
        public string bsubmitterId { get; set; }

        /// <summary>博客标题
        /// 
        /// </summary>
        public string btitle { get; set; }

        /// <summary>
        /// 访问量
        /// </summary>
        public int btraffic { get; set; }

        // /// <summary>
        // /// 评论数量
        // /// </summary>
        // public int bcommentNum { get; set; }
    }
}

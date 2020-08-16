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

        public BlogRankViewModels(int _id, string _submitterId, string _title, int _traffic)
        {

            id = _id;
            bsubmitterId = _submitterId;
            btitle = _title;
            btraffic = _traffic;
        }
        /// <summary>
        ///  blogid
        /// </summary>
        public int id { get; set; }

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

using System;
using SqlSugar;

namespace Blog.Api.Models
{
    public class Tag
    {
        /// <summary>
        /// 主键
        /// </summary>
        /// 这里之所以没用RootEntity，是想保持和之前的数据库一致，主键是bID，不是Id
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int tId { get; set; }

        /// <summary>
        /// 标签名 Tag name
        /// </summary>
        [SugarColumn(Length = 256, IsNullable = true)]
        public string tName { get; set; }

        /// <summary>
        /// DisplayName
        /// </summary>
        [SugarColumn(Length = 256, IsNullable = true)]
        public string tDispalyName { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDataType = "text")]
        public string tDescription { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [SugarColumn(Length = 60, IsNullable = true)]
        public string tsubmitter { get; set; }
        
        /// <summary>
        /// 创建人Id
        /// </summary>
        [SugarColumn(Length = 60, IsNullable = true)]
        public string tsubmitterId { get; set; }

        /// <summary>
        /// icon
        /// </summary>
        [SugarColumn(Length = int.MaxValue, IsNullable = true)]
        public string tIcon { get; set; }


        /// <summary> 
        /// 修改时间
        /// </summary>
        /// 
        [SugarColumn(SerializeDateTimeFormat =SugarDateTimeFormat.Date)]
        public DateTime tModifyTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(SerializeDateTimeFormat = SugarDateTimeFormat.Date)]
        public System.DateTime tCreateTime { get; set; }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public bool? IsDeleted { get; set; }
    }
}

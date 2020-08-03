using AutoMapper;
using Blog.Model.Models;
using Blog.Model.ViewModels;

namespace Blog.Api.AutoMapper
{
    public class CustomProfile : Profile
    {
        /// <summary>
        /// 配置构造函数，用来创建关系映射
        /// </summary>
        public CustomProfile()
        {
            CreateMap<BlogArticle, BlogViewModels>().ForMember(d=>d.id,o=>o.MapFrom(s=>s.bId));
            CreateMap<BlogViewModels, BlogArticle>();
        }
    }
}

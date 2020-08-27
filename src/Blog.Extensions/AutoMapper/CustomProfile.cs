using AutoMapper;
using Blog.Api.Models;
using Blog.Model.ViewModels;
using System;

namespace Blog.Api.AutoMapper
{
    public class DateTimeConverter : ITypeConverter<DateTime, string>
    {
        public string Convert(DateTime date, string dateString, ResolutionContext context)
        {
            return date.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
    public class CustomProfile : Profile
    {
        /// <summary>
        /// 配置构造函数，用来创建关系映射
        /// </summary>
        public CustomProfile()
        {
            CreateMap<DateTime, string>().ConvertUsing<DateTimeConverter>();
            CreateMap<BlogArticle, BlogViewModels>().ForMember(d => d.id, o => o.MapFrom(s => s.bId));
            CreateMap<BlogViewModels, BlogArticle>();
            CreateMap<Tag, TagViewModels>();
            CreateMap<TagViewModels, Tag>();
            CreateMap<BlogArticle, BlogRankViewModels>().ForMember(d => d.id, o => o.MapFrom(s => s.bId));
            CreateMap<BlogRankViewModels, BlogArticle>();
        }
    }
}

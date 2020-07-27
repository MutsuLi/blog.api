using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Common;
using Blog.IRepository;
using Blog.IServices;
using Blog.Model.Models;
using Blog.Model.ViewModels;
using Blog.Services.Base;

namespace Blog.Services
{
    public class BlogArticleServices : BaseServices<BlogArticle>, IBlogArticleServices
    {

        private IRedisCacheManager _redisCacheManager;
        IBlogArticleRepository _dal;
        private readonly IMapper _mapper;
        public BlogArticleServices(IBlogArticleRepository dal, IRedisCacheManager redisCacheManager, IMapper IMapper)
        {
            _redisCacheManager = redisCacheManager;
            this._dal = dal;
            base.BaseDal = dal;
            this._mapper = IMapper;
        }


        /// <summary>
        /// 获取博客列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ///   
        public async Task<List<BlogArticle>> getBlogs()
        {
            var connect = Appsettings.app(new string[] { "AppSettings", "RedisCaching", "ConnectionString" });//按照层级的顺序，依次写出来

            List<BlogArticle> blogArticleList = new List<BlogArticle>();

            if (_redisCacheManager.Get<object>("Redis.Blog") != null)
            {
                blogArticleList = _redisCacheManager.Get<List<BlogArticle>>("Redis.Blog");
            }
            else
            {
                blogArticleList =  await base.Query(a => a.bID > 0, a => a.bID);
                _redisCacheManager.Set("Redis.Blog", blogArticleList, TimeSpan.FromSeconds(30));//缓存2小时
            }

            return blogArticleList;
        }

        /// <summary>
        /// 获取视图博客详情信息(一般版本)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<BlogViewModels> getBlogDetails(int id)
        {
            var blogArticle = (await base.Query(a => a.bID == id && a.IsDeleted == false)).FirstOrDefault();
            BlogViewModels models = null;

            if (blogArticle == null) return models;

            models = _mapper.Map<BlogViewModels>(blogArticle);

            //要取下一篇和上一篇，以当前id开始，按id排序后top(2)，而不用取出所有记录
            //这样在记录很多的时候也不会有多大影响
            var nextBlogs = await base.Query(a => a.bID >= id && a.IsDeleted == false, 2, "bID");
            if (nextBlogs.Count == 2)
            {
                models.next = nextBlogs[1].btitle;
                models.nextID = nextBlogs[1].bID;
            }

            var prevBlogs = await base.Query(a => a.bID <= id && a.IsDeleted == false, 2, "bID desc");
            if (prevBlogs.Count == 2)
            {
                models.previous = prevBlogs[1].btitle;
                models.previousID = prevBlogs[1].bID;
            }

            blogArticle.btraffic += 1;
            await base.Update(blogArticle, new List<string> { "btraffic" });

            return models;
        }

    }

}

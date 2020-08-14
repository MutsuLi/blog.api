using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Common;
using Blog.Common.Helper;
using Blog.IRepository;
using Blog.IServices;
using Blog.Model.Models;
using Blog.Model.ViewModels;
using Blog.Models;
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
        /// 获取博客列表(Redis)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ///   
        public async Task<PageModel<BlogViewModels>> getBlogList(int page, int pageSize, Expression<Func<BlogArticle, bool>> where)
        {

            PageModel<BlogArticle> blogArticleList = new PageModel<BlogArticle>();

            if (_redisCacheManager.Get<object>("Redis.Blog") != null)
            {
                blogArticleList.data = _redisCacheManager.Get<List<BlogArticle>>("Redis.Blog.getBlogList");
            }
            else
            {
                blogArticleList = await base.QueryPage(where, page, pageSize, "bId desc");
                _redisCacheManager.Set("Redis.Blog.getBlogList", blogArticleList, TimeSpan.FromSeconds(10)); //缓存10sec
            }
            PageModel<BlogViewModels> models = new PageModel<BlogViewModels>();
            List<BlogViewModels> data = new List<BlogViewModels>();
            foreach (var each in blogArticleList.data)
            {
                data.Add(_mapper.Map<BlogViewModels>(each));
            }
            models.data = data;
            models.dataCount = blogArticleList.dataCount;
            return models;
        }

        /// <summary>
        /// 获取博客近期热文(Redis)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ///  
        public async Task<PageModel<BlogRankViewModels>> getBlogRank(int page, int pageSize, Expression<Func<BlogArticle, bool>> where)
        {
            PageModel<BlogArticle> blogArticleList = new PageModel<BlogArticle>();

            if (_redisCacheManager.Get<object>("Redis.Blog") != null)
            {
                blogArticleList.data = _redisCacheManager.Get<List<BlogArticle>>("Redis.Blog.getBlogList");
            }
            else
            {
                blogArticleList = await base.QueryPage(where, page, pageSize, "bId desc");
                _redisCacheManager.Set("Redis.Blog.getBlogList", blogArticleList, TimeSpan.FromSeconds(10)); //缓存10sec
            }
            PageModel<BlogRankViewModels> models = new PageModel<BlogRankViewModels>();
            List<BlogRankViewModels> data = new List<BlogRankViewModels>();
            foreach (var each in blogArticleList.data)
            {
                data.Add(_mapper.Map<BlogRankViewModels>(each));
            }
            models.data = data;
            models.dataCount = blogArticleList.dataCount;
            return models;
        }

        /// <summary>
        /// 获取视图博客详情信息(一般版本)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<BlogViewModels> getBlogDetails(int id)
        {
            var blogArticle = (await base.Query(a => a.bId == id && a.IsDeleted == false)).FirstOrDefault();
            BlogViewModels models = null;

            if (blogArticle == null) return models;

            models = _mapper.Map<BlogViewModels>(blogArticle);

            //要取下一篇和上一篇，以当前id开始，按id排序后top(2)，而不用取出所有记录
            //这样在记录很多的时候也不会有多大影响
            var nextBlogs = await base.Query(a => a.bId >= id && a.IsDeleted == false, 2, "bId");
            if (nextBlogs.Count == 2)
            {
                models.next = nextBlogs[1].btitle;
                models.nextID = nextBlogs[1].bId;
            }

            var prevBlogs = await base.Query(a => a.bId <= id && a.IsDeleted == false, 2, "bId desc");
            if (prevBlogs.Count == 2)
            {
                models.previous = prevBlogs[1].btitle;
                models.previousID = prevBlogs[1].bId;
            }

            blogArticle.btraffic += 1;
            await base.Update(blogArticle, new List<string> { "btraffic" });

            return models;
        }

    }

}

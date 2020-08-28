using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Common;
using Blog.IRepository;
using Blog.IServices;
using Blog.Api.Models;
using Blog.Model.ViewModels;
using Blog.Models;
using Blog.Services.Base;

namespace Blog.Services
{
    public class PostsService : Services<BlogArticle>, IPostsRespository
    {

        private IRedisCacheManager _redisCacheManager;
        IBlogArticleRepository _dal;
        private readonly IMapper _mapper;
        public PostsService(IBlogArticleRepository dal, IRedisCacheManager redisCacheManager, IMapper IMapper)
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
            models.pageCount = blogArticleList.pageCount;
            return models;
        }

    }

}

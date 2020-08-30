using System;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Api.Models;
using Blog.Common;
using Blog.IRepository;
using Blog.IServices;
using Blog.Model.ViewModels;
using Blog.Models;
using Blog.Services.Base;

namespace Blog.Services
{
    public class PostsService : Services<Posts>,IPostsService
    {

        private IRedisCacheManager _redisCacheManager;
        IPostsRespository _dal;
        private readonly IMapper _mapper;
        public PostsService(IPostsRespository dal, IRedisCacheManager redisCacheManager, IMapper IMapper)
        {
            _redisCacheManager = redisCacheManager;
            this._dal = dal;
            this._mapper = IMapper;
        }


        /// <summary>
        /// 获取博客列表(Redis)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ///   
        public async Task<PageModel<BlogViewModels>> getBlogList(int page, int pageSize)
        {
            PageModel<BlogViewModels> blogArticleList = new PageModel<BlogViewModels>();
            var result=await _dal.GetAllAsync();
            return blogArticleList;
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }

}

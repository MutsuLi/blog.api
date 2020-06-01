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
        private readonly IMapper _IMapper;
        public BlogArticleServices(IBlogArticleRepository dal, IRedisCacheManager redisCacheManager, IMapper IMapper)
        {
            _redisCacheManager = redisCacheManager;
            this._dal = dal;
            base.BaseDal = dal;
            this._IMapper = IMapper;
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
                blogArticleList = await base.Query(d => d.bID > 1);
                _redisCacheManager.Set("Redis.Blog", blogArticleList, TimeSpan.FromSeconds(30));//缓存2小时
            }

            return blogArticleList;
        }
        /// <summary>
        /// 获取博客列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ///   

        [Caching(AbsoluteExpiration = 10)]
        public async Task<List<BlogArticle>> getRedis()
        {
            var connect = Appsettings.app(new string[] { "AppSettings", "RedisCachingAOP", "ConnectionString" });//按照层级的
            return await Task.Run(() =>
            {
                return new List<BlogArticle>()
                 {
                    new BlogArticle(){
                    bID=1,
                    bsubmitter="test",
                    btitle="test2",
                    bcategory="1",
                    bcontent="2",
                    btraffic=10,
                    bcommentNum=1100,
                    bUpdateTime=DateTime.Now,
                    bCreateTime=DateTime.Now
                 }
                };
            });

        }

        /// <summary>
        /// 获取视图博客详情信息(一般版本)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<BlogViewModels> getBlogDetails(int id)
        {
            var bloglist = await _dal.Query(a => a.bID > 0, a => a.bID);
            var blogArticle = (await _dal.Query(a => a.bID == id)).FirstOrDefault();
            BlogViewModels models = null;

            if (blogArticle != null)
            {
                BlogArticle prevblog;
                BlogArticle nextblog;
                int blogIndex = bloglist.FindIndex(item => item.bID == id);
                if (blogIndex >= 0)
                {
                    try
                    {
                        // 上一篇
                        prevblog = blogIndex > 0 ? (((BlogArticle)(bloglist[blogIndex - 1]))) : null;
                        // 下一篇
                        nextblog = blogIndex + 1 < bloglist.Count() ? (BlogArticle)(bloglist[blogIndex + 1]) : null;


                        models = _IMapper.Map<BlogViewModels>(blogArticle);

                        if (nextblog != null)
                        {
                            models.next = nextblog.btitle;
                            models.nextID = nextblog.bID;
                        }
                        if (prevblog != null)
                        {
                            models.previous = prevblog.btitle;
                            models.previousID = prevblog.bID;
                        }
                    }
                    catch (Exception) { }
                }
                blogArticle.btraffic += 1;
                await _dal.Update(blogArticle, new List<string> { "btraffic" });
            }
            return models;

        }
    }

}

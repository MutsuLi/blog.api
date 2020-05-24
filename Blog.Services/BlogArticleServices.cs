using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Common;
using Blog.Common.Config;
using Blog.IRepository;
using Blog.IServices;
using Blog.Model.Models;
using Blog.Services.Base;

namespace Blog.Services
{
    public class BlogArticleServices : BaseServices<BlogArticle>, IBlogArticleServices
    {

        private IRedisCacheManager _redisCacheManager;
        IBlogArticleRepository _dal;
        public BlogArticleServices(IBlogArticleRepository dal,IRedisCacheManager redisCacheManager)
        {
            _redisCacheManager = redisCacheManager;
            this._dal = dal;
            base.BaseDal = dal;
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
            //return new List<BlogArticle>()
            // {
            //     new BlogArticle(){
            //         bID=1,
            //         bsubmitter="test",
            //         btitle="test2",
            //         bcategory="1",
            //         bcontent="2",
            //         btraffic=10,
            //         bcommentNum=1100,
            //         bUpdateTime=DateTime.Now,
            //         bCreateTime=DateTime.Now
            //     }
            // };
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
        }

    }
}

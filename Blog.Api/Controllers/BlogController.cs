using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.IServices;
using Blog.Model.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Blog.Api.Controllers
{
    /// <summary>
    ///  todoitem
    /// </summary>
    /// 
    [Produces("application/json")]
    [Route("api/Blog")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogArticleServices _IBlogArticleServices;
        private readonly ILogger<BlogController> _logger;

        public BlogController(IBlogArticleServices IBlogArticleServices, ILogger<BlogController> logger)
        {
            _IBlogArticleServices = IBlogArticleServices;
            _logger = logger;
        }

        // GET: api/Blog/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "Get")]
        public async Task<object> Get(int id)
        {
            var model = await _IBlogArticleServices.getBlogDetails(id);//调用该方法，这里 _blogArticleServices 是依赖注入的实例，不是类
            var data = new { success = true, data = model };
            return data;
        }


        /// <summary>
        /// 获取博客列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetBlogs")]
        public async Task<List<BlogArticle>> getBlogs()
        {

            return await _IBlogArticleServices.getBlogs();
        }


        /// <summary>
        /// 获取博客列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("redis")]
        public async Task<List<BlogArticle>> getRedis()
        {
            return await _IBlogArticleServices.getRedis();
        }



    }
}

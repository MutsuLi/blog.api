using System;
using System.Threading.Tasks;
using Blog.Model.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Blog.IServices;
using System.Collections.Generic;

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

        private readonly IAdvertisementServices _IAdvertisementServices;
        private readonly IBlogArticleServices _IBlogArticleServices;
        public BlogController(IAdvertisementServices IAdvertisementServices,IBlogArticleServices IBlogArticleServices)
        {
            _IAdvertisementServices = IAdvertisementServices;
            _IBlogArticleServices= IBlogArticleServices;
        }

        // GET: api/Blog/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "Get")]
        public List<Advertisement> Get(int id)
        {
            return _IAdvertisementServices.Query(d => d.Id == id);
        }


        /// <summary>
        /// 获取博客列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetBlogs")]
        public async Task<List<BlogArticle>> GetBlogs()
        {

            return await _IBlogArticleServices.getBlogs();
        }

    }
}

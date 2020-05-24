using System.Collections.Generic;
using Blog.IServices;
using Blog.Model.Models;
using Microsoft.AspNetCore.Mvc;

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
        public List<BlogArticle> getBlogs()
        {

           return  _IBlogArticleServices.getBlogs();
        }

    }
}

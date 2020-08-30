using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Blog.Api.Models;
using Blog.IServices;
using Blog.Model;
using Blog.Model.ViewModels;
using Blog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Blog.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostsService _postsService;
        private readonly ILogger<PostsController> _logger;

        public PostsController(IPostsService IPostsService, ILogger<PostsController> logger)
        {
            _postsService = IPostsService;
            _logger = logger;
        }

        /// <summary>
        /// 获取博客列表【无权限】
        /// </summary>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="bcategory"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        // [Route("list")]
        public async Task<MessageModel<PageModel<BlogViewModels>>> Get(int id, int page = 1, int pageSize = 25, string bcategory = "", string key = "")
        {
            var pageModelBlog= await _postsService.getBlogList(id,page);
            return new MessageModel<PageModel<BlogViewModels>>()
            {
                success = true,
                msg = "success",
                response = new PageModel<BlogViewModels>()
                {
                    page = page,
                    PageSize = pageSize,
                    dataCount = pageModelBlog.dataCount,
                    data = pageModelBlog.data,
                    pageCount = pageModelBlog.pageCount,
                }
            };
        }
    }
}

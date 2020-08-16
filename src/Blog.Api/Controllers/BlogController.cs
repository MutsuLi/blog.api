using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Blog.Api.Common.Helper;
using Blog.IServices;
using Blog.Model;
using Blog.Model.Models;
using Blog.Model.ViewModels;
using Blog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StackExchange.Profiling;

namespace Blog.Api.Controllers
{
    /// <summary>
    ///  todoitem
    /// </summary>
    /// 
    [Produces("application/json")]
    [Route("api/blogs")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogArticleServices _blogArticleServices;
        private readonly ILogger<BlogController> _logger;

        public BlogController(IBlogArticleServices IBlogArticleServices, ILogger<BlogController> logger)
        {
            _blogArticleServices = IBlogArticleServices;
            _logger = logger;
        }

        /// <summary>
        /// 获取博客详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<object> Get(int id)
        {
            return new MessageModel<BlogViewModels>()
            {
                msg = "success",
                success = true,
                response = await _blogArticleServices.getBlogDetails(id)
            };
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

            Expression<Func<BlogArticle, bool>> where = PredicateBuilder.True<BlogArticle>();
            if (id > 0)
            {
                where.And(a => (a.bId == id && a.IsDeleted == false));
            }
            if (!string.IsNullOrEmpty(bcategory))
            {
                where.And(a => (a.bcategory == bcategory && a.IsDeleted == false));
            }
            if (!string.IsNullOrEmpty(key))
            {
                where.And(a => (a.btitle != null && a.btitle.Contains(key)) || (a.bcontent != null && a.bcontent.Contains(key)));
            }

            var pageModelBlog = await _blogArticleServices.getBlogList(page, pageSize, where);

            using (MiniProfiler.Current.Step("Receive successfully and start to processing data"))
            {
                foreach (var each in pageModelBlog.data)
                {
                    if (!string.IsNullOrEmpty(each.bcontent))
                    {
                        each.bRemark = (HtmlHelper.ReplaceHtmlTag(each.bcontent)).Length >= 200 ? (HtmlHelper.ReplaceHtmlTag(each.bcontent)).Substring(0, 200) : (HtmlHelper.ReplaceHtmlTag(each.bcontent));
                        int totalLength = 500;
                        if (each.bcontent.Length > totalLength)
                        {
                            each.bcontent = each.bcontent.Substring(0, totalLength);
                        }
                    }
                }
            }

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

        /// <summary>
        /// 获取博客近期热文(Redis)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="bcategory"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        ///  
        [HttpGet]
        [AllowAnonymous]
        [Route("rank")]
        public async Task<MessageModel<PageModel<BlogRankViewModels>>> getBlogRank(int id, int page = 1, int pageSize = 25, string bcategory = "", string key = "")
        {
            Expression<Func<BlogArticle, bool>> where = PredicateBuilder.True<BlogArticle>();
            if (id > 0)
            {
                where.And(a => (a.bId == id && a.IsDeleted == false));
            }
            if (!string.IsNullOrEmpty(bcategory))
            {
                where.And(a => (a.bcategory == bcategory && a.IsDeleted == false));
            }
            if (!string.IsNullOrEmpty(key))
            {
                where.And(a => (a.btitle != null && a.btitle.Contains(key)) || (a.bcontent != null && a.bcontent.Contains(key)));
            }

            var pageModelBlog = await _blogArticleServices.getBlogRank(page, pageSize, where);

            return new MessageModel<PageModel<BlogRankViewModels>>()
            {
                success = true,
                msg = "success",
                response = new PageModel<BlogRankViewModels>()
                {
                    page = page,
                    PageSize = pageSize,
                    dataCount = pageModelBlog.dataCount,
                    data = pageModelBlog.data,
                    pageCount = pageModelBlog.pageCount,
                }
            };
        }

        /// <summary>
        /// 添加博客
        /// </summary>
        /// <param name="blogArticle"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Permissions.Name)]
        [Route("post")]
        public async Task<MessageModel<string>> Post([FromBody] BlogArticle blogArticle)
        {
            var data = new MessageModel<string>();
            blogArticle.bCreateTime = DateTime.Now;
            blogArticle.bUpdateTime = DateTime.Now;
            blogArticle.IsDeleted = false;

            var id = await _blogArticleServices.createBlogArticle(blogArticle);
            data.success = !string.IsNullOrEmpty(id);
            if (data.success)
            {
                data.response = id;
                data.msg = "New article has been added.";
            }

            return data;

        }

        /// <summary>
        /// 删除博客
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Permissions.Name)]
        [Route("delete")]
        public async Task<MessageModel<string>> Delete(int id)
        {
            var data = new MessageModel<string>();
            if (id <= 0) return data;
            var blogArticle = await _blogArticleServices.QueryById(id);
            blogArticle.IsDeleted = true;
            data.success = await _blogArticleServices.Update(blogArticle);
            if (data.success)
            {
                data.msg = "Delete article successfully.";
                data.response = blogArticle?.bId.ObjToString();
            }

            return data;

        }
        /// <summary>
        /// 更新博客信息
        /// </summary>
        /// <param name="BlogArticle"></param>
        /// <returns></returns>
        // PUT: api/User/5
        [HttpPut]
        [Route("update")]
        public async Task<MessageModel<string>> Put([FromBody] BlogArticle BlogArticle)
        {
            var data = new MessageModel<string>();
            if (BlogArticle == null || BlogArticle.bId <= 0)
            {
                return data;
            }
            var model = await _blogArticleServices.QueryById(BlogArticle.bId);
            if (model == null) return data;

            model.btitle = BlogArticle.btitle;
            model.bcategory = BlogArticle.bcategory;
            model.bsubmitter = BlogArticle.bsubmitter;
            model.bcontent = BlogArticle.bcontent;

            data.success = await _blogArticleServices.Update(model);

            if (data.success)
            {
                data.msg = "Upadte article successfully.";
                data.response = BlogArticle?.bId.ObjToString();
            }
            return data;
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Blog.Common;
using Blog.Common.Helper;
using Blog.IServices;
using Blog.Model;
using Blog.Model.Models;
using Blog.Model.ViewModels;
using Blog.Models;
using Castle.Core.Internal;
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
    [Route("api/Blog")]
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
                msg = "获取成功",
                success = true,
                response = await _blogArticleServices.getBlogDetails(id)
            };
        }

        /// <summary>
        /// 获取博客列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("list")]
        public async Task<MessageModel<PageModel<BlogArticle>>> Get(int id, int page = 1, int pageSize = 25, string bcategory = "技术博文", string key = "")
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrWhiteSpace(key))
            {
                key = "";
            }

            Expression<Func<BlogArticle, bool>> whereExpression = a => (a.bcategory == bcategory && a.IsDeleted == false) && ((a.btitle != null && a.btitle.Contains(key)) || (a.bcontent != null && a.bcontent.Contains(key)));

            var pageModelBlog = await _blogArticleServices.QueryPage(whereExpression, page, pageSize, "bID desc");

            using (MiniProfiler.Current.Step("获取成功后，开始处理最终数据"))
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

            return new MessageModel<PageModel<BlogArticle>>()
            {
                success = true,
                msg = "获取成功",
                response = new PageModel<BlogArticle>()
                {
                    page = page,
                    dataCount = pageModelBlog.dataCount,
                    data = pageModelBlog.data,
                    pageCount = pageModelBlog.pageCount,
                }
            };
        }


        /// <summary>
        /// 获取博客列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("redis")]
        public async Task<List<BlogArticle>> getRedis()
        {
            return await _blogArticleServices.getRedis();
        }

        /// <summary>
        /// 添加博客【无权限】
        /// </summary>
        /// <param name="blogArticle"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<MessageModel<string>> Post([FromBody] BlogArticle blogArticle)
        {
            var data = new MessageModel<string>();
            blogArticle.bCreateTime = DateTime.Now;
            blogArticle.bUpdateTime = DateTime.Now;
            blogArticle.IsDeleted = false;

            var id = await _blogArticleServices.Add(blogArticle);
            data.success = id > 0;
            if (data.success)
            {
                data.response = id.ObjToString();
                data.msg = "添加成功";
            }

            return data;

        }

        /// <summary>
        /// 删除博客
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        //[Authorize(Permissions.Name)]
        [Route("Delete")]
        public async Task<MessageModel<string>> Delete(int id)
        {
            var data = new MessageModel<string>();
            if (id <= 0) return data;
            var blogArticle = await _blogArticleServices.QueryById(id);
            blogArticle.IsDeleted = true;
            data.success = await _blogArticleServices.Update(blogArticle);
            if (data.success)
            {
                data.msg = "删除成功";
                data.response = blogArticle?.bID.ObjToString();
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
        [Route("Update")]
        public async Task<MessageModel<string>> Put([FromBody] BlogArticle BlogArticle)
        {
            var data = new MessageModel<string>();
            if (BlogArticle == null || BlogArticle.bID <= 0)
            {
                return data;
            }
            var model = await _blogArticleServices.QueryById(BlogArticle.bID);
            if (model == null) return data;

            model.btitle = BlogArticle.btitle;
            model.bcategory = BlogArticle.bcategory;
            model.bsubmitter = BlogArticle.bsubmitter;
            model.bcontent = BlogArticle.bcontent;

            data.success = await _blogArticleServices.Update(model);

            if (data.success)
            {
                data.msg = "更新成功";
                data.response = BlogArticle?.bID.ObjToString();
            }
            return data;
        }


    }
}

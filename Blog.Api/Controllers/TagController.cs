using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Blog.Common.Helper;
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
    [Route("api/tag")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagServices _tagServices;
        private readonly ILogger<TagController> _logger;

        public TagController(ITagServices ITagServices, ILogger<TagController> logger)
        {
            _tagServices = ITagServices;
            _logger = logger;
        }

        /// <summary>
        /// 获取博客列表【无权限】
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="tagId"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        // [Route("list")]
        public async Task<MessageModel<PageModel<Tag>>> Get(int page, int pageSize, int tagId = -1, string key = "")
        {
            Expression<Func<Tag, bool>> where = PredicateBuilder.True<Tag>();
            if (tagId > 0)
            {
                where.And(a => (a.tId == tagId && a.IsDeleted == false));
            }
            if (string.IsNullOrEmpty(key))
            {
                where.And(a => (a.tName != null && a.tName.Contains(key)) || (a.tDescription != null && a.tDescription.Contains(key)));
            }

            var tagPageModel = await _tagServices.QueryPage(where, page, pageSize, "tId desc");

            return new MessageModel<PageModel<Tag>>()
            {
                success = true,
                msg = "success",
                response = new PageModel<Tag>()
                {
                    page = page,
                    PageSize = pageSize,
                    dataCount = tagPageModel.dataCount,
                    data = tagPageModel.data,
                    pageCount = tagPageModel.pageCount,
                }
            };
        }
    }
}

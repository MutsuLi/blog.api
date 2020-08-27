using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Blog.Api.Common.Helper;
using Blog.IServices;
using Blog.Model;
using Blog.Api.Models;
using Blog.Model.ViewModels;
using Blog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
        /// 获取标签列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="tagId"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        // [Route("list")]
        public async Task<MessageModel<PageModel<TagViewModels>>> queryTagList(int page = 1, int pageSize = 25, int tagId = -1, string key = "")
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

            var tagPageModel = await _tagServices.getTagList(page, pageSize, where);

            return new MessageModel<PageModel<TagViewModels>>()
            {
                success = true,
                msg = "success",
                response = new PageModel<TagViewModels>()
                {
                    page = page,
                    PageSize = pageSize,
                    dataCount = tagPageModel.dataCount,
                    data = tagPageModel.data,
                    pageCount = tagPageModel.pageCount,
                }
            };
        }

        /// <summary>
        /// 添加标签【无权限】
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Permissions.Name)]
        [Route("post")]
        public async Task<MessageModel<string>> Post([FromBody] Tag tag)
        {
            var tagInfo = (await _tagServices.Query(a => a.tName == tag.tName)).FirstOrDefault();

            int id;
            var data = new MessageModel<string>();
            if (tagInfo != null)
            {
                tagInfo.tCreateTime = DateTime.Now;
                tagInfo.tModifyTime = DateTime.Now;
                tagInfo.tDescription = tag.tDescription;
                tagInfo.tDispalyName = tag.tDispalyName;
                var success = await _tagServices.Update(tagInfo);
                if (!success) return data;
                id = tagInfo.tId;
            }
            else
            {
                tag.tCreateTime = DateTime.Now;
                tag.tModifyTime = DateTime.Now;
                tag.IsDeleted = false;
                id = await _tagServices.Add(tag);
            }
            data.success = id > 0;
            if (data.success)
            {
                data.response = id.ObjToString();
                data.msg = "New article has been added.";
            }

            return data;

        }

        /// <summary>
        /// 删除标签
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
            var tag = await _tagServices.QueryById(id);
            tag.IsDeleted = true;
            data.success = await _tagServices.Update(tag);
            if (data.success)
            {
                data.msg = "Delete article successfully.";
                data.response = tag?.tId.ObjToString();
            }

            return data;

        }
        /// <summary>
        /// 更新标签信息
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        // PUT: api/tag/5
        [HttpPut]
        [Route("update")]
        public async Task<MessageModel<string>> Put([FromBody] Tag tag)
        {
            var data = new MessageModel<string>();
            if (tag == null || tag.tId <= 0)
            {
                return data;
            }
            var model = await _tagServices.QueryById(tag.tId);
            if (model == null) return data;

            model.tName = tag.tName;
            model.tDispalyName = tag.tDispalyName;
            model.tsubmitter = tag.tsubmitter;
            model.tDescription = tag.tDescription;

            data.success = await _tagServices.Update(model);

            if (data.success)
            {
                data.msg = "Upadte tag info successfully.";
                data.response = tag?.tId.ObjToString();
            }
            return data;
        }
    }
}

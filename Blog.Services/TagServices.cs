using Blog.IServices;
using Blog.Model.Models;
using Blog.Services.Base;
using Blog.Common;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using Blog.IRepository;
using Blog.Model;
using Blog.Models;
using Blog.Model.ViewModels;
namespace Blog.Services
{
    public class TagServices : BaseServices<Tag>, ITagServices
    {

        private IRedisCacheManager _redisCacheManager;
        ITagRepository _dal;
        private readonly IMapper _mapper;
        public TagServices(ITagRepository dal, IRedisCacheManager redisCacheManager, IMapper IMapper)
        {
            _redisCacheManager = redisCacheManager;
            this._dal = dal;
            base.BaseDal = dal;
            this._mapper = IMapper;
        }

        /// <summary>
        /// 获取标签列表(Redis)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ///   
        public async Task<PageModel<TagViewModels>> getTagList(int page, int pageSize, Expression<Func<Tag, bool>> where)
        {
            PageModel<Tag> tagList = new PageModel<Tag>();

            if (_redisCacheManager.Get<object>("Redis.Blog") != null)
            {
                tagList.data = _redisCacheManager.Get<List<Tag>>("Redis.Tag.getTagList");
            }
            else
            {
                tagList = await base.QueryPage(where, page, pageSize, "tModifyTime desc");
                _redisCacheManager.Set("Redis.Tag.getTagList", tagList, TimeSpan.FromSeconds(10)); //缓存10sec
            }
            PageModel<TagViewModels> models = new PageModel<TagViewModels>();
            List<TagViewModels> data = new List<TagViewModels>();
            foreach (var each in tagList.data)
            {
                data.Add(_mapper.Map<TagViewModels>(each));
            }
            models.data = data;
            return models;
        }
    }
}

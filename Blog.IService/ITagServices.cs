using Blog.IServices.Base;
using Blog.Model.Models;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Blog.Model;
using Blog.Model.ViewModels;
using Blog.Models;
using System;

namespace Blog.IServices
{
    /// <summary>
    /// ITasksQzServices
    /// </summary>	
    public interface ITagServices : IBaseServices<Tag>
    {
        Task<PageModel<TagViewModels>> getTagList(int page, int pageSize, Expression<Func<Tag, bool>> where);
    }
}

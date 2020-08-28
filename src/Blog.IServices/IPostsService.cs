using Blog.IServices.Base;
using Blog.Api.Models;
using Blog.Model.ViewModels;
using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.IServices
{
    public interface IPostsService : IBaseServices<BlogArticle>
    {
        Task<PageModel<BlogViewModels>> getBlogList(int page, int pageSize, Expression<Func<BlogArticle, bool>> where);
    }
}

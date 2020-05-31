using Blog.IServices.Base;
using Blog.Model.Models;
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
    public interface IBlogArticleServices : IBaseServices<BlogArticle>
    {
        Task<List<BlogArticle>> getRedis();
        Task<List<BlogArticle>> getBlogs();

        Task<BlogViewModels> getBlogDetails(int id);
    }
}

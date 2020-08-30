using System.Threading.Tasks;
using Blog.Api.Models;
using Blog.IServices.Base;
using Blog.Model.ViewModels;
using Blog.Models;

namespace Blog.IServices
{
    public interface IPostsService : IServices<Posts>
    {
        Task<PageModel<BlogViewModels>> getBlogList(int page, int pageSize);
    }
}

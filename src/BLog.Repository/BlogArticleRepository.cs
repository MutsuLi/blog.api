using Blog.IRepository;
using Blog.IRepository.IUnitOfWork;
using Blog.Api.Models;
using Blog.Repository.Base;

namespace Blog.Repository
{
    public class BlogArticleRepository : BaseRepository<BlogArticle>, IBlogArticleRepository
    {
        public BlogArticleRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

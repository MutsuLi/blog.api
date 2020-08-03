using Blog.IRepository;
using Blog.IRepository.IUnitOfWork;
using Blog.Model.Models;
using Blog.Repository.Base;

namespace Blog.Repository
{
    public class TagRespository : BaseRepository<Tag>, ITagRepository
    {
        public TagRespository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

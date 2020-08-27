using Blog.Core.IRepository;
using Blog.IRepository.IUnitOfWork;
using Blog.Api.Models;
using Blog.Repository.Base;

namespace Blog.Repository
{
    public class TopicDetailRepository : BaseRepository<TopicDetail>, ITopicDetailRepository
    {
        public TopicDetailRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

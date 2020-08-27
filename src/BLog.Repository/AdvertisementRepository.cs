using Blog.IRepository;
using Blog.IRepository.IUnitOfWork;
using Blog.Api.Models;
using Blog.Repository.Base;

namespace Blog.Repository
{
    public class AdvertisementRepository : BaseRepository<Advertisement>, IAdvertisementRepository
    {
        public AdvertisementRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

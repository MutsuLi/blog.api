using Blog.Core.IRepository;
using Blog.IRepository;
using Blog.IRepository.IUnitOfWork;
using Blog.Model.Models;
using Blog.Repository.Base;

namespace Blog.Repository
{
    public class GuestbookRepository : BaseRepository<Guestbook>, IGuestbookRepository
    {
        public GuestbookRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }



    }
}

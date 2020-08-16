using Blog.FrameWork.IRepository;
using Blog.IRepository.IUnitOfWork;
using Blog.Model.Models;
using Blog.Repository.Base;

namespace Blog.Repository
{
    /// <summary>
    /// UserRoleRepository
    /// </summary>	
    public class UserRoleRepository : BaseRepository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

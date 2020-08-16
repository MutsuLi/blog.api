using Blog.IRepository;
using Blog.IRepository.IUnitOfWork;
using Blog.Model.Models;
using Blog.Repository.Base;

namespace Blog.Repository
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

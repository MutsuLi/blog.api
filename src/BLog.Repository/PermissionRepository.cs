using Blog.IRepository;
using Blog.IRepository.IUnitOfWork;
using Blog.Api.Models;
using Blog.Repository.Base;

namespace Blog.Repository
{
    public class PermissionRepository : BaseRepository<Permission>, IPermissionRepository
    {
        public PermissionRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}


using Blog.Core.IRepository;
using Blog.IRepository;
using Blog.IRepository.IUnitOfWork;
using Blog.Api.Models;
using Blog.Repository.Base;

namespace Blog.Repository
{
	/// <summary>
	/// TasksQzRepository
	/// </summary>
    public class TasksQzRepository : BaseRepository<TasksQz>, ITasksQzRepository
    {
        public TasksQzRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
                    
using SqlSugar;
using Blog.Api.Model.EntityFrameworkCore;
namespace Blog.IRepository.IUnitOfWork
{
    public interface IUnitOfWorktest
    {
        // EfcoreDbContext GetDbClient();

        void BeginTran();

        void CommitTran();
        void RollbackTran();
        bool Commit();
        void Dispose();
    }
}

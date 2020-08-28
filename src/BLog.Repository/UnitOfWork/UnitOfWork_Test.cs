using System;
using Blog.IRepository.IUnitOfWork;
// using SqlSugar;
using Blog.Api.Model.EntityFrameworkCore;

namespace Blog.Repository.UnitOfWork
{
    public class UnitOfWorktest : IUnitOfWorktest
    {
        //数据库上下文
        private readonly EfcoreDbContext _context;

        //构造函数注入
        public UnitOfWorktest(EfcoreDbContext context)
        {
            _context = context;
        }

        //上下文提交
        public bool Commit()
        {
            return _context.SaveChanges() > 0;
        }

        //手动回收
        public void Dispose()
        {
            _context.Dispose();
        }
        public void BeginTran()
        {
        }
        public void CommitTran()
        {
        }
        public void RollbackTran()
        {
        }
    }

}

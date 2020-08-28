using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Blog.Api.Model.EntityFrameworkCore;
using Blog.IRepository.Base;
using Blog.IRepository.IUnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Blog.Repository.Base
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        private readonly IUnitOfWorktest _unitOfWork;
        protected readonly DbSet<TEntity> DbSet;
        private EfcoreDbContext _db;
        public Repository(EfcoreDbContext context)
        {
            _db = context;
            DbSet = _db.Set<TEntity>();
        }

        public virtual void Add(TEntity obj)
        {
            DbSet.Add(obj);
        }

        public virtual TEntity GetById(Guid id)
        {
            return DbSet.Find(id);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return DbSet;
        }

        public virtual void Update(TEntity obj)
        {
            DbSet.Update(obj);
        }

        public virtual void Remove(Guid id)
        {
            DbSet.Remove(DbSet.Find(id));
        }

        public int SaveChanges()
        {
            return _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
            GC.SuppressFinalize(this);
        }
    }

}

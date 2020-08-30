using System;
using System.Linq;
using Blog.IRepository.Base;
using Blog.IServices.Base;

namespace Blog.Services.Base
{
    public class Services<TEntity> : IServices<TEntity> where TEntity : class, new()
    {
        //public IBaseRepository<TEntity> baseDal = new BaseRepository<TEntity>();
        public IRepository<TEntity> BaseDal;//通过在子类的构造函数中注入，这里是基类，不用构造函数

        public void Add(TEntity obj)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> GetAll()
        {
            return BaseDal.GetAll();
        }

        public TEntity GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public TEntity QueryById(Guid objId)
        {
            return BaseDal.GetById(objId);
        }

        public void Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity obj)
        {
            throw new NotImplementedException();
        }
    }
}
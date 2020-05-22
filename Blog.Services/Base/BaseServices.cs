using System;
using System.Threading.Tasks;
using Blog.IRepository.Base;
using Blog.IServices.Base;
using System.Linq.Expressions;

namespace Blog.Services.Base
{
    public class BaseServices<TEntity> : IBaseServices<TEntity> where TEntity : class, new()
    {

        //public IBaseRepository<TEntity> baseDal = new BaseRepository<TEntity>();
        public IBaseRepository<TEntity> BaseDal;//通过在子类的构造函数中注入，这里是基类，不用构造函数

        Task<bool> IBaseServices<TEntity>.Delete(TEntity model)
        {
            throw new NotImplementedException();
        }




        Task<TEntity> IBaseServices<TEntity>.QueryById(object objId)
        {
            throw new NotImplementedException();
        }

        Task<TEntity> IBaseServices<TEntity>.QueryById(object objId, bool blnUseCache)
        {
            throw new NotImplementedException();
        }

        Task<bool> IBaseServices<TEntity>.Update(TEntity model)
        {
            throw new NotImplementedException();
        }
    }
}
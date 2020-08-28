using System;
using System.Threading.Tasks;
using Blog.IRepository.Base;
using Blog.IServices.Base;
using System.Linq.Expressions;
using System.Collections.Generic;
using Blog.Api.Models;
using System.Data;
using SqlSugar;
using Blog.Model.ViewModels;

namespace Blog.Services.Base
{
    public class Services<TEntity> : IServices<TEntity> where TEntity : class, new()
    {
        //public IBaseRepository<TEntity> baseDal = new BaseRepository<TEntity>();
        public IRepository<TEntity> BaseDal;//通过在子类的构造函数中注入，这里是基类，不用构造函数

        public TEntity QueryById(Guid objId)
        {
            return BaseDal.GetById(objId);
        }
    }
}
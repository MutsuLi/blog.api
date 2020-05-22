using Blog.IRepository;
using Blog.Model.Models;
using Blog.Repository.Base;
using Blog.IRepository.IUnitOfWork;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using SqlSugar;

namespace Blog.Repository
{
   public class BlogArticleRepository : BaseRepository<BlogArticle>, IBlogArticleRepository
    {

        private DbContext context;
        private SqlSugarClient db;
        private SimpleClient<BlogArticle> entityDB;
        internal SqlSugarClient Db
        {
            get { return db; }
            private set { db = value; }
        }
        public DbContext Context
        {
            get { return context; }
            set { context = value; }
        }
        public BlogArticleRepository()
        {
            DbContext.Init(BaseDBConfig.ConnectionString, DbType.MySql);
            context = DbContext.GetDbContext();
            db = context.Db;
            entityDB = context.GetEntityDB<BlogArticle>(db);
        }
        //public AdvertisementRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        //{


        //}

        public int Add(BlogArticle model)
        {
            var i = db.Insertable(model).ExecuteReturnBigIdentity();
            return i.ObjToInt();
        }

        public bool Delete(BlogArticle model)
        {
            var i = db.Deleteable(model).ExecuteCommand();
            return i > 0;
        }

        public List<BlogArticle> Query(Expression<Func<BlogArticle, bool>> whereExpression)
        {
            return entityDB.GetList(whereExpression);
        }

        public int Sum(int i, int j)
        {
            return i + j;
        }

        public bool Update(BlogArticle model)
        {
            var i = db.Updateable(model).ExecuteCommand();
            return i > 0;
        }
    }
}

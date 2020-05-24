using Blog.IServices;
using Blog.Model.Models;
using Blog.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Blog.IRepository;
using System.Threading.Tasks;
using Blog.Common;

namespace Blog.Services
{
    public class BlogArticleServices : BaseServices<BlogArticle>, IBlogArticleServices
    {
        public IBlogArticleRepository _dal;
        public BlogArticleServices(IBlogArticleRepository dal)
        {
            _dal = dal;
        }
        public int Add(BlogArticle model)
        {
            return _dal.Add(model);
        }

        public bool Delete(BlogArticle model)
        {
            return _dal.Delete(model);
        }

        public List<BlogArticle> Query(Expression<Func<BlogArticle, bool>> whereExpression)
        {
            return _dal.Query(whereExpression);
        }

        public int Sum(int i, int j)
        {
            return _dal.Sum(i, j);
        }

        public bool Update(BlogArticle model)
        {
            return _dal.Update(model);
        }

        /// <summary>
        /// 获取博客列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ///   

       [Caching(AbsoluteExpiration=10)]
        public List<BlogArticle> getBlogs()
        {
           return new List<BlogArticle>()
            {
                new BlogArticle(){
                    bID=1,
                    bsubmitter="test",
                    btitle="test2",
                    bcategory="1",
                    bcontent="2",
                    btraffic=10,
                    bcommentNum=1100,
                    bUpdateTime=DateTime.Now,
                    bCreateTime=DateTime.Now,


                }
            };
        }
    }
}

using Blog.IRepository.Base;
using Blog.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.IRepository
{
    public interface IBlogArticleRepository: IBaseRepository<BlogArticle>
    {
        int Sum(int i, int j);

        int Add(BlogArticle model);
        bool Delete(BlogArticle model);
        bool Update(BlogArticle model);
        List<BlogArticle> Query(Expression<Func<BlogArticle, bool>> whereExpression);
    }
}

using Blog.IRepository.Base;
using Blog.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.IRepository
{
    public interface IPostsRespository: IRepository<Posts>
    {

    }
}
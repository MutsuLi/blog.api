using Blog.IServices;
using Blog.Model.Models;
using Blog.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Blog.Services
{
    public class AdvertisementServices : BaseServices<Advertisement>, IAdvertisementServices
    {

        public int Add(Advertisement model)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Advertisement model)
        {
            throw new NotImplementedException();
        }

        public List<Advertisement> Query(Expression<Func<Advertisement, bool>> whereExpression)
        {
            throw new NotImplementedException();
        }

        public void ReturnExp()
        {
            throw new NotImplementedException();
        }

        public int Sum(int i, int j)
        {
            throw new NotImplementedException();
        }

        public bool Update(Advertisement model)
        {
            throw new NotImplementedException();
        }
    }
}

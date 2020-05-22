using Blog.IServices;
using Blog.Model.Models;
using Blog.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Blog.IRepository;

namespace Blog.Services
{
    public class AdvertisementServices : BaseServices<Advertisement>, IAdvertisementServices
    {

        public IAdvertisementRepository _dal;
        public AdvertisementServices(IAdvertisementRepository dal)
        {
           _dal = dal;
        }
        public int Add(Advertisement model)
        {
            return _dal.Add(model);
        }

        public bool Delete(Advertisement model)
        {
            return _dal.Delete(model);
        }

        public List<Advertisement> Query(Expression<Func<Advertisement, bool>> whereExpression)
        {
            return _dal.Query(whereExpression);
        }


        public int Sum(int i, int j)
        {
            return _dal.Sum(i, j);
        }

        public bool Update(Advertisement model)
        {
            return _dal.Update(model);
        }
    }
}

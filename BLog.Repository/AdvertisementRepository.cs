using Blog.IRepository;
using Blog.Model.Models;
using Blog.Repository.Base;
using Blog.IRepository.IUnitOfWork;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace Blog.Repository
{
    public class AdvertisementRepository : BaseRepository<Advertisement>, IAdvertisementRepository
    {
        public AdvertisementRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {


        }

        public int Add(Advertisement model)
        {
            throw new NotImplementedException();
        }

        public List<Advertisement> Query(Expression<Func<Advertisement, bool>> whereExpression)
        {
            throw new NotImplementedException();
        }

        public int Sum(int i, int j)
        {
            throw new NotImplementedException();
        }

        bool IAdvertisementRepository.Delete(Advertisement model)
        {
            throw new NotImplementedException();
        }

        bool IAdvertisementRepository.Update(Advertisement model)
        {
            throw new NotImplementedException();
        }
    }
}

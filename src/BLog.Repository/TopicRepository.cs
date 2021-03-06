﻿using Blog.Core.IRepository;
using Blog.IRepository.IUnitOfWork;
using Blog.Model.Models;
using Blog.Repository.Base;

namespace Blog.Repository
{
    public class TopicRepository : BaseRepository<Topic>, ITopicRepository
    {
        public TopicRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

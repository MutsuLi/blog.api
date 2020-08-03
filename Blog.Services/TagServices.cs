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
    public class TagServices : BaseServices<Tag>, ITagServices
    {
        ITagRepository _dal;
        public TagServices(ITagRepository dal)
        {
            this._dal = dal;
            base.BaseDal = dal;
        }
    }
}

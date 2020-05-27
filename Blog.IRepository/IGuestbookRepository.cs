using Blog.IRepository.Base;
using Blog.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.IRepository
{
    public interface IGuestbookRepository : IBaseRepository<Guestbook>
    {
    }
}

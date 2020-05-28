using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.IServices.Base;
using Blog.Model.Models;

namespace Blog.IServices
{
    public interface ITopicDetailServices : IBaseServices<TopicDetail>
    {
        Task<List<TopicDetail>> GetTopicDetails();
    }
}

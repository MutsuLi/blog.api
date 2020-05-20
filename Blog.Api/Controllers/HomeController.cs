using Blog.Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    /// <summary>
    /// 首页
    /// </summary>
    [Route("api/Home")]
    [ApiController]

    public class HomeController : ControllerBase
    {
        private readonly TodoContext _context;
        public HomeController(TodoContext context)
        {
            _context = context;
        }
        
        /// <summary>
        /// test
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetResult()
        {
            return "test";
        }
    }
}

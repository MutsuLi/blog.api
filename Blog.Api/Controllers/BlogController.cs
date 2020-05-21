using System;
using System.Threading.Tasks;
using Blog.MiddleWare;
using Blog.Model.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Blog.IServices;
using System.Collections.Generic;
using Blog.Services;

namespace Blog.Api.Controllers
{
    /// <summary>
    ///  todoitem
    /// </summary>
    /// 
    [Produces("application/json")]
    [Route("api/Blog")]
    [ApiController]
    public class BlogController : ControllerBase
    {

        private readonly IAdvertisementServices _IAdvertisementServices;
        public BlogController(IAdvertisementServices IAdvertisementServices)
        {
            _IAdvertisementServices = IAdvertisementServices;
        }

        // GET: api/Blog/5
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "Get")]
        public List<Advertisement> Get(int id)
        {
            return _IAdvertisementServices.Query(d => d.Id == id);
        }

    }
}

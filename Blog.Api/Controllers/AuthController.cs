using System.Threading.Tasks;
using Blog.MiddleWare;
using Blog.Model.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Blog.Api.Controllers
{
    /// <summary>
    ///  todoitem
    /// </summary>
    [Route("api/auth")]
    [ApiController]

    public class UserController : ControllerBase {
        private readonly TodoContext _context;
        private readonly ILogger _logger;

        public UserController (TodoContext context, ILogger<TodoItem> logger) {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// 登录接口：随便输入字符，获取token，然后添加 Authoritarian
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<object> GetJWTToken (string name, string pass) {
            string jwtStr = string.Empty;
            bool suc = false;
            //这里就是用户登陆以后，通过数据库去调取数据，分配权限的操作
            //这里直接写死了

            if (string.IsNullOrEmpty (name) || string.IsNullOrEmpty (pass)) {
                 return new JsonResult (new {
                    Status = false,
                        message = "用户名或密码不能为空"
                });
            }

            Blog.MiddleWare.TokenModelJWT tokenModel = new Blog.MiddleWare.TokenModelJWT ();
            tokenModel.Uid = 1;
            tokenModel.Role = "Admin";

            jwtStr = JwtHelper.IssueJWT (tokenModel);
            suc = true;

             return Ok (new {
                success = suc,
                    token = jwtStr
            });
        }
    }
}
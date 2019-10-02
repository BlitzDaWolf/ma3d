using System;
using System.Linq;
using System.Threading.Tasks;
using Ma3d.Bearer;
using Ma3d.Context;
using Ma3d.Data;
using Ma3d.DTO.V1;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Ma3d.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly UserContext UserContext;

        public UserController(UserContext context)
        {
            this.UserContext =
                context;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register()
        {
            try
            {
                UserRequest u = JsonConvert.DeserializeObject<UserRequest>(await Helper.ReadBody(Request));
                u.Password = Helper.EncryptPassword(u.Password);

                UserContext.Users.Add(u.GetUser());
                await UserContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return StatusCode(500, new { e.Message });
            }

            return Ok();
        }

        const string secret = "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk";

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login()
        {
            UserLoginRequest ulr = JsonConvert.DeserializeObject<UserLoginRequest>(await Helper.ReadBody(Request));

            User u = UserContext.Users.FirstOrDefault(x => x.Name == ulr.Name);
            if(!Helper.VerifyPassword(ulr.Password, u.Password))
            {
                return StatusCode(401);
            }
            return UserBearer.GetToken(u);
        }

        [HttpGet("me")]
        public ActionResult<User> me()
        {
            string token = Request.Headers["Authorization"];

            string[] bearer = token.Split(' ');
            if(bearer.Length == 2)
            {
                if (bearer[0] != "Bearer") return StatusCode(401);
                token = bearer[1];

                return UserBearer.GetUser(token);
            }

            return StatusCode(401);
        }
    }
}
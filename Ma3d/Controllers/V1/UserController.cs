using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Ma3d.Context;
using Ma3d.Data;
using Ma3d.DTO.V1;
using Microsoft.AspNetCore.Http;
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
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            UserLoginRequest ulr = JsonConvert.DeserializeObject<UserLoginRequest>(await Helper.ReadBody(Request));

            User u = UserContext.Users.FirstOrDefault(x => x.Name == ulr.Name);
            if(!Helper.VerifyPassword(ulr.Password, u.Password))
            {
                return StatusCode(401);
            }

            return encoder.Encode(u, secret);
        }

        [HttpGet("me")]
        public ActionResult<User> me()
        {
            string token = Request.Headers["Authorization"];

            string[] bearer = token.Split(' ');
            if(bearer.Length == 2)
            {
                if (bearer[0] == "Bearer") return StatusCode(401);
                token = bearer[1];

                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);

                User json = JsonConvert.DeserializeObject<User>(decoder.Decode(token, secret, verify: true));
                return json;
            }

            return StatusCode(401);
        }
    }
}
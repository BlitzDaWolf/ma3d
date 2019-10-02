using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Ma3d.Context;
using Ma3d.Data;
using Ma3d.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ma3d.Bearer
{
    public class UserBearer
    {
        const string secret = "GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk";
        private readonly UserContext UserContext;

        public UserBearer(UserContext userContext)
        {
            UserContext = userContext;
        }

        public string GetToken(User user)
        {
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            return encoder.Encode(user, secret);
        }

        public User GetUser(string token)
        {
            IJsonSerializer serializer = new JsonNetSerializer();
            IDateTimeProvider provider = new UtcDateTimeProvider();
            IJwtValidator validator = new JwtValidator(serializer, provider);
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);

            try
            {
                return JsonConvert.DeserializeObject<User>(decoder.Decode(token, secret, verify: true));
            }
            catch(Exception e)
            {
                throw new AuthorizationException(e);
            }
        }

        public User GetUser(HttpRequest request)
        {
            string token = request.Headers["Authorization"];
            string[] bearer = token.Split(' ');

            if (bearer.Length == 2)
            {
                if (bearer[0] != "Bearer") throw new Exception("No bearer token has been set");
                token = bearer[1];

                return GetUser(token);
            }
            else
            {
                throw new Exception("Something went wrong with the authorization");
            }
        }
    }
}

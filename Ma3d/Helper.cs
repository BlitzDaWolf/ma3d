using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Ma3d
{
    public static class Helper
    {
        public static string EncryptPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);
        public static bool VerifyPassword(string password, string hash)=>BCrypt.Net.BCrypt.Verify(password, hash);

        public static async Task<string> ReadBody(HttpRequest request)
        {
            using (var reader = new StreamReader(request.Body))
            {
                return await reader.ReadToEndAsync();
            }
        }
    }
}

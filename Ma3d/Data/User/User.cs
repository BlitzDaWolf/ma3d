using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ma3d.Data
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Username Required")]
        public string Name { get; set; }

        [JsonIgnore]
        [Required( AllowEmptyStrings = false, ErrorMessage = "Password Required")]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "E-Mail Required")]
        public string Email { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

        public bool Suspended { get; set; } = false;

        // public List<UserRole> Roles { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ma3d.Data
{
    public class Country
    {
        [Key]
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The country needs a name")]
        public string name { get; set; }
        public string Short { get; set; }

        public int CallCode { get; set; }
    }
}

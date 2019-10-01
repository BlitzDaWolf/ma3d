using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ma3d.Data
{
    public class Printer
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}

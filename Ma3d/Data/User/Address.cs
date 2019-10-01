using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ma3d.Data
{
    public class Address
    {
        public Guid Id { get; set; }
        public City City { get; set; }

        public string Street { get; set; }
        public string Number { get; set; }
    }
}

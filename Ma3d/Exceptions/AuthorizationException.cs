using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ma3d.Exceptions
{
    public class AuthorizationException : Exception
    {
        public AuthorizationException(Exception e)
            : base ("Unauthrized", e)
        {

        }
    }
}

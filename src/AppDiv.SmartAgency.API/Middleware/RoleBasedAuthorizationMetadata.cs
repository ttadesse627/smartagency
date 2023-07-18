using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDiv.SmartAgency.API.Middleware
{
    public class RoleBasedAuthorizationMetadata : Attribute
    {


        public string[] AllowedRoles { get; }

        public RoleBasedAuthorizationMetadata(params string[] allowedRoles)
        {
            AllowedRoles = allowedRoles;
        }
    }
}

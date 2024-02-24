

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

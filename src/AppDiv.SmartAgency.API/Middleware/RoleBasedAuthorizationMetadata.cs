

namespace AppDiv.SmartAgency.API.Middleware
{
    public class RoleBasedAuthorizationMetadata(params string[] allowedRoles) : Attribute
    {


        public string[] AllowedRoles { get; } = allowedRoles;
    }
}

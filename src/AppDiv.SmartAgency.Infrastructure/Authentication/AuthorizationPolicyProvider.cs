using AppDiv.SmartAgency.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace AppDiv.SmartAgency.Infrastructure.Authentication;

public class AuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : DefaultAuthorizationPolicyProvider(options)
{
    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        // Check if the policy already exists
        var policy = await base.GetPolicyAsync(policyName);
        if (policy != null)
        {
            return policy;
        }

        // Parse the policy name to extract resource and action
        var parts = policyName.Split('.');
        if (parts.Length == 2)
        {
            var resource = parts[0];
            var action = Enum.Parse<PermissionEnum>(parts[1]);

            // Create a new policy with the PermissionRequirement
            var policyBuilder = new AuthorizationPolicyBuilder();
            policyBuilder.AddRequirements(new PermissionRequirement(resource, action));
            return policyBuilder.Build();
        }

        return null;
    }
}
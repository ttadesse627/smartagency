
using AppDiv.CRVS.Application.Common;
using Microsoft.AspNetCore.Identity;

namespace AppDiv.CRVS.Application.Service
{
    public static class IdentityResultExtensions
    {
        public static Result ToApplicationResult(this IdentityResult result)
        {
            return result.Succeeded ? Result.Success() : Result.Failure(result.Errors.Select(e => e.Description));

        }

    }
}
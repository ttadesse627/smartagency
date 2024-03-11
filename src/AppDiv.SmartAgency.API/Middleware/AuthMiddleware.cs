
using System.IdentityModel.Tokens.Jwt;
using AppDiv.SmartAgency.Application.Contracts.DTOs.GroupDTOs;
using AppDiv.SmartAgency.Application.Interfaces;
using AppDiv.SmartAgency.Application.Interfaces.Persistence;
using AppDiv.SmartAgency.Utility.Contracts;

namespace AppDiv.SmartAgency.API.Middleware
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;



        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;

        }

        public async Task InvokeAsync(HttpContext context, IUserRepository userRepository)
        {

            var path = context.Request.Path.ToString();


            if (path == "/api/auth/login")
            {
                await _next(context);
                return;

            }



            var tokenValue = context.Request.Headers["Authorization"].FirstOrDefault();
            if (tokenValue == null)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthenticated");
                return;
            }

            if (tokenValue == null || !tokenValue.StartsWith("Bearer "))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthenticated");
                return;
            }

            var rawToken = tokenValue.Substring("Bearer ".Length);

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(rawToken);
            var tokenValidatorService = context.RequestServices.GetRequiredService<ITokenValidatorService>();
            var isValid = await tokenValidatorService.ValidateAsync(token as JwtSecurityToken);

            if (isValid == false)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthenticated");
                return;
            }

            var endpoint = context.GetEndpoint();
            if (endpoint != null)
            {
                var allowAnonymous = endpoint.Metadata.GetMetadata<AllowAnonymousAttribute>();
                if (allowAnonymous == null)
                {

                    var allowedRoles = endpoint.Metadata.GetMetadata<RoleBasedAuthorizationMetadata>()?.AllowedRoles;
                    if (allowedRoles != null && allowedRoles.Length != 0)
                    {


                        var userIdClaim = token.Claims.FirstOrDefault(c => c.Type == "UserId");

                        // Get the user ID value from the claim, or return null if the claim is not found
                        var userId = userIdClaim?.Value;

                        if (userId != null)
                        {

                            var explicitLoadedProperties = new Dictionary<string, NavigationPropertyType>
                                                {
                                                    { "UserGroups", NavigationPropertyType.COLLECTION }
                                                };
                            var userData = await userRepository.GetWithAsync(userId, explicitLoadedProperties);


                            var userRoles = userData.UserGroups.SelectMany(ug => ug.Permissions
                                 .Select(r => new PermissionDto
                                 {
                                     Name = r.Name,
                                     Actions = r.Actions.Select(ac => ac.ToString()).ToList()
                                     //  Page = r.Value<string>("Page") ?? "",
                                     //  Title = r.Value<string>("Title") ?? "",
                                     //  CanAdd = r.Value<bool>("CanAdd"),
                                     //  CanDelete = r.Value<bool>("CanDelete"),
                                     //  CanViewDetail = r.Value<bool>("CanViewDetail"),
                                     //  CanView = r.Value<bool>("CanView"),
                                     //  CanUpdate = r.Value<bool>("CanUpdate")
                                 })).GroupBy(r => r.Name.Trim(), StringComparer.OrdinalIgnoreCase).Select(g => new PermissionDto
                                 {
                                     Name = g.Key,
                                     Actions = g.SelectMany(p => p.Actions).ToList()
                                     //  Page = g.Key,
                                     //  Title = g.FirstOrDefault()?.Title ?? "",
                                     //  CanAdd = g.Aggregate(false, (acc, x) => acc || x.CanAdd),
                                     //  CanDelete = g.Aggregate(false, (acc, x) => acc || x.CanDelete),
                                     //  CanUpdate = g.Aggregate(false, (acc, x) => acc || x.CanUpdate),
                                     //  CanView = g.Aggregate(false, (acc, x) => acc || x.CanView),
                                     //  CanViewDetail = g.Aggregate(false, (acc, x) => acc || x.CanViewDetail)
                                 }).ToList();


                            if (userRoles != null && userRoles.Count != 0)
                            {

                                // var pageName = allowedRoles[0];
                                foreach (var userRole in userRoles)
                                {
                                    if (allowedRoles[0] == userRole.Name)
                                    {
                                        var actionName = allowedRoles[1];
                                        var propertyInfo = typeof(PermissionDto).GetProperty(actionName);
                                        if (propertyInfo != null && propertyInfo.PropertyType == typeof(bool) && (bool)propertyInfo.GetValue(userRole)!)
                                        {
                                            await _next(context);
                                            return;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                context.Response.StatusCode = 403;
                                await context.Response.WriteAsync("a user doesn't have any role");
                            }
                        }
                    }



                }
                else
                {
                    await _next(context);
                    return;
                }
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("unauthorized");

            }
            else
            {

                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Couldn't get an endpoint");
                return;
            }


        }
    }
}
using IdentityModel;
using IdentityServer4.Models;

namespace TtaApp.IdentityProvider.Resources
{
    internal class ProfileWithRoleIdentityResource : IdentityResources.Profile
    {
        public ProfileWithRoleIdentityResource()
        {
            this.UserClaims.Add(JwtClaimTypes.Role);
        }
    }
}

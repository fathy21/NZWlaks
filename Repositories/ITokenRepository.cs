using Microsoft.AspNetCore.Identity;

namespace NZWalks.API.Repositories
{
    public interface ITokenRepository
    {
        string CreatJwtTokent(IdentityUser user, List<string> roles);
    }
}

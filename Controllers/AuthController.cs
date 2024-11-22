using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager , ITokenRepository tokenRepository)
        {
            _userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username
            };

            var identityresult = await _userManager.CreateAsync(identityUser , registerRequestDto.Password);

            if(identityresult.Succeeded)
            {
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    identityresult = await _userManager.AddToRolesAsync(identityUser , registerRequestDto.Roles);

                    if (identityresult.Succeeded)
                    {
                        return Ok("User was registred! please login. ");
                    }
                }
            }
            return BadRequest("Something was wrong try again!");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await _userManager.FindByEmailAsync(loginRequestDto.Username);

            if(user != null) 
            {
                var CheckPasswordResult =   await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                if (CheckPasswordResult)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles is not null)
                    {
                        //UploadImage Token
                        var jwtToken =  tokenRepository.CreatJwtTokent(user , roles.ToList());
                        var response = new LoginResponseDto
                        {
                            JwtToken = jwtToken
                        };
                        return Ok($"jwtToken : {jwtToken} ");
                    }
                    return Ok("Login has successfuly!");
                }
            }
            return BadRequest("username or password incorrect !");
        }
    }
}

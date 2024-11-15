using DAO.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repository.Interface;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Test2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ISilverJewelryRepo _silverJewelryRepo;
        private readonly IAccountRepo _accountRepo;

        public AuthController(ISilverJewelryRepo silverJewelryRepo, IAccountRepo accountRepo)
        {
            _silverJewelryRepo = silverJewelryRepo;
            _accountRepo = accountRepo;
        }

        [HttpPost("Auth")]
        public async Task<IActionResult> login([FromBody] AccountRequest dto)
        {
            var account = await _accountRepo.Login(dto.Email, dto.Password);
            if (account == null)
            {
                return Unauthorized("Invalid Email or Password");
            }

            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true).Build();

            var claim = new List<Claim>
            {
                new Claim(ClaimTypes.Email, account.EmailAddress),
                new Claim("Role", account.Role.ToString()),
                new Claim("AccountId", account.AccountId.ToString()),
                new Claim("AccountName", account.FullName.ToString())
            };

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                    issuer: configuration["JWT:ValidIssuer"],
                    audience: configuration["JWT:ValidAudience"],
                    claims: claim,
                    expires: DateTime.Now.AddDays(3),
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );
            var toeken1 = new JwtSecurityTokenHandler().WriteToken(token);
            var role = account.Role;
            var AccountId = account.AccountId;
            return Ok(new TokenResponse
            {
                RoleId = role,
                TokenString = toeken1,
                AccountId = AccountId
            });

        }
    }
}

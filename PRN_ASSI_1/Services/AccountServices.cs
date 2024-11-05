using Azure;
using BO.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository;
using Services.DTO;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AccountServices : IAccountServices
    {
        private readonly IConfiguration _configuration;
        private readonly IAccountRepo _repo;
        public AccountServices(IAccountRepo repo, IConfiguration configuration)
        {
            _repo = repo;
            _configuration = configuration;
        }

        public async Task<TokenResponse?> Login(LoginRequest dto)
        {
            var data = await _repo.Login(dto.Email, dto.Password);
            if (data == null)
            {
                return null;
            }

            JwtSecurityToken token = GetToken(data);
            var returnedToken = new TokenResponse()
            {
                TokenString = new JwtSecurityTokenHandler().WriteToken(token),
                RoleId = data.Role,
                AccountId = data.AccountId
            };
            return returnedToken;
        }


        public JwtSecurityToken GetToken(BranchAccount user)
        {
            List<Claim> authClaims = new List<Claim>
            {
                 new Claim(ClaimTypes.Name, user.FullName),
                 new Claim(ClaimTypes.Email, user.EmailAddress),
                 new Claim(ClaimTypes.Role, user.Role.ToString()),
                 new Claim(ClaimTypes.NameIdentifier, user.AccountId.ToString())
            };

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(

                issuer: _configuration["JWT:ValidAudience"],
                audience: _configuration["JWT:ValidIssuer"],
                claims: authClaims,
                expires: DateTime.Now.AddDays(3),
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }

        public ClaimsPrincipal DecodeToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]);

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = "http://localhost:5028",
                    ValidAudience = "http://localhost:5028",
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var claimsIdentity = new ClaimsIdentity(jwtToken.Claims);

                return new ClaimsPrincipal(claimsIdentity);
            }
            catch (Exception ex)
            {
                // Xử lý trường hợp token không hợp lệ
                Console.WriteLine($"Token validation failed: {ex.Message}");
                return null;
            }
        }
    }
}


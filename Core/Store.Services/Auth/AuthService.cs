using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Store.Domain.Entities.Identity;
using Store.Domain.Exceptions.BadRequest;
using Store.Domain.Exceptions.NotFound;
using Store.Domain.Exceptions.Unauthorized;
using Store.Services.Abstractions.Auth;
using Store.Shared.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Auth {
    public class AuthService(UserManager<AppUser> _userManager) : IAuthService {

        public async Task<UserResponse?> LoginAsync(LoginRequest request) {

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null) throw new UserNotFoundException(request.Email);


            var flag = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!flag) throw new UnauthorizedException();


            return new UserResponse() {

                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await GenerateTokenAsync(user)

            };

        }

        public async Task<UserResponse?> RegisterAsync(RegisterRequest request) {

            var user = new AppUser() {

                UserName = request.UserName,
                Email = request.Email,
                DisplayName = request.DisplayName,
                PhoneNumber = request.PhoneNumber,

            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded) throw new RegistrationBadRequestException(result.Errors.Select(e => e.Description).ToList());


            return new UserResponse() {

                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await GenerateTokenAsync(user)

            };

        }
        
        private async Task<string> GenerateTokenAsync(AppUser user) {
            
            var authClaims = new List<Claim> {
                new Claim(ClaimTypes.GivenName, user.DisplayName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles) {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("STRONGSecurityKEYFORAUTHENTICATIONSSTRONGSecurityKEYFORAUTHENTICATIONSSTRONGSecurityKEYFORAUTHENTICATIONSSTRONGSecurityKEYFORAUTHENTICATIONS"));

            var token = new JwtSecurityToken(
                
                    issuer: "https://localhost:7145",
                    audience: "MyStore",
                    claims: authClaims,
                    expires: DateTime.Now.AddDays(2),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );


            return new JwtSecurityTokenHandler().WriteToken(token);

        }

    }
}

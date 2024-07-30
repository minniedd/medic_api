using medic_api.Data;
using medic_api.Data.Models;
using medic_api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace medic_api.Endpoints.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthEndpoint : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public AuthEndpoint(ApplicationDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }


        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            var user = await _dbContext.User.SingleOrDefaultAsync(x => x.username ==request.username);

            if (user == null) 
            {
                return BadRequest("User has not been found");
            }

            if(!VerifyPasswordHash(request.password, user.passwordHash, user.passwordSalt))
            {
                return BadRequest("You entered a wrong password");
            }

            if (!user.isAdmin)
            {
                return Unauthorized("denied access");
            }

            user.lastLoginDate = DateTime.Now;
            _dbContext.User.Update(user);
            await _dbContext.SaveChangesAsync();

            string token = CreateToken(user);

            var refreshToken = GenerateRefreshToken();
            SetRefreshToken(refreshToken);

            return Ok(token);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete("refreshToken");

            return Ok("You logged out!");
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<string>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];

            var user = await _dbContext.User.SingleOrDefaultAsync(x => x.refreshToken == refreshToken);

            if (user == null || !user.refreshToken.Equals(refreshToken))
            {
                return Unauthorized("invalid refresh token");
            }
            else if (user.TokenExpires < DateTime.Now)
            {
                return Unauthorized("token is expired");
            }

            string token = CreateToken(user);
            var newRefreshToken = GenerateRefreshToken();
            SetRefreshToken(newRefreshToken);

            return Ok(token);
        }

        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };

            return refreshToken;
        }

        private void SetRefreshToken(RefreshToken newRefreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            var user = _dbContext.User.SingleOrDefault(x => x.username == User.Identity.Name);
            if (user != null)
            {
                user.refreshToken = newRefreshToken.Token;
                user.TokenExpires = newRefreshToken.Created;
                user.TokenExpires = newRefreshToken.Expires;
                _dbContext.User.Update(user);
                _dbContext.SaveChanges();
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claim = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.username),
                new Claim(ClaimTypes.Role, user.isAdmin ? "Admin" : "User")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _configuration.GetSection("AppSettings:Token").Value));


            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claim,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}

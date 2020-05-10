using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using WebApi.Models;

namespace WebApi.Services
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
    }
    
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly AppSettings _appSettings;
        public UserService(AppDbContext dbContext, IOptions<AppSettings> appSettings)
        {
            this._appSettings = appSettings.Value;
            this._context = dbContext;
        }

        public User Authenticate(string username, string password)
        {
            var user = _context.User.SingleOrDefault(s => s.Username == username && s.Password == password);
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.User.ToList();
        }
    }
}

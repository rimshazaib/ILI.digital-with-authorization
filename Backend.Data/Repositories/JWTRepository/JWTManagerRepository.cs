using Backend.Data;
using Backend.Data.Entities.tokens;
using Backend.Data.IRepositories.IJWTManager;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Backend.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Backend.Application.DataTransferObjects.Register;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Data.Repositories.JWTRepository
{
    public class JWTManagerRepository : IJWTManagerRepository
    {
        private readonly IConfiguration iconfiguration;
        private readonly EFDataContext _context;

        public JWTManagerRepository(EFDataContext context, IConfiguration iconfiguration)
        {
            this._context = context;
            this.iconfiguration = iconfiguration;
        }
        public async Task<Backend.Data.Entities.registration.Register> registration(RegisterDto reg)
        {
            Backend.Data.Entities.registration.Register obj = new Backend.Data.Entities.registration.Register();
            obj.Name = reg.Name;
            obj.Email = reg.Email;
            obj.Password = reg.Password;
            var result = await _context.Register.AddAsync(obj);

            await _context.SaveChangesAsync();
            return result.Entity;
        }
        public Token Authenticate(users user)
        {
            var reg = _context.Register.Where(e => e.Email == user.Email && e.Password == user.Password).FirstOrDefault();
            if (reg == null)
            {
                return null;
            }

            // Else we generate JSON Web Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(iconfiguration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
              {
                 new Claim(ClaimTypes.Email, user.Email)
              }),
                Expires = DateTime.UtcNow.AddHours(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new Token { Tokens = tokenHandler.WriteToken(token) };

        }
    }
}


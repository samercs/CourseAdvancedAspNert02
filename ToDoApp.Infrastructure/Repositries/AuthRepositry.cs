using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoApp.Application.Dtos;
using ToDoApp.Application.Interfaces;
using ToDoApp.Domain.Entity;

namespace ToDoApp.Infrastructure.Repositries;

public class AuthRepositry(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration):IAuthRepositry
{
    public bool Register(RegisterDto dto)
    {
        var user = new ApplicationUser()
        {
            UserName = dto.UserName,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            Gender = dto.Gender,
            BirthDate = dto.BirthDate,
            FullName = dto.FullName
        };
        var result = userManager.CreateAsync(user, dto.Password).Result;
        if (result.Succeeded)
        {
            return true;
        }

        throw new Exception($"Registration Faild. {result.Errors}");
    }

    public LoginResponseDto Login(LoginDto dto)
    {
        var user = userManager.FindByNameAsync(dto.UserName).Result;
        if (user is null)
        {
            return new LoginResponseDto()
            {
                IsSuccess = false
            };
        }

        var loginResult = signInManager.PasswordSignInAsync(user, dto.Password, true, true).Result;
        if(loginResult.Succeeded)
        {
            return new LoginResponseDto()
            {
                IsSuccess = true,
                UserId = user.Id,
                Token = GenerateToken(user)
            };
        }
        return new LoginResponseDto()
        {
            IsSuccess = false
        };
    }

    private string GenerateToken(ApplicationUser user)
    {
        var key = Encoding.ASCII.GetBytes(configuration.GetSection("JWTSecret").Value);
        var roles = userManager.GetRolesAsync(user).Result;
        var clams = new List<Claim>()
        {
            new Claim("Id", user.Id),
            new Claim("Email", user.Email)
        };
        foreach (var role in roles)
        {
            clams.Add(new Claim(ClaimTypes.Role, role));
        }

        var jwtTokenDescriper = new SecurityTokenDescriptor()
        {
            Audience = "ToDoApp",
            Issuer = "ToDoApp",
            Subject = new ClaimsIdentity(clams),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(jwtTokenDescriper);
        return tokenHandler.WriteToken(token);
    }
}
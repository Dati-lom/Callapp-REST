using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Callapp.Context;
using Callapp.Dto;
using Callapp.Models;
using Callapp.Response;

namespace Callapp.Services;

public class AuthService : IAuthService
{
    private readonly Callappdb _callappdb;
    private readonly IConfiguration _configuration;

    public AuthService(Callappdb callappdb, IConfiguration configuration)
    {
        _callappdb = callappdb;
        _configuration = configuration;
    }


    public ProfileResponse Register(UserProfileDto userProfileDto)
    {
        if (_callappdb.Users
            .Any(u => u.Email == userProfileDto.Email
                      || u.Username == userProfileDto.Username))
            return new ProfileResponse
            {
                ErrorMessage = "Email or Username already exists",
                Successful = false
            };
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(userProfileDto.Password);

        var user = new User
        {
            Password = passwordHash,
            Username = userProfileDto.Username,
            Email = userProfileDto.Email
        };

        var userProfile = new UserProfile
        {
            FirstName = userProfileDto.FirstName,
            LastName = userProfileDto.LastName,
            PersonalNumber = userProfileDto.PersonalNumber,
            
            
        };
        user.UserProfile = userProfile;
        userProfile.User = user;
        
         _callappdb.Users.Add(user);
        _callappdb.UserProfiles.Add(userProfile);

        _callappdb.SaveChanges();
        
        Connect(user.UserId);

        return new ProfileResponse
        {
            ErrorMessage = CreateToken(user),
            Successful = true
        };
    }

    private void Connect(int userId)
    {
        var user = _callappdb.Users.Find(userId);
        var userProfile = _callappdb.UserProfiles.Find(userId);
        if (user == null || userProfile == null) return;
        user.UserProfile = userProfile;
        userProfile.User = user;

        _callappdb.SaveChanges();

    }


    public ProfileResponse Login(LoginDto loginDto)
    {
        var user = _callappdb.Users.FirstOrDefault(u => u.Email == loginDto.Email);

        if (user == null)
            return new ProfileResponse
            {
                ErrorMessage = "Email is incorrect",
                Successful = false
            };

        if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            return new ProfileResponse
            {
                ErrorMessage = "Password is incorrect",
                Successful = false
            };

        var token = CreateToken(user);

        return new ProfileResponse
        {
            ErrorMessage = token,
            Successful = true
        };
    }

    public IEnumerable<User> Users { get; set; }

    private string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Email)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"]));

        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(2),
            signingCredentials: cred
        );
        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }
}
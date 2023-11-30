using Callapp.Dto;
using Callapp.Models;
using Callapp.Response;

namespace Callapp.Services;

public interface IAuthService
{
    IEnumerable<User> Users { get; }
    ProfileResponse Register(UserProfileDto userProfileDto);
    ProfileResponse Login(LoginDto loginDto);
}
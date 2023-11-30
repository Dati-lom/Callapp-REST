using Callapp.Dto;
using Callapp.Models;
using Callapp.Response;
using Callapp.Dto;
using Callapp.Models;
using Callapp.Response;

namespace Callapp.Services;

public interface IUserProfileService
{
    IEnumerable<UserProfile> UserProfiles { get; }
    ProfileResponse DeleteUser(string email);
    ProfileResponse AddUser(UserProfileDto userProfileDto);
    ProfileResponse EditUser(int id,UserProfileDto userProfileDto);
    User GetProfile(int Id);
}
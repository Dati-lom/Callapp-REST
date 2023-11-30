using Callapp.Context;
using Callapp.Dto;
using Callapp.Models;
using Callapp.Response;

namespace Callapp.Services;

public class UserProfileService : IUserProfileService
{
    private readonly Callappdb _callappdb;

    public UserProfileService(Callappdb callappdb)
    {
        _callappdb = callappdb;
    }

    public IEnumerable<UserProfile> UserProfiles { get; }

    public ProfileResponse DeleteUser(string email)
    {
        var user = _callappdb.Users.FirstOrDefault(u => u.Email == email);

        if (user == null)
            return new ProfileResponse
            {
                Successful = false,
                ErrorMessage = "User not found"
            };

        _callappdb.Users.Remove(user);
        _callappdb.SaveChanges();

        return new ProfileResponse
        {
            Successful = true,
            ErrorMessage = "Deleted User Succesfully" + user
        };
    }


    public ProfileResponse AddUser(UserProfileDto userDto)
    {
        throw new NotImplementedException();
    }

    public ProfileResponse EditUser(int id, UserProfileDto userProfileDto)
    {
        var user = _callappdb.Users.Find(id);
        if (user == null)
        {
            return new ProfileResponse
            {
                ErrorMessage = "No such user Exists",
                Successful = false
            };
        }
        
        var profile = _callappdb.UserProfiles.Find(user.UserProfIdFk);
        if (profile == null)
            return new ProfileResponse
            {
                Successful = false,
                ErrorMessage = "Error Finding Profile"
            };

        user.Email = userProfileDto.Email;
        user.Username = userProfileDto.Username;
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(userProfileDto.Password);
        user.Password = hashedPassword;

        profile.FirstName = userProfileDto.FirstName;
        profile.LastName = userProfileDto.LastName;
        profile.PersonalNumber = userProfileDto.PersonalNumber;

        _callappdb.SaveChanges();

        return new ProfileResponse
        {
            Successful = true,
            ErrorMessage = "Edited Succesfully"
        };

    }

    public User GetProfile(int Id)
    {
        return _callappdb.Users.FirstOrDefault(u => u.UserId== Id);
        ;
    }
}
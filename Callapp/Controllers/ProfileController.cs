using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Callapp.Dto;
using Callapp.Models;
using Callapp.Response;
using Callapp.Services;

namespace Callapp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProfileController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IUserProfileService _userProfileService;

    public ProfileController(IUserProfileService userProfileService, IAuthService authService)
    {
        _userProfileService = userProfileService;
        _authService = authService;
    }

    [HttpDelete("delete")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<User> DeleteUser([FromQuery] string email)
    {
        var response = _userProfileService.DeleteUser(email);
        if (response.Successful) return Ok(response.ErrorMessage);
        return BadRequest(response.ErrorMessage);
    }

    [HttpPost("add-user")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<User> AddUser([FromBody] UserProfileDto userProfileDto)
    {
        var response = _authService.Register(userProfileDto);
        if (response.Successful) return Ok("Account Created");

        return BadRequest(response.ErrorMessage);
    }

    [HttpGet("get-user/{id}")]
    public ActionResult<User> GetUserProf(int id)
    {
        var response = _userProfileService.GetProfile(id);
        if (response == null) return BadRequest("User does not exits");
        return Ok(response);
    }
    

    [HttpPut("edit-user/{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<UserProfile> EditProfile([FromBody] UserProfileDto userProfileDto, int id)
    {
        var response = _userProfileService.EditUser(id, userProfileDto);
        return response.Successful ? Ok(response.ErrorMessage) : BadRequest(response.ErrorMessage);
    }
}
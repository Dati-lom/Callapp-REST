using Microsoft.AspNetCore.Mvc;
using Callapp.Dto;
using Callapp.Models;
using Callapp.Services;

namespace Callapp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public ActionResult<User> Register([FromBody] UserProfileDto userProfileDto)
    {
        var response = _authService.Register(userProfileDto);
        if (response.Successful) return Ok(response.ErrorMessage);

        return BadRequest(response.ErrorMessage);
    }

    [HttpPost("login")]
    public ActionResult<User> Login([FromBody] LoginDto loginDto)
    {
        var response = _authService.Login(loginDto);

        if (response.Successful) return Ok(response.ErrorMessage);

        return BadRequest(response.ErrorMessage);
    }
}
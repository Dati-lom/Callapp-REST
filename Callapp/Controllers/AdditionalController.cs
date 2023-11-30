using Callapp.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Callapp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdditionalController:ControllerBase
{
    private readonly IAdditionalService _additionalService;

    public AdditionalController(IAdditionalService additionalService)
    {
        _additionalService = additionalService;
    }
    

    [HttpGet("get-posts/{id}")]
    public async Task<ActionResult> GetPost(int id)
    {
        var result = await _additionalService.GetPost(id);
        if (result.Count == 0) return BadRequest("Data was not found");
        return Ok(result);
    }
    
    
    [HttpGet("get-album/{id}")]
    public async Task<ActionResult> GetAlbum(int id)
    {
        var result = await _additionalService.GetAlbum(id);
        if (result.Count == 0) return BadRequest("Data was not found");
        return Ok(result);
    }
    
    [HttpGet("get-todo/{id}")]
    public async Task<ActionResult> GetTodo(int id)
    {
        var result = await _additionalService.GetTodo(id);
        if (result.Count == 0) return BadRequest("Data was not found");
        return Ok(result);
    }
}
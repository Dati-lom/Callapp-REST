using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Callapp.Models;

public class Comment
{
    public int Id { get; set; }
    public int PostId { get; set; }
    public int UserId { get; set; }
    public string Text { get; set; }

    public Action TheAction { get; set; }
}
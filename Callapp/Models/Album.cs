using Newtonsoft.Json;

namespace Callapp.Models;


[JsonObject(MemberSerialization.OptIn)]
public class Album
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; }
    public Action TheAction { get; set; }
}

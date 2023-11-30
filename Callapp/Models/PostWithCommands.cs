namespace Callapp.Models;

public class PostWithComments
{
    public Post Post { get; set; }
    public List<Comment> Comments { get; set; }
}
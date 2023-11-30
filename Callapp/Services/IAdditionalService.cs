using Callapp.Models;
using Callapp.Response;

namespace Callapp.Services;

public interface IAdditionalService
{
    Task<List<PostWithComments>> GetPost(int userId);
    Task<List<AlbumWithPhotos>> GetAlbum(int userId);
    Task<List<Todo>> GetTodo(int userId);

}
using Callapp.Models;
using Callapp.Response;
using Newtonsoft.Json;

namespace Callapp.Services;

public class AdditionalService:IAdditionalService
{
    private readonly HttpClient _httpClient;

    public AdditionalService(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<List<PostWithComments>> GetPost(int userId)
    {
        var posts = await _httpClient.GetFromJsonAsync<List<Post>>("https://my-json-server.typicode.com/Dati-lom/callapp/posts");

        if (posts == null)
        {
            return null;
        }

        var userPosts = posts.FindAll(post => post.UserId == userId);

        var postWithCommentsList = new List<PostWithComments>();
        foreach (var post in userPosts)
        {
            var comments = await _httpClient.GetFromJsonAsync<List<Comment>>($"https://my-json-server.typicode.com/Dati-lom/callapp/comments?postId={post.Id}");
            var postWithComments = new PostWithComments { Post = post, Comments = comments };
            postWithCommentsList.Add(postWithComments);
        }

        return postWithCommentsList;
        
    }
    

    public async Task<List<AlbumWithPhotos>> GetAlbum(int userId)
    {
        var albums = await _httpClient.GetFromJsonAsync<List<Album>>("https://my-json-server.typicode.com/Dati-lom/callapp/albums");

        if (albums == null)
        {
            return null;
        }

        var userAlbums = albums.FindAll(album => album.UserId == userId);

        var albumWithPhotosList = new List<AlbumWithPhotos>();
        foreach (var album in userAlbums)
        {
            var photos = await _httpClient.GetFromJsonAsync<List<Photo>>($"https://my-json-server.typicode.com/Dati-lom/callapp/photos?albumId={album.Id}");
            var albumWithPhotos = new AlbumWithPhotos { Album = album, Photos = photos };
            albumWithPhotosList.Add(albumWithPhotos);
        }

        return albumWithPhotosList;
    }

    public async Task<List<Todo>> GetTodo(int userId)
    {
        var todos = await _httpClient.GetFromJsonAsync<List<Todo>>("https://my-json-server.typicode.com/Dati-lom/callapp/todos");
        if (todos == null)
        {
            return null;
        }
        var userTodos = todos.FindAll(todo => todo.UserId == userId);
        return userTodos;
        }
      
    }


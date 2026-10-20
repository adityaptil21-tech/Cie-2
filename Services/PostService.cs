using HospitalAppointmentSystem.Models;

namespace HospitalAppointmentSystem.Services;

public interface IPostService
{
    Task<IReadOnlyList<Post>> GetPostsAsync(CancellationToken cancellationToken = default);
}

public class PostService : IPostService
{
    private readonly HttpClient _http;

    public PostService(HttpClient http) => _http = http;

    // GET https://jsonplaceholder.typicode.com/posts
    public async Task<IReadOnlyList<Post>> GetPostsAsync(CancellationToken cancellationToken = default)
    {
        var posts = await _http.GetFromJsonAsync<List<Post>>("posts", cancellationToken);
        return posts ?? new List<Post>();
    }
}

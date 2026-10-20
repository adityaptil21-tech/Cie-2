using System.Text.Json;
using HospitalAppointmentSystem.Models;
using HospitalAppointmentSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAppointmentSystem.Controllers;

public class PostsController : Controller
{
    private readonly IPostService _postService;
    private readonly ILogger<PostsController> _logger;

    public PostsController(IPostService postService, ILogger<PostsController> logger)
    {
        _postService = postService;
        _logger = logger;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var model = new PostsViewModel();
        try
        {
            model.Posts = await _postService.GetPostsAsync(cancellationToken);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Could not reach JSONPlaceholder.");
            model.ErrorMessage = "We could not reach the posts service. Please check your internet connection and try again.";
        }
        catch (TaskCanceledException ex) when (!cancellationToken.IsCancellationRequested)
        {
            _logger.LogError(ex, "JSONPlaceholder request timed out.");
            model.ErrorMessage = "The posts service took too long to respond. Please try again.";
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, "Invalid JSON from JSONPlaceholder.");
            model.ErrorMessage = "The posts service returned unexpected data.";
        }
        return View(model);
    }
}

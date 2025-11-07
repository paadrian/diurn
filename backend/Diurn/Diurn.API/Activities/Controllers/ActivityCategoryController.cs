using Diurn.Contracts;
using Microsoft.AspNetCore.Mvc;
using Diurn.Activities.Mappers;
using Diurn.Application;

namespace Diurn.Activities.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class ActivityCategoryController : ControllerBase
{
    private readonly IActivityCategoryRepository _activityCategoryRepository;
    private readonly ILogger<ActivityCategoryController> _logger;

    public ActivityCategoryController(IActivityCategoryRepository activityCategoryRepository, ILogger<ActivityCategoryController> logger)
    {
        _activityCategoryRepository = activityCategoryRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ActivityCategoryResponse>>> Get()
    {
        var userName = User.Identity?.Name ?? "Unknown";
        var userId = User.Claims.FirstOrDefault(c => c.Type == "oid")?.Value ?? "Unknown";
        
        _logger.LogInformation("User {UserName} ({UserId}) is getting activities", userName, userId);
        return Ok ((await _activityCategoryRepository.GetAsync()).ToResponse());
    }
}
using Diurn.Contracts;
using Microsoft.AspNetCore.Mvc;
using Diurn.Activities.Mappers;

namespace Diurn.Activities.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class ActivityCategoryController : ControllerBase
{
    private readonly IActivityCategoryRepository _activityCategoryRepository;

    public ActivityCategoryController(IActivityCategoryRepository activityCategoryRepository)
    {
        _activityCategoryRepository = activityCategoryRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ActivityCategoryResponse>>> Get()
    {
        return Ok ((await _activityCategoryRepository.GetAsync()).ToResponse());
    }
}
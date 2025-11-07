using Diurn.Activities.Mappers;
using Diurn.Application;
using Diurn.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace Diurn.Activities.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "ApiAccessPolicy")]
[ApiController]
[Route("api/[controller]")]
// [RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
public class ActivitiesController : ControllerBase
{
    private readonly ActivityService _activityService;
    private readonly ILogger<ActivitiesController> _logger;

    public ActivitiesController(ActivityService activityService, ILogger<ActivitiesController> logger)
    {
        _activityService = activityService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<ActivityResponse>>> Get([FromQuery] MyFilter filter)
    {
        var userName = User.Claims.FirstOrDefault(c => c.Type == "name")?.Value ?? "Unknown";
        var userId = User.Claims.FirstOrDefault(c => c.Type == "oid")?.Value ?? "Unknown";
        
        _logger.LogInformation("User {UserName} ({UserId}) is getting activities", userName, userId);
        var result = (await _activityService.GetAsync(filter.PageNo, filter.PageSize)).ToResponse();
        return Ok(new List<ActivityResponse>{ new (Guid.Empty, "Adrian",
            new ActivityTypeResponse(Guid.Empty, "TypeAdrian", ActivityCategoryResponse.Mental))});
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ActivityResponse>> Get(Guid id)
    {
        var userName = User.Identity?.Name ?? "Unknown";
        var userId = User.Claims.FirstOrDefault(c => c.Type == "oid")?.Value ?? "Unknown";
        
        _logger.LogInformation("User {UserName} ({UserId}) is getting activity {Guid}", userName, userId, id);
        
        var activity = await _activityService.GetAsync(id);
        if (activity is null)
            return NotFound();

        return Ok(activity.ToResponse());
    }

    [HttpPost]
    public async Task<ActionResult> Create(ActivityCreate request)
    {
        var activity = await _activityService.CreateAsync(request.ToModel());
        return CreatedAtAction(nameof(Get), activity.Id);
    }
    
    [HttpPost("{id:guid}")]
    public async Task<ActionResult> Update(Guid id, ActivityUpdate request)
    {
        if (id != request.Id)
            return BadRequest();
        
        var result = await _activityService.UpdateAsync(request.ToModel());
        if (result is null)
            return NotFound();
        
        return CreatedAtAction(nameof(Get), result.Id);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var result = await _activityService.DeleteAsync(id);
        if (!result)
            return NotFound();
        
        return NoContent();
    }
}
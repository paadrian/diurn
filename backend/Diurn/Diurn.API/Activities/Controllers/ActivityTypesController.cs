using Diurn.Activities.Mappers;
using Diurn.Application;
using Diurn.Contracts;
using Diurn.Core;
using Microsoft.AspNetCore.Mvc;

namespace Diurn.Activities.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ActivityTypesController : ControllerBase
{
    private readonly ActivityTypeService _activityService;

    public ActivityTypesController(ActivityTypeService activityService)
    {
        _activityService = activityService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ActivityTypeResponse>>> Get()
    {
        return Ok((await _activityService.GetAsync()).ToResponse());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ActivityTypeResponse>> Get(Guid id)
    {
        var activityType = await _activityService.GetAsync(id);
        if (activityType is null)
            return NotFound();
        
        return Ok(activityType.ToResponse());
    }

    [HttpPost]
    public async Task<ActionResult<ActivityTypeResponse>> Create(ActivityTypeCreate activityType)
    {
        var result = await _activityService.CreateAsync(activityType.ToModel());
        return Ok(result.ToResponse());
    }

    [HttpPost("{id:guid}")]
    public async Task<ActionResult> Update(Guid id, ActivityType activityType)
    {
        if (id != activityType.Id)
            return BadRequest();
        
        var result = await _activityService.UpdateAsync(activityType);
        if (result is null)
            return NotFound();
        
        return CreatedAtAction(nameof(Get), result.Id);
    }
}
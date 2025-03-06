using Diurn.Activities.Mappers;
using Diurn.Application;
using Diurn.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Diurn.Activities.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ActivityController : ControllerBase
{
    private readonly ActivityService _activityService;

    public ActivityController(ActivityService activityService)
    {
        _activityService = activityService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ActivityResponse>>> Get([FromQuery] MyFilter filter)
    {
        var result = (await _activityService.GetAsync(filter.PageNo, filter.PageSize)).ToResponse();
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ActivityResponse>> Get(Guid id)
    {
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
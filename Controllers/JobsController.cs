using JobApi.Models;
using JobApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace JobApi.Contollers;

[ApiController]
[Route("api/[controller]")]
public class JobsController : ControllerBase
{
    private readonly IJobService _jobService;

    public JobsController(IJobService jobService)
    {
        _jobService = jobService;
    }

    [HttpPost]
    public ActionResult<Job> CreateJob(Job job)
    {
        var createdJob = _jobService.CreateJob(job);

        return CreatedAtAction(nameof(GetJobById), new { id = createdJob.Id }, createdJob);
    }

    [HttpGet]
    public ActionResult<IEnumerable<Job>> GetAllJobs([FromQuery] string? status)
    {
        var jobs = _jobService.GetAllJobs();

        if (!string.IsNullOrWhiteSpace(status))
        {
            jobs = jobs.Where(job =>
                job.Status.Equals(status, StringComparison.OrdinalIgnoreCase));
        }

        return Ok(jobs);
    }

    [HttpGet("{id:guid}")]
    public ActionResult<Job> GetJobById(Guid id)
    {
        var job = _jobService.GetJobById(id);

        if (job == null)
        {
            return NotFound();
        }

        return Ok(job);
    }

    [HttpPost("{id:guid}/close")]
    public ActionResult CloseJob(Guid id)
    {
        var closed = _jobService.CloseJob(id);

        if (!closed)
        {
            return NotFound();
        }

        return NoContent();
    }
}
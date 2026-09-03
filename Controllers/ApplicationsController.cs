using JobApi.Models;
using JobApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace JobApi.Controllers;

[ApiController]
[Route("api/jobs/{jobId:guid}/applications")]
public class ApplicationsController : ControllerBase
{
    private readonly IApplicationService _applicationService;

    public ApplicationsController(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    [HttpPost]
    public ActionResult<Application> CreateApplication (Guid jobId, Application application)
    {
        var createdApplication = 
            _applicationService.CreateApplication(jobId, application);

        if (createdApplication == null)
        {
            return NotFound("Job does not exist or is closed");
        }

        return Ok(createdApplication);
    }

    [HttpGet]
    public ActionResult<IEnumerable<Application>> GetApplications(Guid jobId)
    {
        var applications =
            _applicationService.GetApplicationsByJobId(jobId);

        return Ok(applications);
    }

}
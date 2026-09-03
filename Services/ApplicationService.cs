using JobApi.Models;
using JobApi.Repositories;

namespace JobApi.Services;

public class ApplicationService : IApplicationService
{
    private readonly IApplicationRepository _applicationRepository;
    private readonly IJobRepository _jobRepository;

    public ApplicationService(IApplicationRepository applicationRepository, IJobRepository jobRepository)
    {
        _applicationRepository = applicationRepository;
        _jobRepository = jobRepository;
    }

    public Application? CreateApplication(Guid jobId, Application application)
    {
        var job = _jobRepository.GetById(jobId);

        if (job ==null)
        {
            return null;
        }

        if (job.Status != "OPEN")
        {
            return null;
        }

        application.Id = Guid.NewGuid();
        application.JobId = jobId;
        application.SubmittedDateTime = DateTime.UtcNow;

        _applicationRepository.Add(application);

        return application;
    }

    public IEnumerable<Application> GetApplicationsByJobId(Guid jobId)
    {
        return _applicationRepository.GetByJobId(jobId);
    }

}
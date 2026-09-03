using JobApi.Models;
using JobApi.Repositories;

namespace JobApi.Services;

public class JobService : IJobService
{
    private readonly IJobRepository _jobRepository;

    public JobService(IJobRepository jobRepository)
    {
        _jobRepository = jobRepository;
    }

    public IEnumerable<Job> GetAllJobs()
    {
        return _jobRepository.GetAll();
    }

    public Job? GetJobById(Guid id)
    {
        return _jobRepository.GetById(id);
    }

    public Job CreateJob(Job job)
    {
        job.Id = Guid.NewGuid();
        job.CreatedDateTime = DateTime.UtcNow;
        job.Status = "OPEN";

        _jobRepository.Add(job);

        return job;
    }

    public bool CloseJob(Guid id)
    {
        var job = _jobRepository.GetById(id);

        if (job == null)
        {
            return false;
        }

        job.Status = "CLOSED";

        _jobRepository.Update(job);

        return true;

    }
}
using JobApi.Models;

namespace JobApi.Services;

public interface IJobService
{
    IEnumerable<Job> GetAllJobs();
    Job? GetJobById(Guid id);
    Job CreateJob(Job job);
    bool CloseJob(Guid id);

}
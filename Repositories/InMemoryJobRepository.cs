using JobApi.Models;

namespace JobApi.Repositories;

public class InMemoryJobRepository : IJobRepository
{
    private readonly List<Job> _jobs = new();

    public IEnumerable<Job> GetAll()
    {
        return _jobs;
    }

    public Job? GetById(Guid id)
    {
        return _jobs.FirstOrDefault(job => job.Id == id);
    }

    public void Add(Job job)
    {
        _jobs.Add(job);
    }

}
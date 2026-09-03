using JobApi.Models;

namespace JobApi.Repositories;

public interface IJobRepository
{
    IEnumerable<Job> GetAll();
    Job? GetById(Guid id);
    void Add(Job job);
    void Update(Job job);
}
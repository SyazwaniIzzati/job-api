using JobApi.Models;

namespace JobApi.Repositories;

public interface IApplicationRepository
{
    IEnumerable<Application> GetByJobId(Guid Jobid);
    void Add(Application application);
}
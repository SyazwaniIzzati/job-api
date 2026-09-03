using JobApi.Models;

namespace JobApi.Repositories;

public class InMemoryApplicationRepository : IApplicationRepository
{
    private readonly List<Application> _applications = new();

    public IEnumerable<Application> GetByJobId(Guid jobId)
    {
        return _applications
            .Where(application => application.JobId == jobId);
    }

    public void Add(Application application)
    {
        _applications.Add(application);
    }
}
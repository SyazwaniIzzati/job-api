using JobApi.Models;

namespace JobApi.Services;

public interface IApplicationService
{
    Application? CreateApplication(Guid jobId, Application application);
    IEnumerable<Application> GetApplicationsByJobId(Guid jobId);
}
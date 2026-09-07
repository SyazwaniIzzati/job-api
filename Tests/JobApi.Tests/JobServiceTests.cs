using JobApi.Models;
using JobApi.Repositories;
using JobApi.Services;

namespace JobApi.Tests;

public class JobServiceTests
{
    [Fact]
    public void CreateJob_ShouldSetStatusToOpen()
    {
    
        var repository = new InMemoryJobRepository();
        var service = new JobService(repository);

        var job = new Job
        {
            Title = "Software Engineer",
            Description = "Develop backend applications",
            Location = "Kuala Lumpur"
        };

       
        var result = service.CreateJob(job);

        
        Assert.Equal("OPEN", result.Status);
    }

    [Fact]
    public void CloseJob_ShouldSetStatusToClosed()
    {
        
        var repository = new InMemoryJobRepository();
        var service = new JobService(repository);

        var job = new Job
        {
            Title = "Software Engineer",
            Description = "Develop backend applications",
            Location = "Kuala Lumpur"
        };

        var createdJob = service.CreateJob(job);

        
        var result = service.CloseJob(createdJob.Id);

        
        Assert.True(result);
        Assert.Equal("CLOSED", createdJob.Status);
    }

    [Fact]
    public void CloseJob_ShouldReturnFalse_WhenJobDoesNotExist()
    {
        
        var repository = new InMemoryJobRepository();
        var service = new JobService(repository);

        var randomJobId = Guid.NewGuid();

        var result = service.CloseJob(randomJobId);

        Assert.False(result);
    }
}
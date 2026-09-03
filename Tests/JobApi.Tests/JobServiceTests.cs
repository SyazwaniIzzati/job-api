using JobApi.Models;
using JobApi.Repositories;
using JobApi.Services;

namespace JobApi.Tests;

public class JobServiceTests
{
    [Fact]
    public void CreateJob_ShouldSetStatusToOpen()
    {
        // Arrange
        var repository = new InMemoryJobRepository();
        var service = new JobService(repository);

        var job = new Job
        {
            Title = "Software Engineer",
            Description = "Develop backend applications",
            Location = "Kuala Lumpur"
        };

        // Act
        var result = service.CreateJob(job);

        // Assert
        Assert.Equal("OPEN", result.Status);
    }

    [Fact]
    public void CloseJob_ShouldSetStatusToClosed()
    {
        // Arrange
        var repository = new InMemoryJobRepository();
        var service = new JobService(repository);

        var job = new Job
        {
            Title = "Software Engineer",
            Description = "Develop backend applications",
            Location = "Kuala Lumpur"
        };

        var createdJob = service.CreateJob(job);

        // Act
        var result = service.CloseJob(createdJob.Id);

        // Assert
        Assert.True(result);
        Assert.Equal("CLOSED", createdJob.Status);
    }

    [Fact]
    public void CloseJob_ShouldReturnFalse_WhenJobDoesNotExist()
    {
        // Arrange
        var repository = new InMemoryJobRepository();
        var service = new JobService(repository);

        var randomJobId = Guid.NewGuid();

        // Act
        var result = service.CloseJob(randomJobId);

        // Assert
        Assert.False(result);
    }
}
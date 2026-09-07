using JobApi.Models;
using JobApi.Repositories;
using JobApi.Services;

namespace JobApi.Tests;

public class ApplicationServiceTests
{
    [Fact]
    public void CreateApplication_ShouldSucceed_WhenJobIsOpen()
    {
        
        var jobRepository = new InMemoryJobRepository();
        var applicationRepository = new InMemoryApplicationRepository();
        var service = new ApplicationService(
            applicationRepository,
            jobRepository);

        var job = new Job
        {
            Title = "Software Engineer",
            Description = "Develop backend applications",
            Location = "Kuala Lumpur"
        };

        var createdJob = new JobService(jobRepository).CreateJob(job);

        var application = new Application
        {
            CandidateName = "John",
            CandidateEmail = "john@example.com"
        };

        var result = service.CreateApplication(
            createdJob.Id,
            application);

        Assert.NotNull(result);
        Assert.Equal(createdJob.Id, result.JobId);
    }

    [Fact]
    public void CreateApplication_ShouldFail_WhenJobIsClosed()
    {
        var jobRepository = new InMemoryJobRepository();
        var applicationRepository = new InMemoryApplicationRepository();

        var service = new ApplicationService(
            applicationRepository,
            jobRepository);

        var job = new Job
        {
            Title = "Software Engineer",
            Description = "Develop backend applications",
            Location = "Kuala Lumpur"
        };

        var jobService = new JobService(jobRepository);

        var createdJob = jobService.CreateJob(job);

        jobService.CloseJob(createdJob.Id);

        var application = new Application
        {
            CandidateName = "John",
            CandidateEmail = "john@example.com"
        };

        var result = service.CreateApplication(
            createdJob.Id,
            application);

        Assert.Null(result);
    }
}
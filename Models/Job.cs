namespace JobApi.Models;

public class Job
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public DateTime CreatedDateTime { get; set; }
    public string Status { get; set; } = string.Empty;
}
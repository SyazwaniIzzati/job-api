namespace JobApi.Models;

public class Application
{
    public Guid Id { get; set; }
    public Guid JobId { get; set; }
    public string CandidateName { get; set; } = string.Empty;
    public string CandidateEmail { get; set; } = string.Empty;
    public string Summary {get; set; } = string.Empty;
    public DateTime SubmittedDateTime { get; set; }
}
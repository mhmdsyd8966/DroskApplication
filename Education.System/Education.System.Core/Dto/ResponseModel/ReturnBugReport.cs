namespace Education.System.Core.Dto.ResponseModel;

public class ReturnBugReport
{
    public Guid BugReportId { set; get; }
    public string Name { set; get; }
    public string Content { set; get; }
    public DateTime CreatedAt { set; get; }
    public bool Finished { set; get; }
}


using Education.System.Core.Application;
using Education.System.Core.Dto.ApplicationDto;
using Education.System.Core.Dto.ResponseModel;

namespace Education.System.IServices.IApplicationService;

public interface IBugReportService
{
    Task<BugReport> ReportABug(BugReportDto dto);
    Task<List<ReturnBugReport>> GetAllBugReports();
    Task<bool> FinishBugReport(Guid id);
    Task<BugReport> GetBugReportById(Guid id);
}
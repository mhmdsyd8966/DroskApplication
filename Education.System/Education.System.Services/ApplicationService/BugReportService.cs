using Education.System.Core.Application;
using Education.System.Core.Dto.ApplicationDto;
using Education.System.Core.Dto.ResponseModel;
using Education.System.IServices.IApplicationService;
using Education.System.Presentation.Context;
using Microsoft.EntityFrameworkCore;

namespace Education.System.Services.ApplicationService;

public class BugReportService(TheLayerContext context) : IBugReportService
{
    public async Task<List<ReturnBugReport>> GetAllBugReports()
    {
        var reports = await context.BugReports.Select(br => new ReturnBugReport
        {
            Name = br.FirstName + " " + br.LastName,
            Content = br.BugContent,
            BugReportId = br.Id,
            CreatedAt = br.CreatedAt,
            Finished = br.Finshed
        }).OrderBy(br => br.CreatedAt).ToListAsync();
        return reports;
    }

    public async Task<BugReport> ReportABug(BugReportDto dto)
    {
        var consumer = await context.Consumers.FirstOrDefaultAsync(x => x.Id == dto.UserId);
        var report = new BugReport()
        {
            Id = Guid.NewGuid(),
            consumer = consumer,
            BugContent = dto.BugContent,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            CreatedAt = DateTime.UtcNow,
            Finshed = false
        };
        await context.BugReports.AddAsync(report);
        await context.SaveChangesAsync();
        return report;
    }

    public async Task<bool> FinishBugReport(Guid id)
    {
        var bug = await GetBugReportById(id);
        if (bug.Finshed)
        {
            throw new Exception("Bug report already finished");
        }
        bug.Finshed = true;
        context.BugReports.Update(bug);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<BugReport> GetBugReportById(Guid id) =>
        await context.BugReports.FirstOrDefaultAsync(x => x.Id == id) ??
        throw new Exception("Can't find bug report with this id");
}
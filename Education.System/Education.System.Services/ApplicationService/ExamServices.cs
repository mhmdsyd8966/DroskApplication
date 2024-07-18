using Education.System.Core.Application;
using Education.System.Core.Dto.ApplicationDto;
using Education.System.Core.Dto.ResponseModel;
using Education.System.IServices.IApplicationService;
using Education.System.Presentation.Context;
using Microsoft.EntityFrameworkCore;

namespace Education.System.Services.ApplicationService;

public class ExamServices(TheLayerContext context) : IExamServices
{
    public async Task AddExam(ExamDto model)
    {
        var exam = model.ToModel();
        await context.Exams.AddAsync(exam);
        await context.SaveChangesAsync();
    }

    public async Task<List<ExamReternedDto>> GetAllExams() =>
        await context.Exams.Select(e => new ExamReternedDto()
        {
            Name = e.Name,
            TeacherId = e.TeacherId,
            ExamLink = e.ExamLink,
            Id = e.Id
        }).ToListAsync();

    public async Task<List<ExamReternedDto>> GetExamsOfTeacher(string teacherId) =>
        await context.Exams.Select(e => new ExamReternedDto()
        {
            Name = e.Name,
            TeacherId = e.TeacherId,
            ExamLink = e.ExamLink,
            Id = e.Id
        }).Where(e => e.TeacherId.Equals(teacherId)).ToListAsync();

    public async Task<ExamReternedDto> GetExamById(Guid examId) =>
        await context.Exams.Select(e => new ExamReternedDto()
        {
            Name = e.Name,
            TeacherId = e.TeacherId,
            ExamLink = e.ExamLink,
            Id = e.Id
        }).FirstOrDefaultAsync(e => e.Id.Equals(examId)) ?? throw new Exception("No Exam With this Id");

    public async Task<ExamReternedDto> EditExam(Guid examId, EditedExamDto model)
    {
        var exam = await GetExam(examId);
        exam.ExamLink = string.IsNullOrEmpty(model.ExamLink) ? exam.ExamLink : model.ExamLink;
        exam.Name = string.IsNullOrEmpty(model.Name) ? exam.Name : model.Name;
        context.Exams.Update(exam);
        await context.SaveChangesAsync();
        return new ExamReternedDto()
        {
            Name = exam.Name,
            TeacherId = exam.TeacherId,
            ExamLink = exam.ExamLink,
            Id = exam.Id
        };
    }

    public async Task DeleteExam(Guid examId)
    {
        var exam = await GetExam(examId);
        context.Exams.Remove(exam);
        await context.SaveChangesAsync();
    }

    private async Task<Exam> GetExam(Guid id) => await context.Exams.FirstOrDefaultAsync(e => e.Id.Equals(id)) ??
                                                 throw new Exception("No Exam With this Id");
}
using Education.System.Core.Application;
using Education.System.Core.Dto.ApplicationDto;
using Education.System.Core.Dto.ResponseModel;
using Education.System.Core.Views;

namespace Education.System.IServices.IApplicationService;

public interface IExamServices
{
    Task<List<ExamReternedDto>> GetAllExams();
    Task AddExam(ExamDto model);
    Task<List<ExamReternedDto>> GetExamsOfTeacher(string teacherId);
    Task<ExamReternedDto> GetExamById(Guid examId);
    Task<ExamReternedDto> EditExam(Guid examId, EditedExamDto model);
    Task DeleteExam(Guid examId);
}
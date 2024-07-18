
using Education.System.Core.Application;
using Education.System.Core.Dto.ApplicationDto;

namespace Education.System.IServices.IApplicationService;

public interface ICallSupportService
{
    Task<CallSupport> AddCallSupport(CallSupportDto dto);
    Task<List<CallSupport>> GetAllPendingCallSupport();
    Task<List<CallSupport>> GetAllCallSupport();
    Task<List<CallSupport>> GetAllStudentCallSupports(string studentId);

    Task<bool> AcceptCallSupport(Guid callSupportId);
    Task<bool> RejectCallSupport(Guid callSupportId);
    Task<CallSupport> GetCallSupportById(Guid id);
}
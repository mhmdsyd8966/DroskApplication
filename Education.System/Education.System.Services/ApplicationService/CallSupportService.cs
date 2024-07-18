using Education.System.Core.Application;
using Education.System.Core.Constants;
using Education.System.Core.Dto.ApplicationDto;
using Education.System.IServices.IApplicationService;
using Education.System.Presentation.Context;
using Microsoft.EntityFrameworkCore;

namespace Education.System.Services.ApplicationService;

public class CallSupportService(
    TheLayerContext context,
    ITransactionService transactionService) : ICallSupportService
{
    public async Task<CallSupport> AddCallSupport(CallSupportDto dto)
    {
        var newSupportCall = new CallSupport()
        {
            Id = Guid.NewGuid(),
            StudentId = dto.StudentId,
            finalprice = dto.Finalprice,
            phoneNumber = dto.PhoneNumber,
            PackageId = dto.PackageId,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            DiscountCode = dto.DiscountCode,
            TransactionPhoto = dto.TransactionPhoto,
            Status = CallSupportStatus.Pending
        };
        await context.CallSupports.AddAsync(newSupportCall);

        await context.SaveChangesAsync();
        return newSupportCall;
    }

    public async Task<List<CallSupport>> GetAllPendingCallSupport()
    {
        var pendingcalls = await context.CallSupports.Where(x => x.Status == CallSupportStatus.Pending).ToListAsync();
        return pendingcalls;
    }
    public async Task<List<CallSupport>> GetAllCallSupport()
    {
        var pendingCalls = await context.CallSupports.ToListAsync();
        return pendingCalls;
    }

    public async Task<List<CallSupport>> GetAllStudentCallSupports(string studentId)
    {
        var calls = await context.CallSupports.Where(x => x.StudentId == studentId).ToListAsync();
        return calls;
    }

    public async Task<bool> AcceptCallSupport(Guid callSupportId)
    {
        var callSupport = await GetCallSupportById(callSupportId);
        if (!callSupport.Status.Equals(CallSupportStatus.Pending))
        {
            throw new Exception("Call Support already reseved and not pending anymore");
        }
        callSupport.Status = CallSupportStatus.Accepted;
        context.CallSupports.Update(callSupport);
        await context.SaveChangesAsync();
        await transactionService.MakeTransaction(callSupport);
        return true;
    }

    public async Task<bool> RejectCallSupport(Guid callSupportId)
    {
        var callSupport = await GetCallSupportById(callSupportId);
        if (!callSupport.Status.Equals(CallSupportStatus.Pending))
        {
            throw new Exception("Call Support already reseved and not pending anymore");
        }
        callSupport.Status = CallSupportStatus.Rejected;
        context.CallSupports.Update(callSupport);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<CallSupport> GetCallSupportById(Guid id) => await context.CallSupports.FirstOrDefaultAsync(c => c.Id.Equals(id)) ??
                                                                   throw new Exception(
                                                                       "Can't find call support with id");

}
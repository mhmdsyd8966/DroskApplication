using Education.System.Core.Application;
using Education.System.Core.Dto.ApplicationDto;
using Education.System.Core.Views;
namespace Education.System.IServices.IApplicationService;

public interface ITransactionService
{
    Task<Transaction> MakeTransaction(CallSupport model);
}
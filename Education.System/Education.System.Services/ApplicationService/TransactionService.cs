using Education.System.Services.Helpers;
using Microsoft.EntityFrameworkCore;
using Education.System.Core.Application;
using Education.System.Core.Dto.ApplicationDto;
using Education.System.IServices.IApplicationService;
using Education.System.Presentation.Context;
using Education.System.Core.Helpers.Functions;
using Education.System.Core.Constants;

namespace Education.System.Services.ApplicationService;

public class TransactionService(
    TheLayerContext context,
    IPackageService packageService,
    IDiscountService discountService) : ITransactionService
{
    public async Task<Transaction> MakeTransaction(CallSupport model)
    {
        var transaction =
            await AddTranscation(await model.FromCallSupportToTransactionDto(packageService, discountService));
        await packageService.AddPackageToStudentManual(model.StudentId, model.PackageId);
        await discountService.DeleteDiscount(Guid.Parse(model.DiscountCode));

        return transaction;
    }


    private async Task<Transaction> AddTranscation(TransactionDto dto)
    {
        var package = await packageService.GetPackageById(dto.PackageId);

        var transaction = new Transaction()
        {
            StudentId = dto.StudentId,
            Id = Guid.NewGuid(),
            Status = CallSupportStatus.Accepted,
            DiscountCode = dto.DiscountCode,
            DiscountPercentage = dto.DiscountPercentage,
            PackageId = dto.PackageId,
            PackagePrice = dto.PackagePrice,
            PackageName = dto.PackageName,
            StudentName = dto.StudentName,
            FinalPrice = package.PackagePrice * (dto.DiscountPercentage / 100),
        };
        await context.Transactions.AddAsync(transaction);
        await context.SaveChangesAsync();
        return transaction;
    }
}
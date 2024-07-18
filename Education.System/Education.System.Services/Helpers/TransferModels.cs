

using Education.System.Core.Application;
using Education.System.Core.Dto.ApplicationDto;
using Education.System.IServices.IApplicationService;

namespace Education.System.Services.Helpers;

public static class TransferModels
{
    public static async Task<TransactionDto> FromCallSupportToTransactionDto(this CallSupport model,
        IPackageService packageService, IDiscountService discountService)

    {
        var discount = await discountService.GetDiscountIfAvailable(model.DiscountCode);
        var package = await packageService.GetPackageById(model.PackageId);
        return new TransactionDto()
        {
            StudentId = model.StudentId,
            DiscountCode = model.DiscountCode,
            Status = model.Status,
            FinalPrice = model.finalprice,
            StudentName = model.FirstName + " " + model.LastName,
            PackageId = model.PackageId,
            DiscountPercentage = discount.IsAvailable ? discount.DiscountPercentege : 0,
            PackageName = package.PackageName,
            PackagePrice = package.PackagePrice
        };
    }
}
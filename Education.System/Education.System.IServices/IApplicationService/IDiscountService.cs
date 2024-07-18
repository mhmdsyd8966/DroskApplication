
using Education.System.Core.Application;
using Education.System.Core.Dto.ApplicationDto;

namespace Education.System.IServices.IApplicationService;

public interface IDiscountService
{
    Task<Discount> AddDiscount(int percentege);
    Task<List<Discount>> GetAllDiscounts();
    Task<DiscountDto> GetDiscountIfAvailable(string DiscountId);
    Task DeleteDiscount(Guid DiscountId);

}
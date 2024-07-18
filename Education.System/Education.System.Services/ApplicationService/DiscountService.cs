using Education.System.Core.Application;
using Education.System.Core.Dto.ApplicationDto;
using Education.System.IServices.IApplicationService;
using Education.System.Presentation.Context;
using Microsoft.EntityFrameworkCore;

namespace Education.System.Services.ApplicationService;

public class DiscountService(TheLayerContext context) : IDiscountService
{
    public async Task<Discount> AddDiscount(int percentege)
    {
        var discount = new Discount()
        {
            Id = Guid.NewGuid(),
            Pencentege = percentege
        };
        await context.Discounts.AddAsync(discount);
        await context.SaveChangesAsync();
        return discount;
    }

    public async Task<List<Discount>> GetAllDiscounts()
    {
        var discounts = await context.Discounts.ToListAsync();
        return discounts;
    }

    public async Task<DiscountDto> GetDiscountIfAvailable(string discountId)
    {
        if (string.IsNullOrEmpty(discountId))
        {
            return new DiscountDto()
            {
                IsAvailable = false
            };
        }
        var discount = await context.Discounts.FirstOrDefaultAsync(x => x.Id == Guid.Parse(discountId));
        if (discount == null)
            return new DiscountDto()
            {
                IsAvailable = false
            };
        var discountDto = new DiscountDto()
        {
            DiscountId = discountId,
            DiscountPercentege = discount.Pencentege,
            IsAvailable = true
        };
        return discountDto;
    }

    public async Task DeleteDiscount(Guid discountId)
    {
        var discount = await context.Discounts.FirstOrDefaultAsync(x => x.Id == discountId) ??
                       throw new NullReferenceException("Discount Doesn't exist");
        context.Discounts.Remove(discount);
        await context.SaveChangesAsync();
    }
}
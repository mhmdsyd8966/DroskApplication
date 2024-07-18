using Education.System.Core.Application;

namespace Education.System.Core.Dto.ApplicationDto;

public class DiscountDto
{
    public bool IsAvailable { get; set; }
    public string DiscountId { get; set; }
    public double DiscountPercentege { get; set; } = 0;

    public double GetFinalPrice(Package package) => IsAvailable
        ? package.PackagePrice - DiscountPercentege * (package.PackagePrice / 100)
        : package.PackagePrice;
}
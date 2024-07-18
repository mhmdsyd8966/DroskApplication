using System.Diagnostics.CodeAnalysis;

namespace Education.System.Core.Dto.ApplicationDto;

public class TransactionDto
{
    public string StudentName { get; set; }
    public string StudentId { get; set; }
    public Guid PackageId { get; set; }
    public string PackageName { get; set; }
    public double PackagePrice { get; set; }
    [AllowNull]
    public string DiscountCode { get; set; }
    public double DiscountPercentage { get; set; }
    public double FinalPrice { get; set; }
    public string Status { get; set; }
}
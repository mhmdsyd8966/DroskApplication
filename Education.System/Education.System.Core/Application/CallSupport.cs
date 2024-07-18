using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Education.System.Core.Application;

public class CallSupport
{
    [Key]
    public Guid Id { get; set; }
    [DataType(DataType.PhoneNumber)]
    public string phoneNumber { set; get; }
    public string FirstName { get; set; }
    public string LastName { set; get; }
    public string TransactionPhoto { set; get; }
    public Guid PackageId { get; set; }
    public double finalprice { get; set; }
    [AllowNull]
    public string? DiscountCode { get; set; }
    public string Status { get; set; }
    public string StudentId { get; set; }
}
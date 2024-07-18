using Education.System.Core.Dto.ApplicationDto;
using Education.System.IServices.IApplicationService;
using System.ComponentModel.DataAnnotations;

namespace Education.System.Client.ViewModels;

public class AddCallSupportModelView
{
    [Required]
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { set; get; }

    public double Price { set; get; } = 0;

    [Required]
    [MaxLength(20)]
    [MinLength(3)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(20)]
    [MinLength(3)]
    public string LastName { set; get; }

    [Required] public IFormFile TransactionPhoto { set; get; }
    [Required] public Guid PackageId { get; set; }

    public string? DiscountCode { get; set; }
    [Required]
    public string StudentId { get; set; }

    public AddCallSupportModelView(double price, Guid PackageId, string StudentId)
    {
        Price = price;
        this.PackageId = PackageId;
        this.StudentId = StudentId;
    }
    public AddCallSupportModelView()
    {
        
    }
    public async Task<CallSupportDto> ToCallSupportDto(IStorageService storageService, double finalPrice)
    {
        return new CallSupportDto()
        {
            StudentId = StudentId,
            DiscountCode = DiscountCode,
            FirstName = FirstName,
            LastName = LastName,
            PackageId = PackageId,
            Finalprice = finalPrice,
            PhoneNumber = PhoneNumber,
            TransactionPhoto = await storageService.AddPhotoToStorage(TransactionPhoto),
        };
    }
}

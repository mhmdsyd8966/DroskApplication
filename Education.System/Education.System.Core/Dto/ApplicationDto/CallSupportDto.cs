namespace Education.System.Core.Dto.ApplicationDto;

public class CallSupportDto
{
    public string PhoneNumber { set; get; }
    public string FirstName { get; set; }
    public string LastName { set; get; }
    public string TransactionPhoto { set; get; }
    public Guid PackageId { get; set; }
    public double Finalprice { get; set; }
    public string DiscountCode { get; set; }
    public string StudentId { get; set; }
}
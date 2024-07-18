using Education.System.Core.Application;
using Education.System.Core.Dto.ApplicationDto;
using Education.System.Core.Views;
namespace Education.System.IServices.IApplicationService;

public interface IAdvertisementServices
{
    Task AddPackageToAdvertisements(Guid packageId);
    Task RemovePackageFromAdvertisements(Guid packageId);
    Task<List<Advertisement>> GetAllAdvertisements();
    Task<Advertisement> GetAdvertismentByPackageId(Guid id);
    bool IsThisPackageExistInAdvertisements(Guid packageId);
}
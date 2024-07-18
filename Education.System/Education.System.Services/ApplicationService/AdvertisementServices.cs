using Education.System.Core.Application;
using Education.System.Core.Constants;
using Education.System.Core.Helpers.Functions;
using Education.System.IServices.IApplicationService;
using Education.System.IServices.IdentityService;
using Education.System.Presentation.Context;
using Microsoft.EntityFrameworkCore;

namespace Education.System.Services.ApplicationService;

public class AdvertisementServices(TheLayerContext context, IPackageService packageService, ITeacherService teacherService) : IAdvertisementServices
{
    public async Task AddPackageToAdvertisements(Guid packageId)
    {
        if (IsAdvertisementsFull())
        {
            throw new Exception("maximam number of packages in advertisements");
        }

        var package = await packageService.GetPackageById(packageId);
        if (IsThisPackageExistInAdvertisements(packageId))
        {
            throw new Exception("This package already in advetisments");
        }
        var teacher = await teacherService.GetTeacherById(package.TeacherId);
        await context.Advertisements.AddAsync(package.FromPackageToSelectedPackage(teacher));
        await context.SaveChangesAsync();
    }

    public async Task RemovePackageFromAdvertisements(Guid packageId)
    {
        if (IsAdvertisementsEmpty())
        {
            throw new Exception("No Advertisements");
        }
        var advertisement = await GetAdvertismentByPackageId(packageId);
        context.Advertisements.Remove(advertisement);
        await context.SaveChangesAsync();
    }

    public async Task<List<Advertisement>> GetAllAdvertisements() =>
        await context.Advertisements.ToListAsync();
    public async Task<Advertisement> GetAdvertismentByPackageId(Guid id) =>
        await context.Advertisements.FirstOrDefaultAsync(a => a.PackageId.Equals(id)) ??
        throw new Exception("Can't Find advertisment with this id");

    public bool IsThisPackageExistInAdvertisements(Guid packageId) =>
        context.Advertisements.Any(p => p.PackageId.Equals(packageId));

    private bool IsAdvertisementsFull() =>
        context.Advertisements.Count() == AdvertisementsConstrains.NumberOfPackages;

    private bool IsAdvertisementsEmpty() =>
        !context.Advertisements.Any();

}
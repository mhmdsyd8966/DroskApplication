

using Education.System.IServices.IApplicationService;
using Education.System.IServices.IdentityService;
using Education.System.Services.ApplicationService;
using Education.System.Services.IdentityService;

namespace Education.System.Client.Helpers.ServicesConfigurationHelpers;

public static class ServicesDI
{
    public static IServiceCollection AddApplicationServicesDI(this IServiceCollection services)
    {
        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<IBugReportService, BugReportService>();
        services.AddScoped<ICallSupportService, CallSupportService>();
        services.AddScoped<IDiscountService, DiscountService>();
        services.AddScoped<ILessonService, LessonService>();
        services.AddScoped<IPackageService, PackageService>();
        services.AddScoped<IPostService, PostService>();
        services.AddScoped<ITransactionService, TransactionService>();
        services.AddScoped<IStorageService, StorageService>();
        services.AddScoped<IExamServices, ExamServices>();
        services.AddScoped<IAdvertisementServices, AdvertisementServices>();
        return services;
    }
    public static IServiceCollection AddApplicationIIdentityServicesDI(this IServiceCollection services)
    {
        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<ITeacherService, TeacherService>();
        services.AddScoped<IStudentService, StudentService>();
        return services;
    }
}
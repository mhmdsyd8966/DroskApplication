using Education.System.Core.Helpers.Enums;

namespace Education.System.Client.ViewModels;

public class ServiceReturnErrorViewModel
{
    public ApiCallStatus Status { set; get; }
    public string? ErrorMessage { set; get; }

    public ServiceReturnErrorViewModel()
    {
        Status = ApiCallStatus.NoAction;
        ErrorMessage = string.Empty;
    }
    public ServiceReturnErrorViewModel(ApiCallStatus status, string? errorMessage = null)
    {
        Status = status;
        ErrorMessage = string.IsNullOrEmpty(errorMessage) ? string.Empty : errorMessage;
    }
}
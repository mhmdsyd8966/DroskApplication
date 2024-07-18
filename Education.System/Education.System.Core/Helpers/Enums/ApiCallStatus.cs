namespace Education.System.Core.Helpers.Enums;

[Flags]
public enum ApiCallStatus
{
    NoAction = 0,
    Pending = 1,
    Responsed = 2,
    Success200_201_204 = 4,
    NotAuthorized401 = 8,
    BadRequest400 = 16,
    NotFound404 = 32
}
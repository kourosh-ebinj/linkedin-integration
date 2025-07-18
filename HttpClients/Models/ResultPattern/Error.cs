namespace LinkedIn_Notes.HttpClients.Models.ResultPattern
{
    public sealed record Error([NotNull] string Message,
                               ErrorType Type = ErrorType.Failure,
                               string? Code = null
        );

    public enum ErrorType
    {
        Failure = 0,
        NotFound = 1,
        Validation = 2,
        Conflict = 3,
        AccessUnAuthorized = 4,
        AccessForbidden = 5
    }

}

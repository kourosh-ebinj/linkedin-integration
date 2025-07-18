using System.Text.Json.Serialization;

namespace LinkedIn_Notes.HttpClients.Models.ResultPattern
{
    public record Result
    {
        protected Result()
        {
            Error = default;
        }

        protected Result(Error error)
        {
            Error = error;
        }

        public bool IsSuccess => Error is null;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Error? Error { get; }

        public static Result Success() =>
            new Result();

        public static Result Failure(Error error) =>
            new(error);
        public static Result Failure([NotNull] string errorMessage, ErrorType errorType, string? errorCode) =>
            new(new Error(errorMessage, errorType, errorCode));

        public static implicit operator Result(Error error) =>
            new(error);
    }
}

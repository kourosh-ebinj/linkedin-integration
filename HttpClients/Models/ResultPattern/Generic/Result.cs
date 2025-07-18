
namespace LinkedIn_Notes.HttpClients.Models.ResultPattern.Generic
{
    public record Result<TResponse> : Result 
    {
        public Result(TResponse response)
        {
            Response = response;
        }

        public Result(Error error): base(error)
        {
            Response = default;
        }

        public TResponse? Response { get; set; }

        public static Result<TResponse> Success(TResponse response) =>
            new Result<TResponse>(response);

        public static Result<TResponse> Failure(Error error) =>
            new Result<TResponse>(error);
        
        public static Result<TResponse> Failure([NotNull] string errorMessage) =>
            new Result<TResponse>(new Error(errorMessage));

        public static implicit operator Result<TResponse>(Error error)  =>
            Failure(error);

        public static implicit operator Result<TResponse>(string message) =>
            new Result<TResponse>(message);
    }
}

using Domain.Common.Enums;

namespace Domain.Common
{
    public class Result
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<string> Errors { get; set; } = new();
        public ErrorType? Error { get; set; }

        protected Result() { }

        public static Result Ok(string message = "Operação realizada com sucesso")
            => new Result { Success = true, Message = message };

        public static Result Failure(string message = "Erro na operação", ErrorType error = ErrorType.Unexpected)
            => new Result { Success = false, Message = message, Error = error };

        public static Result Validation(List<string> errors)
            => new Result { Success = false, Message = string.Join(", ", errors), Errors = errors, Error = ErrorType.Validation };

        public static Result Unauthorized(string message) => Failure(message, ErrorType.Unauthorized);
        public static Result NotFound(string message) => Failure(message, ErrorType.NotFound);
        public static Result Conflict(string message) => Failure(message, ErrorType.Conflict);
    }

    public class Result<T> : Result
    {
        public T Data { get; private set; } = default!;

        private Result() { }

        public static Result<T> Ok(T data, string message = "Operação realizada com sucesso")
            => new Result<T> { Success = true, Message = message, Data = data };

        public new static Result<T> Failure(string message = "Erro na operação", ErrorType error = ErrorType.Unexpected)
            => new Result<T> { Success = false, Message = message, Error = error };

        public new static Result<T> Validation(List<string> errors)
            => new Result<T> { Success = false, Message = string.Join(", ", errors), Errors = errors, Error = ErrorType.Validation };
    }
}
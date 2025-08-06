namespace Domain.Common
{
    public class Result
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<string> Errors { get; set; } = new();

        protected Result() { }

        public static Result Ok(string message = "Operação realizada com sucesso")
            => new Result { Success = true, Message = message };

        public static Result Failure(string message = "Erro na operação")
            => new Result { Success = false, Message = message };

        public static Result Failure(List<string> errors)
            => new Result { Success = false, Message = string.Join(", ", errors), Errors = errors };
    }

    public class Result<T> : Result
    {
        public T Data { get; private set; } = default!;

        private Result() { }

        public static Result<T> Ok(T data, string message = "Operação realizada com sucesso")
            => new Result<T> { Success = true, Message = message, Data = data };

        public new static Result<T> Failure(string message = "Erro na operação")
            => new Result<T> { Success = false, Message = message, Data = default! };

        public new static Result<T> Failure(List<string> errors)
            => new Result<T> { Success = false, Message = string.Join(", ", errors), Data = default!, Errors = errors };
    }
}
namespace Application.Interfaces
{
    public interface IValidatorService
    {
        Task ValidateAsync<T>(T dto);
    }
}

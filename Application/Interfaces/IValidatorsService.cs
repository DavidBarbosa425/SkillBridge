namespace Application.Interfaces
{
    public interface IValidatorsService
    {
        Task ValidateAsync<T>(T dto);
    }
}

using Application.Interfaces;
using FluentValidation;

namespace Application.Validators
{
    public class ValidatorService : IValidatorService
    {

        private readonly IServiceProvider _serviceProvider;
        public ValidatorService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task ValidateAsync<T>(T dto)
        {
            var validator = (IValidator<T>?)_serviceProvider.GetService(typeof(IValidator<T>));
            if (validator == null)
                throw new Exception($"Validator for type {typeof(T).Name} not found.");

            var result = await validator.ValidateAsync(dto);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);
        }
    }
}

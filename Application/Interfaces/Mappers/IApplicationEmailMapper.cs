using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces.Mappers
{
    public interface IApplicationEmailMapper
    {
        Task<SendEmail> ToSendEmailConfirmation(User user);
    }
}

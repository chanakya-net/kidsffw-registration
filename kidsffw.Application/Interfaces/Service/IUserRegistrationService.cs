namespace kidsffw.Application.Interfaces.Service;

using Common.DTO;

public interface IUserRegistrationService
{
    Task<GetUserRequestDto> AddUserRegistration(CreateUserRegistrationRequestDto request);
    Task<string> UpdateUserRegistration(UpdateUserTransactionDtoRequest request);
    Task<IEnumerable<GetUserRequestDto>> GetUsersByMobileNumber(string mobileNumber);
}
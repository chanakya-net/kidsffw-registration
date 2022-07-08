namespace kidsffw.Application.Interfaces.Service;

using Common.DTO;

public interface IUserRegistrationService
{
    Task<CreateUserRegistrationResponseDto> AddUserRegistration(CreateUserRegistrationRequestDto request);
    Task<IEnumerable<GetUserRequestDto>> GetUsersByMobileNumber(string mobileNumber);
}
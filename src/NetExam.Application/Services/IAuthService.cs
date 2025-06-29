using NetExam.Application.Dtos;

namespace NetExam.Application.Services;

public interface IAuthService
{

    Task<long> SignUpUserAsync(UserCreateDto dto);


    Task<LoginResponseDto> LoginUserAsync(UserLoginDto dto);


    Task<LoginResponseDto> RefreshTokenAsync(RefreshRequestDto request);


    Task LogOut(string token);




}

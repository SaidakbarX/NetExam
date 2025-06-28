using NetExam.Application.Dtos;
using System.Security.Claims;

namespace NetExam.Application.Helpers;

public interface ITokenService
{
    public string GenerateToken(UserReadDto user);
    public string GenerateRefreshToken();
    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
}


﻿using Microsoft.IdentityModel.Tokens;
using NetExam.Application.Dtos;
using NetExam.Application.Setting;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace NetExam.Application.Helpers;

public class TokenService : ITokenService
{
    private readonly string _lifetime;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly string _securityKey;

    public TokenService(JwtAppSettings jwtSetting)
    {
        _lifetime = jwtSetting.Lifetime;
        _issuer = jwtSetting.Issuer;
        _audience = jwtSetting.Audience;
        _securityKey = jwtSetting.SecurityKey;
    }

    public string GenerateToken(UserReadDto user)
    {
        var identityClaims = new Claim[]
   {
        new Claim("Id", user.Id.ToString()),
        new Claim("FullName", user.FullName),
        new Claim("Email", user.Email),
        new Claim("RoleName", user.RoleName),
        new Claim(JwtRegisteredClaimNames.UniqueName, user.FullName), 
        new Claim(ClaimTypes.Role, user.RoleName),
        new Claim(ClaimTypes.Email, user.Email)
   };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_securityKey!));
        var keyCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expiresHours = int.Parse(_lifetime);
        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: identityClaims,
            expires: TimeHelper.GetDateTime().AddHours(expiresHours),
            signingCredentials: keyCredentials
        );


        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var randomBytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }

    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = _issuer,
            ValidateAudience = true,
            ValidAudience = _audience,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_securityKey!))
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.ValidateToken(token, tokenValidationParameters, out _);
    }


}


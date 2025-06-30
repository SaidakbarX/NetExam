using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using NetExam.Application.Dtos;
using NetExam.Application.Services;

namespace NetExam.Api.Entpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/auth").WithTags("Authentication");

        group.MapPost("/register", async (UserCreateDto dto, IAuthService authService,[FromServices] IValidator<UserCreateDto> validator) =>
        {
            var validation = await validator.ValidateAsync(dto);
            if (!validation.IsValid)
                return Results.BadRequest(validation.Errors);

            var result = await authService.SignUpUserAsync(dto);
            return Results.Ok(result);
        });

        group.MapPost("/login", async (UserLoginDto dto, IAuthService authService, [FromServices] IValidator<UserLoginDto> validator) =>
        {
            var validation = await validator.ValidateAsync(dto);
            if (!validation.IsValid)
                return Results.BadRequest(validation.Errors);

            var result = await authService.LoginUserAsync(dto);
            return Results.Ok(result);
        });

        group.MapPost("/refresh-token", async (RefreshRequestDto dto, IAuthService authService, [FromServices] IValidator<RefreshRequestDto> validator) =>
        {
            var validation = await validator.ValidateAsync(dto);
            if (!validation.IsValid)
                return Results.BadRequest(validation.Errors);

            var result = await authService.RefreshTokenAsync(dto);
            return Results.Ok(result);
        });

        group.MapPost("/logout", async (HttpContext http, IAuthService authService) =>
        {
            var accessToken = http.Request.Headers.Authorization.ToString().Replace("Bearer ", "");
            await authService.LogOut(accessToken);
            return Results.Ok();
        });
    }
}
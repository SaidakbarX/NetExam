using FluentValidation;
using NetExam.Application.Dtos;
using NetExam.Application.Services;

namespace NetExam.Api.Entpoints;

public static class UserRoleEndpoints
{
    public static void MapUserRoleEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/user-roles").WithTags("User Roles");

        group.MapGet("/", async (IUserRoleService service) =>
        {
            var roles = await service.GetAllAsync();
            return Results.Ok(roles);
        });

        group.MapGet("/{id:long}", async (long id, IUserRoleService service) =>
        {
            var role = await service.GetByIdAsync(id);
            return role is not null ? Results.Ok(role) : Results.NotFound();
        });

        group.MapPost("/", async (UserRoleDto dto, IUserRoleService service, IValidator<UserRoleDto> validator) =>
        {
            var validation = await validator.ValidateAsync(dto);
            if (!validation.IsValid)
                return Results.BadRequest(validation.Errors);

            await service.AddAsync(dto);
            return Results.Ok();
        });

        group.MapPut("/", async (UserRoleDto dto, IUserRoleService service, IValidator<UserRoleDto> validator) =>
        {
            var validation = await validator.ValidateAsync(dto);
            if (!validation.IsValid)
                return Results.BadRequest(validation.Errors);

            await service.UpdateAsync(dto);
            return Results.Ok();
        });

        group.MapDelete("/{id:long}", async (long id, IUserRoleService service) =>
        {
            await service.DeleteAsync(id);
            return Results.NoContent();
        });
    }
}

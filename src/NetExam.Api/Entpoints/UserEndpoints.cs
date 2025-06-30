using NetExam.Application.Services;

namespace NetExam.Api.Entpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/users").WithTags("Users");

        group.MapPut("/{userId:long}/role", async (
            long userId,
            string userRole,
            IUserService service) =>
        {
            await service.UpdateUserRoleAsync(userId, userRole);
            return Results.Ok();
        });

        group.MapDelete("/{userId:long}", async (
            long userId,
            HttpRequest request,
            IUserService service) =>
        {
            var userRole = request.Query["userRole"].ToString();

            if (string.IsNullOrWhiteSpace(userRole))
                return Results.BadRequest("userRole query param is required");

            await service.DeleteUserByIdAsync(userId, userRole);
            return Results.NoContent();
        });
    }
}
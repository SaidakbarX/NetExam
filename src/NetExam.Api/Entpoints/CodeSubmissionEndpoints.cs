using FluentValidation;
using NetExam.Application.Dtos;
using NetExam.Application.Services;

namespace NetExam.Api.Entpoints;

public static class CodeSubmissionEndpoints
{
    public static void MapCodeSubmissionEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/code-submissions").WithTags("Code Submissions");

        group.MapGet("/", async (ICodeSubmissionService service) =>
        {
            var list = await service.GetAllAsync();
            return Results.Ok(list);
        });

        group.MapGet("/{id:long}", async (long id, ICodeSubmissionService service) =>
        {
            var item = await service.GetByIdAsync(id);
            return item is not null ? Results.Ok(item) : Results.NotFound();
        });

        group.MapGet("/by-user/{userId:long}", async (long userId, ICodeSubmissionService service) =>
        {
            var items = await service.GetByUserIdAsync(userId);
            return Results.Ok(items);
        });

        group.MapGet("/by-question/{questionId:long}", async (long questionId, ICodeSubmissionService service) =>
        {
            var items = await service.GetByQuestionIdAsync(questionId);
            return Results.Ok(items);
        });

        group.MapPost("/", async (CodeSubmissionDto dto, ICodeSubmissionService service, IValidator<CodeSubmissionDto> validator) =>
        {
            var validation = await validator.ValidateAsync(dto);
            if (!validation.IsValid)
                return Results.BadRequest(validation.Errors);

            await service.AddAsync(dto);
            return Results.Created("/api/code-submissions", dto);
        });
    }
}

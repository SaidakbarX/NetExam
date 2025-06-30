using FluentValidation;
using NetExam.Application.Dtos;
using NetExam.Application.Services;

namespace NetExam.Api.Entpoints;

public static class CodeQuestionEndpoints
{
    public static void MapCodeQuestionEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/code-questions").WithTags("Code Questions");

        group.MapGet("/", async (ICodeQuestionService service) =>
        {
            var questions = await service.GetAllAsync();
            return Results.Ok(questions);
        });

        group.MapGet("/{id:long}", async (long id, ICodeQuestionService service) =>
        {
            var question = await service.GetByIdAsync(id);
            return question is not null ? Results.Ok(question) : Results.NotFound();
        });

        group.MapPost("/", async (CodeQuestionDto dto, ICodeQuestionService service, IValidator<CodeQuestionDto> validator) =>
        {
            var validation = await validator.ValidateAsync(dto);
            if (!validation.IsValid)
                return Results.BadRequest(validation.Errors);

            await service.AddAsync(dto);
            return Results.Created("/api/code-questions", dto);
        });

        group.MapPut("/{id:long}", async (long id, CodeQuestionDto dto, ICodeQuestionService service, IValidator<CodeQuestionDto> validator) =>
        {
            if (dto.Id != id)
                return Results.BadRequest("ID does not match DTO ID");

            var validation = await validator.ValidateAsync(dto);
            if (!validation.IsValid)
                return Results.BadRequest(validation.Errors);

            await service.UpdateAsync(dto);
            return Results.Ok();
        });

        group.MapDelete("/{id:long}", async (long id, ICodeQuestionService service) =>
        {
            await service.DeleteAsync(id);
            return Results.NoContent();
        });
    }
}
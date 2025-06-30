using FluentValidation;
using NetExam.Application.Dtos;
using NetExam.Application.Services;

namespace NetExam.Api.Entpoints;

public static class ExamQuestionEndpoints
{
    public static void MapExamQuestionEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/exam-questions").WithTags("Exam Questions");

        group.MapGet("/by-exam/{examId:long}", async (long examId, IExamQuestionService service) =>
        {
            var result = await service.GetByExamIdAsync(examId);
            return Results.Ok(result);
        });

        group.MapPost("/", async (ExamQuestionDto dto, IExamQuestionService service, IValidator<ExamQuestionDto> validator) =>
        {
            var validation = await validator.ValidateAsync(dto);
            if (!validation.IsValid)
                return Results.BadRequest(validation.Errors);

            await service.AddAsync(dto);
            return Results.Ok();
        });

        group.MapPut("/", async (ExamQuestionDto dto, IExamQuestionService service, IValidator<ExamQuestionDto> validator) =>
        {
            var validation = await validator.ValidateAsync(dto);
            if (!validation.IsValid)
                return Results.BadRequest(validation.Errors);

            await service.UpdateAsync(dto);
            return Results.Ok();
        });

        group.MapDelete("/{examId:long}/{questionId:long}", async (long examId, long questionId, IExamQuestionService service) =>
        {
            await service.RemoveAsync(examId, questionId);
            return Results.NoContent();
        });
    }
}
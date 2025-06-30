using FluentValidation;
using NetExam.Application.Dtos;
using NetExam.Application.Services;

namespace NetExam.Api.Entpoints;

public static class ExamResultEndpoints
{
    public static void MapExamResultEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/exam-results").WithTags("Exam Results");

        group.MapGet("/by-user/{userId:long}", async (long userId, IExamResultService service) =>
        {
            var result = await service.GetByUserIdAsync(userId);
            return Results.Ok(result);
        });

        group.MapGet("/by-exam/{examId:long}", async (long examId, IExamResultService service) =>
        {
            var result = await service.GetByExamIdAsync(examId);
            return Results.Ok(result);
        });

        group.MapGet("/by-title/{examTitle}", async (string examTitle, IExamResultService service) =>
        {
            var result = await service.GetByExamTitleAsync(examTitle);
            return Results.Ok(result);
        });

        group.MapGet("/average-score/{examId:long}", async (long examId, IExamResultService service) =>
        {
            var average = await service.GetAverageScoreAsync(examId);
            return Results.Ok(average);
        });

        group.MapPost("/", async (ExamResultDto dto, IExamResultService service, IValidator<ExamResultDto> validator) =>
        {
            var validation = await validator.ValidateAsync(dto);
            if (!validation.IsValid)
                return Results.BadRequest(validation.Errors);

            await service.AddAsync(dto);
            return Results.Ok();
        });
    }
}

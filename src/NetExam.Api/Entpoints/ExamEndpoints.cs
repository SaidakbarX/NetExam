using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using NetExam.Application.Dtos;
using NetExam.Application.Services;

namespace NetExam.Api.Entpoints
{
    public static class ExamEndpoints
    {
        public static void MapExamEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/exams").WithTags("Exams");

            group.MapGet("/", async ([FromServices] IExamService service) =>
            {
                var exams = await service.GetAllAsync();
                return Results.Ok(exams);
            });

            group.MapGet("/{id:long}", async (long id, IExamService service) =>
            {
                var exam = await service.GetByIdAsync(id);
                return exam is not null ? Results.Ok(exam) : Results.NotFound();
            });

            group.MapPost("/", async (ExamCreateDto dto, IExamService service, IValidator<ExamCreateDto> validator) =>
            {
                var validation = await validator.ValidateAsync(dto);
                if (!validation.IsValid)
                    return Results.BadRequest(validation.Errors);

                await service.AddAsync(dto);
                return Results.Created("/api/exams", dto);
            });

            group.MapPut("/{id:long}", async (long id, ExamCreateDto dto, IExamService service, IValidator<ExamCreateDto> validator) =>
            {
                var validation = await validator.ValidateAsync(dto);
                if (!validation.IsValid)
                    return Results.BadRequest(validation.Errors);

                await service.UpdateAsync(dto, id);
                return Results.Ok();
            });

            group.MapDelete("/{id:long}", async (long id, IExamService service) =>
            {
                await service.DeleteAsync(id);
                return Results.NoContent();
            });
        }
    }
}

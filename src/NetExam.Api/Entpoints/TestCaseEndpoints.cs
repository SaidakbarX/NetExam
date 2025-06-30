using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using NetExam.Application.Dtos;
using NetExam.Application.Services;

namespace NetExam.Api.Entpoints;

public static class TestCaseEndpoints
{
    public static void MapTestCaseEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/test-cases").WithTags("Test Cases");

        group.MapGet("/by-question/{questionId:long}", async (long questionId, ITestCaseService service) =>
        {
            var result = await service.GetByQuestionIdAsync(questionId);
            return Results.Ok(result);
        });

        group.MapGet("/{id:long}", async (long id, ITestCaseService service) =>
        {
            var item = await service.GetByIdAsync(id);
            return item is not null ? Results.Ok(item) : Results.NotFound();
        });

        group.MapPost("/", async (TestCaseDto dto, ITestCaseService service, [FromServices] IValidator<TestCaseDto> validator) =>
        {
            var validation = await validator.ValidateAsync(dto);
            if (!validation.IsValid)
                return Results.BadRequest(validation.Errors);

            await service.AddAsync(dto);
            return Results.Ok();
        });

        group.MapPut("/", async (TestCaseDto dto, ITestCaseService service, [FromServices] IValidator<TestCaseDto> validator) =>
        {
            var validation = await validator.ValidateAsync(dto);
            if (!validation.IsValid)
                return Results.BadRequest(validation.Errors);

            await service.UpdateAsync(dto);
            return Results.Ok();
        });

        group.MapDelete("/{id:long}", async (long id, ITestCaseService service) =>
        {
            await service.DeleteAsync(id);
            return Results.NoContent();
        });
    }
}
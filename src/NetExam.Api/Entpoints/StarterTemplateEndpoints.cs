using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using NetExam.Application.Dtos;
using NetExam.Application.Services;

namespace NetExam.Api.Entpoints;

public static class StarterTemplateEndpoints
{
    public static void MapStarterTemplateEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/starter-templates").WithTags("Starter Templates");

        group.MapGet("/by-question/{questionId:long}", async (long questionId, IStarterTemplateService service) =>
        {
            var result = await service.GetByQuestionIdAsync(questionId);
            return Results.Ok(result);
        });

        group.MapGet("/{id:long}", async (long id, IStarterTemplateService service) =>
        {
            var item = await service.GetByIdAsync(id);
            return item is not null ? Results.Ok(item) : Results.NotFound();
        });

        group.MapPost("/", async (StarterTemplateDto dto, IStarterTemplateService service, [FromServices] IValidator<StarterTemplateDto> validator) =>
        {
            var validation = await validator.ValidateAsync(dto);
            if (!validation.IsValid)
                return Results.BadRequest(validation.Errors);

            await service.AddAsync(dto);
            return Results.Ok();
        });

        group.MapPut("/", async (StarterTemplateDto dto, IStarterTemplateService service, [FromServices] IValidator<StarterTemplateDto> validator) =>
        {
            var validation = await validator.ValidateAsync(dto);
            if (!validation.IsValid)
                return Results.BadRequest(validation.Errors);

            await service.UpdateAsync(dto);
            return Results.Ok();
        });

        group.MapDelete("/{id:long}", async (long id, IStarterTemplateService service) =>
        {
            await service.DeleteAsync(id);
            return Results.NoContent();
        });
    }
}
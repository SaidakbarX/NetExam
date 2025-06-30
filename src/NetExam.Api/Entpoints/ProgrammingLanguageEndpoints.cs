using FluentValidation;
using NetExam.Application.Dtos;
using NetExam.Application.Services;

namespace NetExam.Api.Entpoints;

public static class ProgrammingLanguageEndpoints
{
    public static void MapProgrammingLanguageEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/programming-languages").WithTags("Programming Languages");

        group.MapGet("/", async (IProgrammingLanguageService service) =>
        {
            var result = await service.GetAllAsync();
            return Results.Ok(result);
        });

        group.MapGet("/{id:int}", async (int id, IProgrammingLanguageService service) =>
        {
            var item = await service.GetByIdAsync(id);
            return item is not null ? Results.Ok(item) : Results.NotFound();
        });

        group.MapPost("/", async (ProgrammingLanguageDto dto, IProgrammingLanguageService service, IValidator<ProgrammingLanguageDto> validator) =>
        {
            var validation = await validator.ValidateAsync(dto);
            if (!validation.IsValid)
                return Results.BadRequest(validation.Errors);

            await service.AddAsync(dto);
            return Results.Ok();
        });

        group.MapPut("/", async (ProgrammingLanguageDto dto, IProgrammingLanguageService service, IValidator<ProgrammingLanguageDto> validator) =>
        {
            var validation = await validator.ValidateAsync(dto);
            if (!validation.IsValid)
                return Results.BadRequest(validation.Errors);

            await service.UpdateAsync(dto);
            return Results.Ok();
        });

        group.MapDelete("/{id:int}", async (int id, IProgrammingLanguageService service) =>
        {
            await service.DeleteAsync(id);
            return Results.NoContent();
        });
    }
}
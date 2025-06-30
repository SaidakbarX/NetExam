using NetExam.Application.Dtos;
using NetExam.Application.Interfaces;
using NetExam.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Application.Services;

public class StarterTemplateService : IStarterTemplateService
{
    private readonly IStarterTemplateRepository _repository;

    public StarterTemplateService(IStarterTemplateRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<StarterTemplateDto>> GetByQuestionIdAsync(long questionId)
    {
        var items = await _repository.GetByQuestionIdAsync(questionId);
        return items.Select(x => new StarterTemplateDto
        {
            Id = x.Id,
            CodeQuestionId = x.CodeQuestionId,
            ProgrammingLanguageId = x.ProgrammingLanguageId,
            Code = x.Code
        });
    }

    public async Task<StarterTemplateDto?> GetByIdAsync(long id)
    {
        var x = await _repository.GetByIdAsync(id);
        return x == null ? null : new StarterTemplateDto
        {
            Id = x.Id,
            CodeQuestionId = x.CodeQuestionId,
            ProgrammingLanguageId = x.ProgrammingLanguageId,
            Code = x.Code
        };
    }

    public async Task AddAsync(StarterTemplateDto dto)
    {
        var entity = new StarterTemplate
        {
            CodeQuestionId = dto.CodeQuestionId,
            ProgrammingLanguageId = dto.ProgrammingLanguageId,
            Code = dto.Code
        };
        await _repository.AddAsync(entity);
    }

    public async Task UpdateAsync(StarterTemplateDto dto)
    {
        var existing = await _repository.GetByIdAsync(dto.Id);
        if (existing == null) throw new Exception("Not found");
        existing.Code = dto.Code;
        await _repository.UpdateAsync(existing);
    }

    public async Task DeleteAsync(long id)
    {
        await _repository.DeleteAsync(id);
    }
}
using NetExam.Application.Dtos;
using NetExam.Application.Interfaces;
using NetExam.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Application.Services;

public class CodeQuestionService : ICodeQuestionService
{
    private readonly ICodeQuestionRepository _repository;

    public CodeQuestionService(ICodeQuestionRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CodeQuestionDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();
        return entities.Select(q => new CodeQuestionDto
        {
            Id = q.Id,
            Title = q.Title,
            Description = q.Description,
            FunctionName = q.FunctionName,
            CreatedAt = q.CreatedAt
        });
    }

    public async Task<CodeQuestionDto?> GetByIdAsync(long id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return null;

        return new CodeQuestionDto
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            FunctionName = entity.FunctionName,
            CreatedAt = entity.CreatedAt
        };
    }

    public async Task AddAsync(CodeQuestionDto dto)
    {
        var entity = new CodeQuestion
        {
            Title = dto.Title,
            Description = dto.Description,
            FunctionName = dto.FunctionName,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(entity);
    }

    public async Task UpdateAsync(CodeQuestionDto dto)
    {
        var existing = await _repository.GetByIdAsync(dto.Id);
        if (existing == null) throw new Exception("Code question not found");

        existing.Title = dto.Title;
        existing.Description = dto.Description;
        existing.FunctionName = dto.FunctionName;

        await _repository.UpdateAsync(existing);
    }

    public async Task DeleteAsync(long id)
    {
        await _repository.DeleteAsync(id);
    }
}
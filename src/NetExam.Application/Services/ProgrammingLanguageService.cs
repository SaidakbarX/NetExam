using NetExam.Application.Dtos;
using NetExam.Application.Interfaces;
using NetExam.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Application.Services;

public class ProgrammingLanguageService : IProgrammingLanguageService
{
    private readonly IProgrammingLanguageRepository _repository;

    public ProgrammingLanguageService(IProgrammingLanguageRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ProgrammingLanguageDto>> GetAllAsync()
    {
        var items = await _repository.GetAllAsync();
        return items.Select(x => new ProgrammingLanguageDto
        {
            Id = x.Id,
            Name = x.Name
        });
    }

    public async Task<ProgrammingLanguageDto?> GetByIdAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return entity == null ? null : new ProgrammingLanguageDto { Id = entity.Id, Name = entity.Name };
    }

    public async Task<ProgrammingLanguageDto?> GetByNameAsync(string name)
    {
        var entity = await _repository.GetByNameAsync(name);
        return entity == null ? null : new ProgrammingLanguageDto { Id = entity.Id, Name = entity.Name };
    }

    public async Task AddAsync(ProgrammingLanguageDto dto)
    {
        var entity = new ProgrammingLanguage { Name = dto.Name, DisplayName = dto.Name, Version = "1.0", IsActive = true };
        await _repository.AddAsync(entity);
    }

    public async Task UpdateAsync(ProgrammingLanguageDto dto)
    {
        var existing = await _repository.GetByIdAsync(dto.Id);
        if (existing == null) throw new Exception("Not found");
        existing.Name = dto.Name;
        await _repository.UpdateAsync(existing);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}
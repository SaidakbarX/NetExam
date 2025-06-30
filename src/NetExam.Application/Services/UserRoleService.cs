using NetExam.Application.Dtos;
using NetExam.Application.Interfaces;
using NetExam.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Application.Services;

public class UserRoleService : IUserRoleService
{
    private readonly IUserRoleRepository _repository;

    public UserRoleService(IUserRoleRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<UserRoleDto>> GetAllAsync()
    {
        var list = await _repository.GetAllAsync();
        return list.Select(x => new UserRoleDto
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description
        });
    }

    public async Task<UserRoleDto?> GetByIdAsync(long id)
    {
        var x = await _repository.GetByIdAsync(id);
        return x == null ? null : new UserRoleDto
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description
        };
    }

    public async Task<UserRoleDto?> GetByNameAsync(string name)
    {
        var x = await _repository.GetByNameAsync(name);
        return x == null ? null : new UserRoleDto
        {
            Id = x.Id,
            Name = x.Name,
            Description = x.Description
        };
    }

    public async Task AddAsync(UserRoleDto dto)
    {
        var entity = new UserRole
        {
            Name = dto.Name,
            Description = dto.Description,
            CreatedAt = DateTime.UtcNow
        };
        await _repository.AddAsync(entity);
    }

    public async Task UpdateAsync(UserRoleDto dto)
    {
        var existing = await _repository.GetByIdAsync(dto.Id);
        if (existing == null) throw new Exception("Not found");
        existing.Name = dto.Name;
        existing.Description = dto.Description;
        await _repository.UpdateAsync(existing);
    }

    public async Task DeleteAsync(long id)
    {
        await _repository.DeleteAsync(id);
    }
}

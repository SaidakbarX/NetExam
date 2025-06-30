using NetExam.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Application.Services;

public interface IProgrammingLanguageService
{
    Task<IEnumerable<ProgrammingLanguageDto>> GetAllAsync();
    Task<ProgrammingLanguageDto?> GetByIdAsync(int id);
    Task<ProgrammingLanguageDto?> GetByNameAsync(string name);
    Task AddAsync(ProgrammingLanguageDto dto);
    Task UpdateAsync(ProgrammingLanguageDto dto);
    Task DeleteAsync(int id);
}
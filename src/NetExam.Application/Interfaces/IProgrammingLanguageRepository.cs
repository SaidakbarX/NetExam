using NetExam.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Application.Interfaces;

public interface IProgrammingLanguageRepository
{
    Task<ProgrammingLanguage?> GetByIdAsync(int id);
    Task<ProgrammingLanguage?> GetByNameAsync(string name);
    Task<IEnumerable<ProgrammingLanguage>> GetAllAsync();

    Task AddAsync(ProgrammingLanguage language);
    Task UpdateAsync(ProgrammingLanguage language);
    Task DeleteAsync(int id);
}

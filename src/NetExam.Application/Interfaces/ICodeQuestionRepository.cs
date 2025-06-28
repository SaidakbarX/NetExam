using NetExam.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Application.Interfaces;

public interface ICodeQuestionRepository
{
    Task<CodeQuestion?> GetByIdAsync(long id);
    Task<CodeQuestion?> GetWithTestCasesAsync(long id);
    Task<IEnumerable<CodeQuestion>> GetAllAsync();
    Task<IEnumerable<CodeQuestion>> SearchAsync(string keyword);

    Task AddAsync(CodeQuestion question);
    Task UpdateAsync(CodeQuestion question);
    Task DeleteAsync(long id);
}


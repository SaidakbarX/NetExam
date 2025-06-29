using NetExam.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Application.Services;

public interface ICodeQuestionService
{
    Task<IEnumerable<CodeQuestionDto>> GetAllAsync();
    Task<CodeQuestionDto?> GetByIdAsync(long id);
    Task AddAsync(CodeQuestionDto dto);
    Task UpdateAsync(CodeQuestionDto dto);
    Task DeleteAsync(long id);
}

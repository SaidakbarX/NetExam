using NetExam.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetExam.Application.Services;

public interface IStarterTemplateService
{
    Task<IEnumerable<StarterTemplateDto>> GetByQuestionIdAsync(long questionId);
    Task<StarterTemplateDto?> GetByIdAsync(long id);
    Task AddAsync(StarterTemplateDto dto);
    Task UpdateAsync(StarterTemplateDto dto);
    Task DeleteAsync(long id);
}
